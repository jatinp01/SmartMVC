using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SmartLion.Web.Models
{
    public class AccountModel
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
    }
}