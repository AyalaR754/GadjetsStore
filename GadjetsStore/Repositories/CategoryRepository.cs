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

        GadjetsStoreContext _gadjetsStoreContext;
        public CategoryRepository(GadjetsStoreContext gadjetsStoreContext)
        {
            _gadjetsStoreContext = gadjetsStoreContext;
        }

        public async Task<List<Category>> Get()
        {
            List <Category> categorys = await _gadjetsStoreContext.Categories.ToListAsync();
            return categorys;
        }



    }
}
