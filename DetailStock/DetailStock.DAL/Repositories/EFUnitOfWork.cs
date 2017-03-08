using DetailStock.DAL.EF;
using DetailStock.DAL.Entities;
using DetailStock.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetailStock.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private DetailContext db;
        private DetailRepository detailRepository;
        private StockmanRepository stockmanRepository;

        public EFUnitOfWork(string connectionString)
        {
            db = new DetailContext(connectionString);
        }
        public IRepository<Stockman> Stockmen
        {
            get
            {
                if (stockmanRepository == null)
                    stockmanRepository = new StockmanRepository(db);
                return stockmanRepository;
            }
        }

        public IRepository<Detail> Details
        {
            get
            {
                if (detailRepository == null)
                    detailRepository = new DetailRepository(db);
                return detailRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
