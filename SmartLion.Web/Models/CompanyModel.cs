using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartLion.Domain.Model;
using SmartLion.Core;
using System.ComponentModel.DataAnnotations;

namespace SmartLion.Web.Models
{
    public class CompanyModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You can't leave Company Name empty.")]
        public string Name { get; set; }

        public string Address { get; set; }

        public string ContactNo { get; set; }

        public string Email { get; set; }
    }
    public class IndexCompanyModel
    {
        public PagedList<CompanyDomainModel> CompanyList { get; set; }
    }
}