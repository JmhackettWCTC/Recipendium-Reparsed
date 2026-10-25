using Microsoft.EntityFrameworkCore;
using Recipendium_Reparsed.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<RecipeDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Migrate from string Category to CategoryId foreign key relationship
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<RecipeDbContext>();
    try
    {
        // Create Category table if it doesn't exist
        db.Database.ExecuteSqlRaw(@"
            IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Category')
            BEGIN
                CREATE TABLE Category (
                    Id INT IDENTITY(1,1) PRIMARY KEY,
                    Name NVARCHAR(50) NOT NULL
                )
            END");

        // Seed default categories if the table is empty
        db.Database.ExecuteSqlRaw(@"
            IF NOT EXISTS (SELECT TOP 1 1 FROM Category)
            BEGIN
                INSERT INTO Category (Name) VALUES ('Breakfast'), ('Lunch'), ('Dinner'), ('Dessert'), ('Snack'), ('Drink')
            END");

        // If Recipe still has the old Category string column, migrate data and replace with CategoryId
        if (db.Database.ExecuteSqlRaw("SELECT COUNT(*) FROM sys.columns WHERE object_id = OBJECT_ID('Recipe') AND name = 'Category'") > 0)
        {
            // Migrate existing string category values to the Category table and set CategoryId
            db.Database.ExecuteSqlRaw(@"
                IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Recipe') AND name = 'Category')
                BEGIN
                    -- Update recipes with known category values
                    UPDATE r SET r.CategoryId = c.Id 
                    FROM Recipe r 
                    INNER JOIN Category c ON r.Category = c.Name
                    WHERE r.Category IS NOT NULL AND r.Category <> ''

                    -- Drop the old Category column
                    ALTER TABLE Recipe DROP COLUMN Category

                    -- Add CategoryId column if it doesn't exist
                    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Recipe') AND name = 'CategoryId')
                    BEGIN
                        ALTER TABLE Recipe ADD CategoryId INT NOT NULL DEFAULT 1
                        ALTER TABLE Recipe ADD CONSTRAINT FK_Recipe_Category_CategoryId FOREIGN KEY (CategoryId) REFERENCES Category(Id)
                    END
                END
                ELSE
                BEGIN
                    -- Add CategoryId column if it doesn't exist
                    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Recipe') AND name = 'CategoryId')
                    BEGIN
                        ALTER TABLE Recipe ADD CategoryId INT NOT NULL DEFAULT 1
                        ALTER TABLE Recipe ADD CONSTRAINT FK_Recipe_Category_CategoryId FOREIGN KEY (CategoryId) REFERENCES Category(Id)
                    END
                END");
        }
        else if (db.Database.ExecuteSqlRaw("SELECT COUNT(*) FROM sys.columns WHERE object_id = OBJECT_ID('Recipe') AND name = 'CategoryId'") == 0)
        {
            // Category column doesn't exist, just add CategoryId
            db.Database.ExecuteSqlRaw(@"
                ALTER TABLE Recipe ADD CategoryId INT NOT NULL DEFAULT 1
                ALTER TABLE Recipe ADD CONSTRAINT FK_Recipe_Category_CategoryId FOREIGN KEY (CategoryId) REFERENCES Category(Id)");
        }
    }
    catch
    {
        // Ignore errors
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

    // Catch unhandled exceptions and redirect to the Error page
    app.UseExceptionHandler("/Home/Error");
}
else
{
    // In development, show detailed error pages
    app.UseDeveloperExceptionPage();
}

// Handle status codes like 404 by redirecting to a friendly page
app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();