using SkillExchange.DAL.Database;
using SkillExchange.DAL.Entities;
using SkillExchange.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillExchange.DAL.Repository
{
    public class FeedbackRepositary : IFeedback
    {
        private readonly AppDbContext _context;
        public FeedbackRepositary(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Feedback feedback)
        {
          _context.Feedbacks.Add(feedback);
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            var entity = _context.Feedbacks.Find(id);
            if (entity != null)
            {
                _context.Feedbacks.Remove(entity);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Feedback> GetAllByContent(int contentId)
        {
           return _context.Feedbacks.Where(f => f.ContentId == contentId).ToList();
        }

        public Feedback? GetById(int id)
        {
            return _context.Feedbacks.FirstOrDefault(f => f.Id == id);  
        }

        public void Update(Feedback feedback)
        {
          _context.Feedbacks.Update(feedback);
            _context.SaveChanges();
        }
    }
}
