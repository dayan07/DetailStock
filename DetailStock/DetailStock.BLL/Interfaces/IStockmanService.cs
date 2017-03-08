using DetailStock.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetailStock.BLL.Interfaces
{
    public interface IStockmanService
    {
        IEnumerable<StockmanDTO> GetStockmen();
       StockmanDTO GetStockmanById(int? id);
        void InsertStockman(StockmanDTO stockmanDTO);
        void UpdateStockman(StockmanDTO stockmanDTO);
        StockmanDTO DeleteStockman(int id);
        void Dispose();
    }
}
