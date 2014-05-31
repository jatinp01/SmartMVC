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
    public class StatusManager : Manager<StatusManager>
    {
        public void AddStatus(StatusDomainModel DomainStatus)
        {
            var AddStatus = new Status
            {
                Status1 = DomainStatus.Status,
                CreateDate = DateTime.Now
            };

            Ctx.Status.AddObject(AddStatus);
            SaveChanges();
        }

        public void UpdateStatus(StatusDomainModel DomainStatus)
        {
            var UpdateStatus = Ctx.Status
                                .Where(x => x.Id == DomainStatus.Id)
                                .FirstOrDefault();
            UpdateStatus.Status1 = string.IsNullOrEmpty(DomainStatus.Status) ? UpdateStatus.Status1 : DomainStatus.Status;
            UpdateStatus.ModifiedDate = DateTime.Now;
            SaveChanges();
        }

        public void DeleteStatus(int Id)
        {
            var DeleteStatus = Ctx.Status
                            .Where(x => x.Id == Id)
                            .FirstOrDefault();

            Ctx.Status.DeleteObject(DeleteStatus);
            SaveChanges();
        }

        public PagedList<StatusDomainModel> GetStatus(int PageIndex, int PageSize, string Search = null)
        {
            PagedList<StatusDomainModel> StatusPagedList = new PagedList<StatusDomainModel>();

            var StatusList = Context.Status
                                  .OrderBy(o => o.CreateDate);

            if (!string.IsNullOrEmpty(Search))
            {
                StatusList = Context.Status
                                  .Where(x => x.Status1.Contains(Search))
                                  .OrderBy(o => o.CreateDate);
            }

            var StatusFetchList = StatusList.ToPagedList(PageIndex, PageSize);

            StatusPagedList.Count = StatusFetchList.Count;
            StatusPagedList.PageIndex = StatusFetchList.PageIndex;
            StatusPagedList.PageSize = StatusFetchList.PageSize;
            StatusPagedList.Result = StatusFetchList.Result.Select(x => new StatusDomainModel
            {
                Id = x.Id,
                Status = x.Status1
            });

            return StatusPagedList;
        }

        public StatusDomainModel GetStatusById(int Id)
        {
            return Context.Status.Where(x => x.Id == Id).Select(x => new StatusDomainModel { Id = x.Id, Status = x.Status1 }).FirstOrDefault();
        }

        public List<StatusDomainModel> GetStatusForDropDown()
        {
            return Context.Status
                          .Select(x => new StatusDomainModel { Id = x.Id, Status = x.Status1 })
                          .ToList();
        }
    }
}
