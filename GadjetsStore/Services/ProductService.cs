using AutoMapper;
using Entities;

using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs;

namespace Services
{
    public class ProductsService : IProductsService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;


        public ProductsService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<List<ProductDTO>> Get(string? name, int? minPrice, int? maxPrice, int?[] categoryIds)
        {
            List<Product> products = await _productRepository.Get(name, minPrice, maxPrice, categoryIds);

            return _mapper.Map<List<Product>, List<ProductDTO>>(products);
        }
    }
}
