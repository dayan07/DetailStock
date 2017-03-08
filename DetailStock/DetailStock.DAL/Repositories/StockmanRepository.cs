using DetailStock.DAL.EF;
using DetailStock.DAL.Entities;
using DetailStock.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Description;

namespace DetailStock.DAL.Repositories
{
    public class StockmanRepository : IRepository<Stockman>
    {
        private DetailContext db;

        public StockmanRepository(DetailContext context)
        {
            this.db = context;
        }

        public IEnumerable<Stockman> GetAll()
        {
            return db.Stockmen.Include(t => t.Details);
        }

        public Stockman Get(int id)
        {
            return db.Stockmen.Include(t => t.Details).FirstOrDefault(t => t.StockmanId == id);

        }

        public void Create(Stockman stockman)
        {
            db.Stockmen.Add(stockman);
        }

        public void Update(Stockman stockman)
        {
            db.Entry(stockman).State = EntityState.Modified;
            db.SaveChanges();
        }
      public Stockman Delete(int id)
        {
            Stockman dbEntry = db.Stockmen.Find(id);
            if (dbEntry != null)
            {
                    db.Stockmen.Remove(dbEntry);
                    db.SaveChanges();
            }
            db.Entry(dbEntry).State = EntityState.Deleted;
            return dbEntry;
        }
    }
}

