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
    public class JobModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }

    public class IndexJobModel
    {
        public PagedList<JobDomainModel> JobList { get; set; }
    }
}