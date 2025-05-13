using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    class ProductRepository : IProductRepository
    {
        GadjetsStoreDBContext _gadjetsStoreDBContext;

        public ProductRepository(GadjetsStoreDBContext gadjetsStoreDBContext)
        {
            _gadjetsStoreDBContext = gadjetsStoreDBContext;
        }
        public async Task<List<Product>> Get()
        {

            List<Product> Products = await _gadjetsStoreDBContext.Products.ToListAsync();
            return Products;

        }
    }
}
