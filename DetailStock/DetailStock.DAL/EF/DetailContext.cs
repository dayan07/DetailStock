using DetailStock.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetailStock.DAL.EF
{
   public class DetailContext : DbContext
    {
        public DbSet<Detail> Details { get; set; }
        public DbSet<Stockman> Stockmen { get; set; }

        static DetailContext()
        {
            Database.SetInitializer<DetailContext>(new StockDbInitializer());
        }
        public DetailContext(string connectionString)
            : base(connectionString)
        {
        }
    }

    public class StockDbInitializer : DropCreateDatabaseIfModelChanges<DetailContext>
    {
        protected override void Seed(DetailContext db)
        {
            db.Stockmen.Add(new Stockman { StockmanName = "Петров Петр Петрович" });
            db.Stockmen.Add(new Stockman { StockmanName = "Иванов Иван Иванович" });
            db.Stockmen.Add(new Stockman { StockmanName = "Сидоров Илья Петрович" });
            db.SaveChanges();
        }
    }
}

