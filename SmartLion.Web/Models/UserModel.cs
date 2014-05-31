using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;
using System.Web.Security;
using SmartLion.Domain.Model;
using SmartLion.Business;
using SmartLion.Core;

namespace SmartLion.Web.Models
{
    public class AddUserModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You can't leave User Name empty.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "You can't leave Password empty.")]  
        public string Password { get; set; }

        [Required(ErrorMessage = "You can't leave First Name empty.")]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [Required(ErrorMessage = "You can't leave Last Name empty.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please select User Role.")]
        public int? RoleId { get; set; }
        public IEnumerable<SelectListItem> Roles
        {
            get { return new SelectList(UserManager.Instance.GetRoleForDropDown(), "Id", "Name"); }
        }

        public int Status { get; set; }
    }

    public class EditUserModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You can't leave User Name empty.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "You can't leave First Name empty.")]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [Required(ErrorMessage = "You can't leave Last Name empty.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please select User Role.")]
        public int? RoleId { get; set; }
        public IEnumerable<SelectListItem> Roles
        {
            get { return new SelectList(UserManager.Instance.GetRoleForDropDown(), "Id", "Name"); }
        }

        public int Status { get; set; }
    }

    public class IndexUserModel
    {
        public PagedList<UserDomainModel> UserList { get; set; }
    }
}