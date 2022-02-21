﻿using ModelsLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository.IRepository
{
    public interface IProductRepository : DataLayer.Repository.IRepository.IRepository<Product>
    {
        void Update(Product obj);
        void Save();
    }
}