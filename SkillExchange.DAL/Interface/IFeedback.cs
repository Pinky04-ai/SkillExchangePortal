using SkillExchange.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillExchange.DAL.Interface
{
    public interface IFeedback
    {
        Feedback? GetById(int id);
        IEnumerable<Feedback> GetAllByContent(int contentId);
        void Add(Feedback feedback);
        void Update(Feedback feedback);
        void Delete(int id);

    }
}
