using BusinessObject;
using ToDoList.Repositories;
using System.Collections.Generic;
using ToDoList.Services;

namespace Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public void SaveCategory(Category c)
        {
            _repository.SaveCategory(c);  // Đồng bộ
        }

        public void UpdateCategory(Category c)
        {
            _repository.UpdateCategory(c);  // Đồng bộ
        }

        public void DeleteCategory(Category c)
        {
            _repository.DeleteCategory(c);  // Đồng bộ
        }

        public Category GetCategoryId(int id)
        {
            return _repository.GetCategoryId(id);  // Đồng bộ
        }

        public List<Category> GetCategories()
        {
            return _repository.GetCategories();  // Đồng bộ
        }
    }
}
