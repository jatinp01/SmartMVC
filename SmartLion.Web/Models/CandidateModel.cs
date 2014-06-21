using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartLion.Domain.Model;
using SmartLion.Core;
using System.Web.Mvc;
using SmartLion.Business;
using System.ComponentModel.DataAnnotations;

namespace SmartLion.Web.Models
{
    public class CandidateModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You can't leave Date empty.")]
        public string Date { get; set; }

        [Required(ErrorMessage = "You can't leave First Name empty.")]
        public string FirstName { get; set; }
        
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "You can't leave Last Name empty.")]
        public string LastName { get; set; }
        
        public string ContactNo { get; set; }

        [Required(ErrorMessage = "You can't leave Interview Date empty.")]
        public string InterviewDate { get; set; }
        
        public string JoiningDate { get; set; }
        
        public decimal? Salary { get; set; }

        [Required(ErrorMessage = "Please select Company.")]
        public int CompanyId { get; set; }
        public IEnumerable<SelectListItem> Companies
        {
            get { return new SelectList(CompanyManager.Instance.GetCompanyForDropDown(), "Id", "Name"); }
        }

        [Required(ErrorMessage = "Please select Job.")]
        public int JobId { get; set; }
        public IEnumerable<SelectListItem> Jobs
        {
            get { return new SelectList(JobManager.Instance.GetJobForDropDown(), "Id", "Name"); }
        }

        [Required(ErrorMessage = "Please select Reference.")]
        public int ReferenceId { get; set; }
        public IEnumerable<SelectListItem> References
        {
            get { return new SelectList(ReferenceManager.Instance.GetReferenceForDropDown(), "Id", "Name"); }
        }

        [Required(ErrorMessage = "Please select Status.")]
        public int StatusId { get; set; }
        public IEnumerable<SelectListItem> Status
        {
            get { return new SelectList(StatusManager.Instance.GetStatusForDropDown(), "Id", "Status"); }
        }
    }

    public class EditCandidateModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You can't leave Date empty.")]
        public string Date { get; set; }

        [Required(ErrorMessage = "You can't leave First Name empty.")]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [Required(ErrorMessage = "You can't leave Last Name empty.")]
        public string LastName { get; set; }

        public string ContactNo { get; set; }

        [Required(ErrorMessage = "You can't leave Interview Date empty.")]
        public string InterviewDate { get; set; }

        public string JoiningDate { get; set; }

        public decimal? Salary { get; set; }

        [Required(ErrorMessage = "Please select Company.")]
        public int CompanyId { get; set; }
        public IEnumerable<SelectListItem> Companies
        {
            get { return new SelectList(CompanyManager.Instance.GetCompanyForDropDown(), "Id", "Name"); }
        }

        [Required(ErrorMessage = "Please select Job.")]
        public int JobId { get; set; }
        public IEnumerable<SelectListItem> Jobs
        {
            get { return new SelectList(JobManager.Instance.GetJobForDropDown(), "Id", "Name"); }
        }

        [Required(ErrorMessage = "Please select Reference.")]
        public int ReferenceId { get; set; }
        public IEnumerable<SelectListItem> References
        {
            get { return new SelectList(ReferenceManager.Instance.GetReferenceForDropDown(), "Id", "Name"); }
        }
    }

    public class EditStatusCandidateModel 
    {
        public int Id { get; set; }

        public int StatusId { get; set; }
        public IEnumerable<SelectListItem> Status
        {
            get { return new SelectList(StatusManager.Instance.GetStatusForDropDown(), "Id", "Status"); }
        }
    }

    public class IndexCandidateModel
    {
        public PagedList<CandidateDomainModel> CandidateList { get; set; }
    }

}