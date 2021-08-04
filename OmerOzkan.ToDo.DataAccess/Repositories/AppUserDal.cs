using Microsoft.EntityFrameworkCore;
using OmerOzkan.ToDo.DataAccess.Concrete.EfCore.Context;
using OmerOzkan.ToDo.DataAccess.Interfaces;
using OmerOzkan.ToDo.Entities.Domains;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OmerOzkan.ToDo.DataAccess.Repositories
{
    public class AppUserDal : GenericDal<AppUser>, IAppUserDal
    {
        private readonly ToDoContext _context;
        public AppUserDal(ToDoContext context) : base(context)
        {
            _context = context;
        }

        public List<AppUser> GetNonAdmins()
        {
            return _context.Users.Join(_context.UserRoles, user => user.Id, userRole => userRole.UserId, (resultUser, resultUserRole) => new
            {
                user = resultUser,
                userRole = resultUserRole
            }).Join(_context.Roles, twoTableResult => twoTableResult.userRole.RoleId, role => role.Id, (resultTable, resultRole) => new
            {
                user = resultTable.user,
                userRoles = resultTable.userRole,
                roles = resultRole
            }).Where(I => I.roles.Name == "Member").Select(I => new AppUser()
            {
                Id = I.user.Id,
                Name = I.user.Name,
                SurName = I.user.SurName,
                Picture = I.user.Picture,
                Email = I.user.Email,
                UserName = I.user.UserName
            }).ToList();
        }


        public List<AppUser> GetNonAdmins(out int toplamSayfa, string aranacakKelime, int aktifSayfa = 1)
        {
            var result = _context.Users.Join(_context.UserRoles, user => user.Id, userRole => userRole.UserId, (resultUser, resultUserRole) => new
            {
                user = resultUser,
                userRole = resultUserRole
            }).Join(_context.Roles, twoTableResult => twoTableResult.userRole.RoleId, role => role.Id, (resultTable, resultRole) => new
            {
                user = resultTable.user,
                userRoles = resultTable.userRole,
                roles = resultRole
            }).Where(I => I.roles.Name == "Member").Select(I => new AppUser()
            {
                Id = I.user.Id,
                Name = I.user.Name,
                SurName = I.user.SurName,
                Picture = I.user.Picture,
                Email = I.user.Email,
                UserName = I.user.UserName
            });

            toplamSayfa = (int)Math.Ceiling((double)result.Count() / 3);

            if (!string.IsNullOrWhiteSpace(aranacakKelime))
            {
                result = result.Where(I => I.Name.ToLower().Contains(aranacakKelime.ToLower()) || I.SurName.ToLower().Contains(aranacakKelime.ToLower()));
                toplamSayfa = (int)Math.Ceiling((double)result.Count() / 3);
            }

            result = result.Skip((aktifSayfa - 1) * 3).Take(3);

            return result.ToList();
        }

        public List<AppUserDutyInfo> GetMostCompleteDutyUsers()
        {
            return _context.Duties.Where(I => I.Status).GroupBy(I => I.AppUserId).OrderByDescending(I => I.Count()).Take(5).Select(I => new AppUserDutyInfo
            {
                Name = I.Key,
                DutyCount = I.Count()
            }).ToList();
        }

        public List<AppUserDutyInfo> GetMostEmployedUsers()
        {
            return _context.Duties.Where(I => !I.Status && I.AppUserId != null).GroupBy(I => I.AppUserId).OrderByDescending(I => I.Count()).Take(5).Select(I => new AppUserDutyInfo
            {
                Name = I.Key,
                DutyCount = I.Count()
            }).ToList();
        }
    }
}

