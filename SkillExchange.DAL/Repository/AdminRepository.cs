using Microsoft.EntityFrameworkCore;
using SkillExchange.Core.Entities;
using SkillExchange.DAL.Interfaces;
using SkillExchange.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillExchange.DAL.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly AppDbContext _dbContext;
        public AdminRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Content>> GetPendingContentAsync()
        {
            return await _dbContext.Contents
                    .Include(c => c.Category) 
                    .Where(c => !c.IsApproved && !c.IsRejected)
                    .ToListAsync();
        }
        public async Task<Content?> GetContentByIdAsync(int id)
        {
            return await _dbContext.Contents
                .Include(c => c.Feedbacks)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task UpdateContentAsync(Content content)
        {
            _dbContext.Contents.Update(content);
            await _dbContext.SaveChangesAsync();
        }
        public async  Task AddContentAsync(Content content)
        {
            await _dbContext.Contents.AddAsync(content);
            await _dbContext.SaveChangesAsync();
        }
    }
}
