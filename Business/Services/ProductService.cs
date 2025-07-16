using Business.Interfaces;
using DataAccess.Interfaces;
using Entities;
using System.Collections.Generic;
using System.Linq;

namespace Business.Services
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> _productRepository;

        public ProductService(IGenericRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public List<Product> GetAll()
        {
            return _productRepository.Find(p => p.Stock > 0); // Stokta olmayanları listeleme
        }

        public Product GetById(int id)
        {
            return _productRepository.GetById(id);
        }

        public List<Product> GetByCategoryId(int categoryId)
        {
            return _productRepository.Find(p => p.CategoryId == categoryId && p.Stock > 0);
        }

        public void Add(Product product)
        {
            _productRepository.Add(product);
            _productRepository.Save();
        }

        public void Update(Product product)
        {
            _productRepository.Update(product);
            _productRepository.Save();
        }

        public void Delete(Product product)
        {
            _productRepository.Delete(product);
            _productRepository.Save();
        }
    }
}
