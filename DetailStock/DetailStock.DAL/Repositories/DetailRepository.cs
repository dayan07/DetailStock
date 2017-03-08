using DetailStock.DAL.EF;
using DetailStock.DAL.Entities;
using DetailStock.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace DetailStock.DAL.Repositories
{
    public class DetailRepository : IRepository<Detail>
    {
        private DetailContext db;

        public DetailRepository(DetailContext context)
        {
            this.db = context;
        }

        public IEnumerable<Detail> GetAll()
        {
         
          return db.Details.Include(o => o.Stockman);
        }

        public Detail Get(int id)
        {
            return db.Details.Find(id);
        }

        public void Create(Detail detail)
        {
            db.Details.Add(detail);
        }

        public void Update(Detail detail)
        {
            db.Entry(detail).State = EntityState.Modified;
            db.SaveChanges();
        }

        public Detail Delete(int id)
        {
            Detail dbEntry = db.Details.Find(id);
            if (dbEntry != null)
            {
                db.Details.Remove(dbEntry);
                db.SaveChanges();
            }
            return dbEntry;
        }
    }
}
