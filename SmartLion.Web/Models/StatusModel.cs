using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartLion.Core;
using SmartLion.Domain.Model;
using System.ComponentModel.DataAnnotations;

namespace SmartLion.Web.Models
{
    public class StatusModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You can't leave Status empty.")]
        public string Status { get; set; }
    }

    public class IndexStatusModel
    {
        public PagedList<StatusDomainModel> StatusList { get; set; }
    }
}