using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Xamarin.Essentials;
namespace MonyCore.Context
{
   public class Context:DbContext
    {
        public DbSet<Model.Many> Manies { get; set; }
        public DbSet<Model.Consumption> Consumptions { get; set; }
        public DbSet<Model.Incom> Incoms { get; set; }

        public Context()
        {
           
            SQLitePCL.Batteries_V2.Init();

            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "Data.db3");

            optionsBuilder
                .UseSqlite($"Filename={dbPath}");
        }


    }
}
