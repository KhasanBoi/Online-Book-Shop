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
    public class ProductRepository : Repository<Product>, IProductRepository 
    {
        private ApplicationDbContext db;
        public ProductRepository(ApplicationDbContext _db) : base(_db)
        {
            db = _db;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Product obj)
        {
            var objFromdb = db.Products.FirstOrDefault(u => u.Id == obj.Id);
            if(objFromdb != null)
            {
                objFromdb.Title = obj.Title;
                objFromdb.Description = obj.Description;
                objFromdb.CoverTypeId = obj.CoverTypeId;
                objFromdb.Price100 = obj.Price100;
                objFromdb.ListPrice = obj.ListPrice;
                objFromdb.Price50 = obj.Price50;
                objFromdb.Isbn = obj.Isbn;
                objFromdb.Price = obj.Price;
                objFromdb.CategoryId = obj.CategoryId;
                objFromdb.Author = obj.Author;
                if(objFromdb.ImageUrl != null)
                {
                    objFromdb.ImageUrl = obj.ImageUrl;
                }
            }
        }
    }
}
