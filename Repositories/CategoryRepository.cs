using BusinessObject;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Repositories
{
    public class CategoryRepository: ICategoryRepository
    {

        private readonly TaskManagementContext _context;
        public CategoryRepository(TaskManagementContext context)
        {
            _context = context;
        }

        public List<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public void SaveCategory(Category c)
        {
            _context.Categories.Add(c);
            _context.SaveChanges();
        }

        public void UpdateCategory(Category c)
        {
            _context.Categories.Update(c);
            _context.SaveChanges();
        }

        public void DeleteCategory(Category c)
        {
            _context.Categories.Remove(c);
            _context.SaveChanges();
        }

        public Category GetCategoryId(int id)
        {
            return _context.Categories.FirstOrDefault(c => c.Id == id);
        }
    }
}
