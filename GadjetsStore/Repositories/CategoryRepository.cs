using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    class CategoryRepository : ICategoryRepository
    {
        GadjetsStoreDBContext _gadjetsStoreDBContext;

        public CategoryRepository(GadjetsStoreDBContext gadjetsStoreDBContext)
        {
            _gadjetsStoreDBContext = gadjetsStoreDBContext;
        }
        public async Task<List<Category>> Get()
        {

            List<Category> Categories = await _gadjetsStoreDBContext.Categories.ToListAsync();
            return Categories;

        }
    }
}
