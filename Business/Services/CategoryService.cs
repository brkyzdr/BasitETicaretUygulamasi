using Business.Interfaces;
using DataAccess.Interfaces;
using Entities;
using System.Collections.Generic;

namespace Business.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _categoryRepository;

        public CategoryService(IGenericRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public List<Category> GetAll()
        {
            return _categoryRepository.GetAll();
        }

        public Category GetById(int id)
        {
            return _categoryRepository.GetById(id);
        }

        public void Add(Category category)
        {
            _categoryRepository.Add(category);
            _categoryRepository.Save();
        }

        public void Update(Category category)
        {
            _categoryRepository.Update(category);
            _categoryRepository.Save();
        }

        public void Delete(Category category)
        {
            _categoryRepository.Delete(category);
            _categoryRepository.Save();
        }
    }
}
