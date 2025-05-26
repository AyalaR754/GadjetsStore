using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ProductRepository : IProductRepository
    {
        GadjetsStoreDBContext _gadjetsStoreDBContext;

        public ProductRepository(GadjetsStoreDBContext gadjetsStoreDBContext)
        {
            _gadjetsStoreDBContext = gadjetsStoreDBContext;
        }
        public async Task<List<Product>> Get(string? name, int? minPrice, int? maxprice, int?[] categoryIds)

        {


            var query = _gadjetsStoreDBContext.Products.Where(product =>

            (name == null ? (true) : (product.Description.Contains(name)))
      && ((minPrice == null ? (true) : (product.Price >= minPrice)))
      && ((maxprice == null ? (true) : (product.Price <= maxprice)))
      && ((categoryIds.Length == 0 ? (true) : (categoryIds.Contains(product.CategoryId))))

            );

            List<Product> products = await query.ToListAsync();

            return products;

        }


    }
}
