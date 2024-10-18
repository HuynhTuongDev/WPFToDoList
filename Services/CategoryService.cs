using BusinessObject;
using Microsoft.EntityFrameworkCore;
using ToDoList.Repositories;
using ToDoList.Services;

namespace Services
{
    public class CategoryService: ICategoryService
    {

        private readonly ICategoryRepository _repository;
        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public CategoryService()
        {
        }

        public void SaveCategory(Category c) => _repository.SaveCategory(c);

        public void UpdateCategory(Category c)=> _repository.UpdateCategory(c);

        public void DeleteCategory(Category c)=> _repository.DeleteCategory(c);

        public Category GetCategoryId(int id)=> _repository.GetCategoryId(id);

        public List<Category> GetCategories()=> _repository.GetCategories();
    }
}
