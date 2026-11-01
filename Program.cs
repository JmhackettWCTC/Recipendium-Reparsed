using Microsoft.EntityFrameworkCore;
using Recipendium_Reparsed.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<RecipeDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Ensure database is ready
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<RecipeDbContext>();
    try
    {
        // Ensure CategoryID column exists on Recipe table
        if (db.Database.ExecuteSqlRaw("SELECT COUNT(*) FROM sys.columns WHERE object_id = OBJECT_ID('Recipe') AND name = 'CategoryID'") == 0)
        {
            db.Database.ExecuteSqlRaw(@"
                IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Recipe') AND name = 'CategoryID')
                BEGIN
                    ALTER TABLE Recipe ADD CategoryID INT NOT NULL DEFAULT 1
                END");
        }

        // Ensure Description and Slug columns exist on Category table
        db.Database.ExecuteSqlRaw(@"
            IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Category') AND name = 'Description')
            BEGIN
                ALTER TABLE Category ADD Description NVARCHAR(200) NULL
            END");

        db.Database.ExecuteSqlRaw(@"
            IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Category') AND name = 'Slug')
            BEGIN
                ALTER TABLE Category ADD Slug NVARCHAR(50) NULL
            END");

        // Update any NULL values to empty strings
        db.Database.ExecuteSqlRaw("UPDATE Category SET Description = '' WHERE Description IS NULL");
        db.Database.ExecuteSqlRaw("UPDATE Category SET Slug = '' WHERE Slug IS NULL");

        // Ensure Featured column exists on Recipe table
        db.Database.ExecuteSqlRaw(@"
            IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Recipe') AND name = 'Featured')
            BEGIN
                ALTER TABLE Recipe ADD Featured BIT NOT NULL DEFAULT 0
            END");

        // Ensure SubRecipe table exists
        db.Database.ExecuteSqlRaw(@"
            IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'SubRecipe')
            BEGIN
                CREATE TABLE SubRecipe (
                    Id INT IDENTITY(1,1) PRIMARY KEY,
                    RecipeID INT NOT NULL,
                    Name NVARCHAR(100) NOT NULL,
                    Ingredients NVARCHAR(MAX) NOT NULL,
                    Instructions NVARCHAR(MAX) NOT NULL,
                    [Order] INT NOT NULL,
                    CONSTRAINT FK_SubRecipe_Recipe_RecipeID FOREIGN KEY (RecipeID) REFERENCES Recipe(Id) ON DELETE CASCADE
                )
                CREATE INDEX IX_SubRecipe_RecipeID ON SubRecipe(RecipeID)
            END");
    }
    catch
    {
        // Ignore migration errors
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