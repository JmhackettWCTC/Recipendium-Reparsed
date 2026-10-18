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
            modelBuilder.Entity<Recipe>(entity =>
            {
                entity.ToTable("Recipe");
                entity.Property(e => e.Name).HasColumnName("Title");
                entity.Property(e => e.Time).HasColumnName("CookTime")
                    .HasConversion((string v) => StringToIntConverter(v), (int v) => IntToStringConverter(v));
                entity.Property(e => e.Servings).HasColumnName("Servings")
                    .HasConversion((string v) => StringToIntConverter(v), (int v) => IntToStringConverter(v));
                entity.Property(e => e.Image).HasColumnName("ImagePath");
                entity.Ignore(e => e.Rating);
            });
        }

        private static int StringToIntConverter(string value)
        {
            return int.TryParse(value, out var result) ? result : 0;
        }

        private static string IntToStringConverter(int value)
        {
            return value.ToString();
        }
    }
}