using ModelsLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository.IRepository
{
    public interface ICoverTypeRepository : DataLayer.Repository.IRepository.IRepository<CoverType>
    {
        void Update(CoverType obj);
        void Save();
    }
}
