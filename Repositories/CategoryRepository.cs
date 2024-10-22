using BusinessObject;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

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
            _context.Categories.Add(c); // Sử dụng phương thức đồng bộ Add
            _context.SaveChanges();     // Sử dụng SaveChanges đồng bộ
        }

        public void UpdateCategory(Category c)
        {
            _context.Categories.Update(c); // Phương thức đồng bộ Update
            _context.SaveChanges();        // Sử dụng SaveChanges đồng bộ
        }

        public void DeleteCategory(Category c)
        {
            _context.Categories.Remove(c); // Phương thức đồng bộ Remove
            _context.SaveChanges();        // Sử dụng SaveChanges đồng bộ
        }

        public Category GetCategoryId(int id)
        {
            return _context.Categories.FirstOrDefault(c => c.Id == id); // Phương thức đồng bộ FirstOrDefault
        }
    }
}
