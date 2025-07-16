using Entities;
using System.Collections.Generic;

namespace Business.Interfaces
{
    public interface IProductService
    {
        List<Product> GetAll();
        Product GetById(int id);
        List<Product> GetByCategoryId(int categoryId);
        void Add(Product product);
        void Update(Product product);
        void Delete(Product product);
    }
}
