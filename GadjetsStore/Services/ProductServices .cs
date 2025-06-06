﻿using Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Services
{
    class ProductServices : IProductServices
    {

        private readonly IProductRepository _productRepository;
        public ProductServices(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<List<Product>> Get()
        {
            List<Product> products = await _productRepository.Get();
            return products;
        }









    }
}
