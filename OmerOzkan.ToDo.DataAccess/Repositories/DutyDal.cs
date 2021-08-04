using Microsoft.EntityFrameworkCore;
using OmerOzkan.ToDo.DataAccess.Concrete.EfCore.Context;
using OmerOzkan.ToDo.DataAccess.Interfaces;
using OmerOzkan.ToDo.Entities.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace OmerOzkan.ToDo.DataAccess.Repositories
{
    public class DutyDal : GenericDal<Duty>, IDutyDal
    {
        private ToDoContext _context;
        public DutyDal(ToDoContext context) : base(context)
        {
            _context = context;
        }     

        public List<Duty> GetAll()
        {
            return _context.Duties.Include(I => I.Urgency).Include(I => I.Reports).Where(I => !I.Status).OrderByDescending(I => I.CreatedDate).ToList();
        }

        public List<Duty> GetAll(Expression<Func<Duty, bool>> filter)
        {
            return _context.Duties.Include(I => I.Urgency).Include(I => I.Reports).Where(filter).OrderByDescending(I => I.CreatedDate).ToList();
        }

        public List<Duty> GetAllByIncomplete(out int totalPage, string userId, int activePage = 1)
        {
            var returnValue = _context.Duties.Include(I => I.Urgency).Include(I => I.Reports).Where(I => I.AppUserId == userId && I.Status).OrderByDescending(I => I.CreatedDate);

            totalPage = (int)Math.Ceiling((double)returnValue.Count() / 3);
            return returnValue.Skip((activePage - 1) * 3).Take(3).ToList();
        }

        public List<Duty> GetByAppUserId(string appUserId)
        {
            return _context.Duties.Where(I => I.AppUserId == appUserId).ToList();
        }

        public List<Duty> GetByIncompleteWithUrgency()
        {
            return _context.Duties.Include(I => I.Urgency).Where(I => !I.Status).OrderByDescending(I => I.CreatedDate).ToList();
        }

        public Duty GetByReportId(int id)
        {
            return _context.Duties.Include(I => I.Reports).Where(I => I.Id == id).FirstOrDefault();
        }

        public Duty GetByUrgencyId(int id)
        {
            return _context.Duties.Include(I => I.Urgency).FirstOrDefault(I => !I.Status && I.Id == id);
        }

        public int GetDutyCountCompleteByAppUserId(string id)
        {
            return _context.Duties.Count(I => I.AppUserId == id && I.Status);
        }

        public int GetDutyCountCompleted()
        {
            return _context.Duties.Count(I => I.Status);
        }

        public int GetDutyCountPendingAssignment()
        {
            return _context.Duties.Count(I => I.AppUserId == null && !I.Status);
        }

        public int GetDutyCountToBeCompletedByAppUserId(string id)
        {
            return _context.Duties.Count(I => I.AppUserId == id && !I.Status);
        }
    }
}
