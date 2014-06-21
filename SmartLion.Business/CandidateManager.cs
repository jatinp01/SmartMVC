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
    public class CandidateManager : Manager<CandidateManager>
    {
        public void AddCandidate(CandidateDomainModel DomainCandidate)
        {
            var AddCandidate = new Candidate
            {
                Date = DomainCandidate.Date,
                FirstName = DomainCandidate.FirstName,
                MiddleName = DomainCandidate.MiddleName,
                LastName = DomainCandidate.LastName,
                ContactNo = DomainCandidate.ContactNo,
                InterviewDate = DomainCandidate.InterviewDate,
                JoiningDate = DomainCandidate.JoiningDate,
                Salary = DomainCandidate.Salary,
                CompanyId = DomainCandidate.CompanyId,
                JobId = DomainCandidate.JobId,
                ReferenceId = DomainCandidate.ReferenceId,
                StatusId = DomainCandidate.StatusId,
                CreateDate = DateTime.Now,
                CreateUserId = DomainCandidate.CreateUserId
            };

            Ctx.Candidates.AddObject(AddCandidate);
            SaveChanges();
        }

        public void UpdateCandidate(CandidateDomainModel DomainCandidate)
        {
            var UpdateCandidate = Ctx.Candidates
                                .Where(x => x.Id == DomainCandidate.Id)
                                .FirstOrDefault();
            UpdateCandidate.Date = DomainCandidate.Date;
            UpdateCandidate.FirstName = string.IsNullOrEmpty(DomainCandidate.FirstName) ? UpdateCandidate.FirstName : DomainCandidate.FirstName;
            UpdateCandidate.MiddleName = string.IsNullOrEmpty(DomainCandidate.MiddleName) ? UpdateCandidate.MiddleName : DomainCandidate.MiddleName;
            UpdateCandidate.LastName = string.IsNullOrEmpty(DomainCandidate.LastName) ? UpdateCandidate.LastName : DomainCandidate.LastName;
            UpdateCandidate.ContactNo = string.IsNullOrEmpty(DomainCandidate.ContactNo) ? UpdateCandidate.ContactNo : DomainCandidate.ContactNo;
            UpdateCandidate.InterviewDate = DomainCandidate.InterviewDate;
            UpdateCandidate.JoiningDate = DomainCandidate.JoiningDate;
            UpdateCandidate.Salary = DomainCandidate.Salary;
            UpdateCandidate.CompanyId = DomainCandidate.CompanyId;
            UpdateCandidate.JobId = DomainCandidate.JobId;
            UpdateCandidate.ReferenceId = DomainCandidate.ReferenceId;
            UpdateCandidate.ModifiedDate = DateTime.Now;
            UpdateCandidate.ModifiedUserId = DomainCandidate.ModifiedUserId;
            SaveChanges();
        }

        public void UpdateCandidateStatus(int Id, int StatusId, int UserId)
        {
            var UpdateCandidate = Ctx.Candidates
                                           .Where(x => x.Id == Id)
                                           .FirstOrDefault();
            UpdateCandidate.StatusId = StatusId;
            UpdateCandidate.ModifiedDate = DateTime.Now;
            UpdateCandidate.ModifiedUserId = UserId;
            SaveChanges();
        }

        public void DeleteCandidate(int Id)
        {
            var DeleteCandidate = Ctx.Candidates
                            .Where(x => x.Id == Id)
                            .FirstOrDefault();

            Ctx.Candidates.DeleteObject(DeleteCandidate);
            SaveChanges();
        }

        public PagedList<CandidateDomainModel> GetCandidate(int PageIndex, int PageSize, int UserId, bool UserFilterOn, string Search = null)
        {
            PagedList<CandidateDomainModel> CandidatePagedList = new PagedList<CandidateDomainModel>();

            var CandidateList = Context.Candidates
                                  .OrderBy(o => o.CreateDate);

            if (UserFilterOn)
            {
                CandidateList = CandidateList
                                 .Where(x => x.CreateUserId == UserId)
                                 .OrderBy(o => o.CreateDate);
            }

            if (!string.IsNullOrEmpty(Search))
            {
                CandidateList = CandidateList
                                  .Where(x => x.FirstName.Contains(Search) || x.MiddleName.Contains(Search) ||
                                   x.ContactNo.Contains(Search) || x.LastName.Contains(Search))
                                  .OrderBy(o => o.CreateDate);
            }

            var CandidateFetchList = CandidateList.ToPagedList(PageIndex, PageSize);

            CandidatePagedList.Count = CandidateFetchList.Count;
            CandidatePagedList.PageIndex = CandidateFetchList.PageIndex;
            CandidatePagedList.PageSize = CandidateFetchList.PageSize;
            CandidatePagedList.Result = CandidateFetchList.Result.Select(x => new CandidateDomainModel
            {
                Id = x.Id,
                Date = x.Date,
                FirstName = x.FirstName,
                MiddleName = x.MiddleName,
                LastName = x.LastName,
                CandidateName = (!string.IsNullOrEmpty(x.MiddleName)) ? string.Format("{0} {1} {2}", x.FirstName, x.MiddleName, x.LastName) : string.Format("{0} {1}", x.FirstName, x.LastName),
                ContactNo = x.ContactNo,
                InterviewDate = x.InterviewDate,
                JoiningDate = x.JoiningDate,
                Salary = x.Salary,
                CompanyId = x.CompanyId,
                JobId = x.JobId,
                ReferenceId = x.ReferenceId,
                StatusId = x.StatusId,
                StatusName = x.Status.Status1,
                IsAdmin = !UserFilterOn
            });

            return CandidatePagedList;
        }

        public CandidateDomainModel GetCandidateById(int Id)
        {
            return Context.Candidates.Where(x => x.Id == Id).Select(x => new CandidateDomainModel
            {
                Id = x.Id,
                Date = x.Date,
                FirstName = x.FirstName,
                MiddleName = x.MiddleName,
                LastName = x.LastName,
                ContactNo = x.ContactNo,
                InterviewDate = x.InterviewDate,
                JoiningDate = x.JoiningDate,
                Salary = x.Salary,
                CompanyId = x.CompanyId,
                JobId = x.JobId,
                ReferenceId = x.ReferenceId,
                StatusId = x.StatusId
            }).FirstOrDefault();
        }
    }
}
