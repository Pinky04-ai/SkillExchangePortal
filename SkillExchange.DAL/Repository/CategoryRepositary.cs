using SkillExchange.DAL.Database;
using SkillExchange.DAL.Entities;
using SkillExchange.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillExchange.DAL.Repository
{
    public class CategoryRepositary : ICategory
    {
        private readonly AppDbContext _context;
        public CategoryRepositary(AppDbContext context)
        {
            _context = context;
        }
        public void Add(Category entity)
        {
            _context.Categories.Add(entity);
            _context.SaveChanges();
        }
        public void Delete (int id)
        {
            var category = _context.Categories.Find(id);
            if(category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
        }
        public void Update(Category entity)
        {
            _context.Categories.Update(entity);
            _context.SaveChanges();
        }
        public IEnumerable<Category> GetAll()
        {
            return _context.Categories.Include(c => c.Contents).ToList();
        }
        public Category GetById(int id)
        {
            return _context.Categories.Include(c => c.Contents).FirstOrDefault(c => c.Id == id);
        }
        public Category? GetByName(string name)
        {
            return _context.Categories.FirstOrDefault(c => c.Name == name);
        }
    }
}
