﻿using DAL.Entities;
using System;
using System.Data.Entity;

namespace DAL.EF
{
    public class DatabaseContext:DbContext
    {
        public DatabaseContext() : base("DefaultConnection")
        {
            Database.SetInitializer<DatabaseContext>(new StoreDbInitializer());
        }
        public DbSet<Electricity> Electricity { get; set; }
        public DbSet<Water> Water { get; set; }
        public DbSet<Gas> Gas { get; set; }
    }

    public class StoreDbInitializer : CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext db)
        {
            db.Electricity.Add(new Electricity() { StartValue = 1234, EndValue = 1456, PriceAmong100=300, PricePer100 = 100, Consumed = 222, Price = 400, Date = Convert.ToDateTime("12/06/18") });

            db.Water.Add(new Water() { CounterValue = 1020, Date = Convert.ToDateTime("12/06/18"), Price = 350 });
            db.SaveChanges();
        }
    }
}
