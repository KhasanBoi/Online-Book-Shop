using DataLayer.Data;
using DataLayer.Repository.IRepository;
using ModelsLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
    public class CoverTypeRepository : Repository<CoverType>, ICoverTypeRepository 
    {
        private ApplicationDbContext db;
        public CoverTypeRepository(ApplicationDbContext _db) : base(_db)
        {
            db = _db;
        }
        public void Save()
        {
            db.SaveChanges();
        }
        public void Update(CoverType obj)
        {
            db.CoverType.Update(obj);
        }
    }
}
