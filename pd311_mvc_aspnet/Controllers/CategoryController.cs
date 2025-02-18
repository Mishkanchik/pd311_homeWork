using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using pd311_mvc_aspnet.Data;
using pd311_mvc_aspnet.Models;

namespace pd311_mvc_aspnet.Repositories.Categories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Category> _categories;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
            _categories = _context.Set<Category>();
        }

        public async Task<bool> CreateAsync(Category model)
        {
            await _categories.AddAsync(model);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Category model)
        {
            _categories.Update(model);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var model = await FindByIdAsync(id);
            if (model == null) return false;
            _categories.Remove(model);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _categories.ToListAsync();
        }

        public async Task<Category?> FindByIdAsync(string id)
        {
            return await _categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category?> FindByNameAsync(string name)
        {
            return await _categories.FirstOrDefaultAsync(c => c.Name == name);
        }
    }
}
