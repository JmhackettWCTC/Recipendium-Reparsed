// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.EntityFrameworkCore;
using Recipendium_Reparsed.Models;

namespace Recipendium_Reparsed.Data
{
    public class RecipeDbContext : DbContext
    {
        public DbSet<Recipe> Recipes { get; set; }

        public RecipeDbContext(DbContextOptions<RecipeDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Recipe>().HasData(
                new Recipe
                {
                    Id = 1,
                    Name = "Classic Margherita Pizza",
                    Category = "Italian",
                    Time = "30 mins",
                    Servings = "2",
                    Ingredients = "Pizza dough, tomato sauce, fresh mozzarella, basil leaves, olive oil",
                    Instructions = "Preheat oven to 450°F|Roll out dough|Spread tomato sauce|Add mozzarella slices|Bake for 12-15 mins|Top with fresh basil and olive oil",
                    Image = "margherita.jpg",
                    ShortDescription = "A simple yet delicious Italian classic.",
                    LongDescription = "The perfect Margherita pizza featuring fresh basil and creamy mozzarella on a crispy crust.",
                    Rating = 4.8
                },
                new Recipe
                {
                    Id = 2,
                    Name = "Creamy Mushroom Pasta",
                    Category = "Vegetarian",
                    Time = "20 mins",
                    Servings = "4",
                    Ingredients = "Fettuccine, mixed mushrooms, garlic, heavy cream, parmesan cheese, parsley",
                    Instructions = "Boil pasta|Sauté garlic and mushrooms in butter|Add cream and simmer|Toss pasta with sauce and parmesan",
                    Image = "mushroom_pasta.jpg",
                    ShortDescription = "Rich and velvety mushroom sauce.",
                    LongDescription = "An indulgent pasta dish made with sautéed mushrooms and a rich garlic cream sauce.",
                    Rating = 4.5
                },
                new Recipe
                {
                    Id = 3,
                    Name = "Spicy Chicken Tacos",
                    Category = "Mexican",
                    Time = "25 mins",
                    Servings = "3",
                    Ingredients = "Chicken breast, taco seasoning, corn tortillas, salsa, lime, avocado",
                    Instructions = "Cook seasoned chicken in a pan|Warm tortillas|Assemble tacos with chicken, salsa, and avocado slices",
                    Image = "chicken_tacos.jpg",
                    ShortDescription = "Quick and zesty street-style tacos.",
                    LongDescription = "Flavorful chicken tacos seasoned to perfection and topped with fresh avocado.",
                    Rating = 4.7
                },
                new Recipe
                {
                    Id = 4,
                    Name = "Fresh Greek Salad",
                    Category = "Salad",
                    Time = "15 mins",
                    Servings = "2",
                    Ingredients = "Cucumber, tomatoes, feta cheese, kalamata olives, red onion, olive oil, oregano",
                    Instructions = "Chop vegetables|Combine in a bowl|Add olives and feta|Drizzle with oil and sprinkle oregano.",
                    Image = "greek_salad.jpg",
                    ShortDescription = "A refreshing Mediterranean staple.",
                    LongDescription = "A crisp and healthy salad loaded with fresh vegetables and tangy feta cheese.",
                    Rating = 4.2
                },
                new Recipe
                {
                    Id = 5,
                    Name = "Beef Stir-Fry",
                    Category = "Asian",
                    Time = "15 mins",
                    Servings = "4",
                    Ingredients = "Beef strips, broccoli, soy sauce, ginger, garlic, sesame oil, cornstarch",
                    Instructions = "Sear beef in a hot pan|Add broccoli and aromatics|Pour in soy sauce mixture|Stir until thickened",
                    Image = "beef_stirfry.jpg",
                    ShortDescription = "Savory beef and broccoli in minutes.",
                    LongDescription = "A high-heat stir-fry with tender beef and crunchy broccoli in a savory soy glaze.",
                    Rating = 4.6
                },
                new Recipe
                {
                    Id = 6,
                    Name = "Chocolate Lava Cake",
                    Category = "Dessert",
                    Time = "20 mins",
                    Servings = "2",
                    Ingredients = "Dark chocolate, butter, eggs, sugar, flour, vanilla extract",
                    Instructions = "Melt chocolate and butter|Whisk eggs and sugar|Combine and fold in flour|Bake in ramekins for 12 mins",
                    Image = "lava_cake.jpg",
                    ShortDescription = "Decadent molten chocolate center.",
                    LongDescription = "The ultimate dessert for chocolate lovers, featuring a warm, gooey center.",
                    Rating = 4.9
                }
            );
        }
    }
}