using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartLion.Operation.Manager;
using SmartLion.Domain.Model;
using SmartLion.Model;
using SmartLion.Core;

namespace SmartLion.Business
{
    public class UserManager : Manager<UserManager>
    {
        public void AddUser(UserDomainModel DomainUser)
        {
            var AddUser = new User
            {
                UserName = DomainUser.UserName,
                Password = DomainUser.Password,
                FirstName = DomainUser.FirstName,
                MiddleName = DomainUser.MiddleName,
                LastName = DomainUser.LastName,
                Status = DomainUser.Status,
                RoleId = DomainUser.RoleId,
                CreateDate = DateTime.Now
            };

            Ctx.Users.AddObject(AddUser);
            SaveChanges();
        }

        public void UpdateUser(UserDomainModel DomainUser)
        {
            var UpdateUser = Ctx.Users
                                .Where(x => x.Id == DomainUser.Id)
                                .FirstOrDefault();
            UpdateUser.UserName = string.IsNullOrEmpty(DomainUser.UserName) ? UpdateUser.UserName : DomainUser.UserName;
            UpdateUser.Password = string.IsNullOrEmpty(DomainUser.Password) ? UpdateUser.Password : DomainUser.Password;
            UpdateUser.FirstName = string.IsNullOrEmpty(DomainUser.FirstName) ? UpdateUser.FirstName : DomainUser.FirstName;
            UpdateUser.MiddleName = string.IsNullOrEmpty(DomainUser.MiddleName) ? UpdateUser.MiddleName : DomainUser.MiddleName;
            UpdateUser.LastName = string.IsNullOrEmpty(DomainUser.LastName) ? UpdateUser.LastName : DomainUser.LastName;
            UpdateUser.Status = (DomainUser.Status == 0) ? UpdateUser.Status : DomainUser.Status;
            UpdateUser.RoleId = (DomainUser.RoleId == 0) ? UpdateUser.RoleId : DomainUser.RoleId;
            SaveChanges();
        }

        public void DeleteUser(int Id)
        {
            var DeleteUser = Ctx.Users
                            .Where(x => x.Id == Id)
                            .FirstOrDefault();

            Ctx.Users.DeleteObject(DeleteUser);
            SaveChanges();
        }

        public int Login(UserDomainModel DomainUser)
        {
            return Ctx.Users
                      .Where(x => x.UserName.ToLower() == DomainUser.UserName.ToLower() &&
                      x.Password.ToLower() == DomainUser.Password.ToLower())
                      .Select(x => x.Id)
                      .FirstOrDefault();
        }

        public PagedList<UserDomainModel> GetUsers(int PageIndex, int PageSize, string Search = null)
        {
            PagedList<UserDomainModel> UserPagedList = new PagedList<UserDomainModel>();

            var UserList = Context.Users
                                  .Include("Role")
                                  .OrderBy(o => o.CreateDate);

            if (!string.IsNullOrEmpty(Search))
            {
                UserList = UserList
                                  .Where(x => x.UserName.Contains(Search) || x.FirstName.Contains(Search) ||
                                   x.MiddleName.Contains(Search) || x.LastName.Contains(Search))
                                  .OrderBy(o => o.CreateDate);
            }

            var UsersFetchList = UserList.ToPagedList(PageIndex, PageSize);

            UserPagedList.Count = UsersFetchList.Count;
            UserPagedList.PageIndex = UsersFetchList.PageIndex;
            UserPagedList.PageSize = UsersFetchList.PageSize;
            UserPagedList.Result = UsersFetchList.Result.Select(x => new UserDomainModel
            {
                Id = x.Id,
                UserName = x.UserName,
                FullName = (!string.IsNullOrEmpty(x.MiddleName)) ? string.Format("{0} {1} {2}", x.FirstName, x.MiddleName, x.LastName) : string.Format("{0} {1}", x.FirstName, x.LastName),
                RoleName = x.Role.Name,
                StatusName = x.Status == 0 ? "InActive" : "Active"
            });

            return UserPagedList;
        }

        public UserDomainModel GetUserById(int Id)
        {
            return Context.Users
                          .Include("Role")
                          .Where(x => x.Id == Id)
                          .Select(s => new UserDomainModel
                          {
                              Id = s.Id,
                              UserName = s.UserName,
                              Password = s.Password,
                              FirstName = s.FirstName,
                              MiddleName = s.MiddleName,
                              LastName = s.LastName,
                              Status = s.Status,
                              RoleId = s.RoleId
                          }).FirstOrDefault();
        }

        public List<RoleModel> GetRoleForDropDown()
        {
            return Context.Roles
                          .Select(x => new RoleModel { Id = x.Id, Name = x.Name })
                          .ToList();
        }

        public string GetUserRole(string UserName)
        {
            return Context.Users
                .Where(x => x.UserName == UserName)
                .Select(w => w.Role.Name)
                .FirstOrDefault();
        }

        public bool UserExist(string UserName)
        {
            return Context.Users
                .Where(x => x.UserName == UserName)
                .Any();
        }
    }
}
