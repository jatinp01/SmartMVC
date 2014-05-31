using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartLion.Domain.Model
{
    public class CandidateDomainModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string CandidateName { get; set; }
        public string ContactNo { get; set; }
        public DateTime InterviewDate { get; set; }
        public DateTime? JoiningDate { get; set; }
        public decimal? Salary { get; set; }
        public int CompanyId { get; set; }
        public int JobId { get; set; }
        public int ReferenceId { get; set; }
        public int StatusId { get; set; }
    }
}
