using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
