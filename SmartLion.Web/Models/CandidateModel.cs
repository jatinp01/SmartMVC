using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartLion.Domain.Model;
using SmartLion.Core;
using System.Web.Mvc;
using SmartLion.Business;

namespace SmartLion.Web.Models
{
    public class CandidateModel
    {
        public int Id { get; set; }
        
        public string Date { get; set; }
        
        public string FirstName { get; set; }
        
        public string MiddleName { get; set; }
        
        public string LastName { get; set; }
        
        public string ContactNo { get; set; }
        
        public string InterviewDate { get; set; }
        
        public string JoiningDate { get; set; }
        
        public decimal? Salary { get; set; }
        
        public int CompanyId { get; set; }
        public IEnumerable<SelectListItem> Companies
        {
            get { return new SelectList(CompanyManager.Instance.GetCompanyForDropDown(), "Id", "Name"); }
        }
        
        public int JobId { get; set; }
        public IEnumerable<SelectListItem> Jobs
        {
            get { return new SelectList(JobManager.Instance.GetJobForDropDown(), "Id", "Name"); }
        }

        public int ReferenceId { get; set; }
        public IEnumerable<SelectListItem> References
        {
            get { return new SelectList(ReferenceManager.Instance.GetReferenceForDropDown(), "Id", "Name"); }
        }

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