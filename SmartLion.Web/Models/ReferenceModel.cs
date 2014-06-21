using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartLion.Domain.Model;
using SmartLion.Core;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SmartLion.Web.Models
{
    public class ReferenceModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You can't leave Reference Name empty.")]
        public string Name { get; set; }

        public string Address { get; set; }

        public string ContactNo { get; set; }

        [Required(ErrorMessage = "Please select Reference Type.")]
        public int? Type { get; set; }
        public IEnumerable<SelectListItem> ReferenceTypes
        {
            get { return new SelectList(new List<ReferenceTypeDomainModel>() { new ReferenceTypeDomainModel() { Id = 0, Name = "Internal" }, new ReferenceTypeDomainModel() { Id = 1, Name = "External" } }, "Id", "Name"); }
        }
    }
    public class IndexReferenceModel
    {
        public PagedList<ReferenceDomainModel> ReferenceList { get; set; }
    }
}