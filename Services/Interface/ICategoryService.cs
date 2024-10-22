using BusinessObject;
using System.Collections.Generic;

namespace ToDoList.Services
{
    public interface ICategoryService
    {
        List<Category> GetCategories();

        void SaveCategory(Category c);

        void UpdateCategory(Category c);

        void DeleteCategory(Category c);

        Category GetCategoryId(int id);
    }
}
