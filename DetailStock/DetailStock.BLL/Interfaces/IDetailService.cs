using DetailStock.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetailStock.BLL.Interfaces
{
    public interface IDetailService
    {
        IEnumerable<DetailDTO> GetDetails();
        DetailDTO GetDetail(string Code);
        DetailDTO GetDetailById(int? id);
        void InsertDetail(DetailDTO detailDTO);
        void UpdateDetail(DetailDTO detailDTO);
        DetailDTO DeleteDetail(int id);
        void Dispose();
    }
}

