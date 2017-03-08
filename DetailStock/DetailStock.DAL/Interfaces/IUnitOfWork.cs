using DetailStock.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetailStock.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Detail> Details { get; }
        IRepository<Stockman> Stockmen { get; }
        void Save();
    }
}
