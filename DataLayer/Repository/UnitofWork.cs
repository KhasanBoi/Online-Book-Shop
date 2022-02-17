using DataLayer.Data;
using DataLayer.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
    public class UnitofWork : IUnitofWork
    {
        private ApplicationDbContext db;
        public ICategoryRepository Category { get; private set; }
        public ICoverTypeRepository CoverType { get; private set; }

        public UnitofWork(ApplicationDbContext _db)
        {
            db = _db;
            Category = new CategoryRepository(db);
            CoverType = new CoverTypeRepository(db);
        }
        

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
