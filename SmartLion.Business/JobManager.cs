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
    public class JobManager : Manager<JobManager>
    {
        public void AddJob(JobDomainModel DomainJob)
        {
            var AddJob = new Job
            {
                Name = DomainJob.Name,
                CreateDate = DateTime.Now
            };

            Ctx.Jobs.AddObject(AddJob);
            SaveChanges();
        }

        public void UpdateJob(JobDomainModel DomainJob)
        {
            var UpdateJob = Ctx.Jobs
                                .Where(x => x.Id == DomainJob.Id)
                                .FirstOrDefault();
            UpdateJob.Name = string.IsNullOrEmpty(DomainJob.Name) ? UpdateJob.Name : DomainJob.Name;
            UpdateJob.ModifiedDate = DateTime.Now;
            SaveChanges();
        }

        public void DeleteJob(int Id)
        {
            var DeleteJob = Ctx.Jobs
                            .Where(x => x.Id == Id)
                            .FirstOrDefault();

            Ctx.Jobs.DeleteObject(DeleteJob);
            SaveChanges();
        }

        public PagedList<JobDomainModel> GetJob(int PageIndex, int PageSize, string Search = null)
        {
            PagedList<JobDomainModel> JobPagedList = new PagedList<JobDomainModel>();

            var JobList = Context.Jobs
                                  .OrderBy(o => o.CreateDate);

            if (!string.IsNullOrEmpty(Search))
            {
                JobList = Context.Jobs
                                  .Where(x => x.Name.Contains(Search))
                                  .OrderBy(o => o.CreateDate);
            }

            var JobFetchList = JobList.ToPagedList(PageIndex, PageSize);

            JobPagedList.Count = JobFetchList.Count;
            JobPagedList.PageIndex = JobFetchList.PageIndex;
            JobPagedList.PageSize = JobFetchList.PageSize;
            JobPagedList.Result = JobFetchList.Result.Select(x => new JobDomainModel
            {
                Id = x.Id,
                Name = x.Name
            });

            return JobPagedList;
        }

        public JobDomainModel GetJobById(int Id)
        {
            return Context.Jobs.Where(x => x.Id == Id).Select(x => new JobDomainModel { Id = x.Id, Name = x.Name }).FirstOrDefault();
        }

        public List<JobDomainModel> GetJobForDropDown()
        {
            return Context.Jobs
                          .Select(x => new JobDomainModel { Id = x.Id, Name = x.Name })
                          .ToList();
        }
    }
}
