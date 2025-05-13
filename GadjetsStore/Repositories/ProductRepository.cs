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

        GadjetsStoreContext _gadjetsStoreContext;
        public ProductRepository(GadjetsStoreContext gadjetsStoreContext)
        {
            _gadjetsStoreContext = gadjetsStoreContext;
        }

        public async Task<List<Product>> Get()
        {
            List<Product> products = await _gadjetsStoreContext.Products.ToListAsync();
            return products;
        }



    }
}
