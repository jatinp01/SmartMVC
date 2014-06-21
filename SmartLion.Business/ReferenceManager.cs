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
    public class ReferenceManager : Manager<ReferenceManager>
    {
        public void AddReference(ReferenceDomainModel DomainReference)
        {
            var AddReference = new Reference
            {
                Name = DomainReference.Name,
                Address = DomainReference.Address,
                ContactNo = DomainReference.ContactNo,
                CreateDate = DateTime.Now
            };

            Ctx.References.AddObject(AddReference);
            SaveChanges();
        }

        public void UpdateReference(ReferenceDomainModel DomainReference)
        {
            var UpdateReference = Ctx.References
                                .Where(x => x.Id == DomainReference.Id)
                                .FirstOrDefault();
            UpdateReference.Name = string.IsNullOrEmpty(DomainReference.Name) ? UpdateReference.Name : DomainReference.Name;
            UpdateReference.Address = string.IsNullOrEmpty(DomainReference.Address) ? UpdateReference.Address : DomainReference.Address;
            UpdateReference.ContactNo = string.IsNullOrEmpty(DomainReference.ContactNo) ? UpdateReference.ContactNo : DomainReference.ContactNo;
            UpdateReference.ModifiedDate = DateTime.Now;
            SaveChanges();
        }

        public void DeleteReference(int Id)
        {
            var DeleteReference = Ctx.References
                            .Where(x => x.Id == Id)
                            .FirstOrDefault();

            Ctx.References.DeleteObject(DeleteReference);
            SaveChanges();
        }

        public PagedList<ReferenceDomainModel> GetReference(int PageIndex, int PageSize, string Search = null)
        {
            PagedList<ReferenceDomainModel> ReferencePagedList = new PagedList<ReferenceDomainModel>();

            var ReferenceList = Context.References
                                  .OrderBy(o => o.CreateDate);

            if (!string.IsNullOrEmpty(Search))
            {
                ReferenceList = ReferenceList
                                  .Where(x => x.Name.Contains(Search) || x.Address.Contains(Search) ||
                                   x.ContactNo.Contains(Search))
                                  .OrderBy(o => o.CreateDate);
            }

            var ReferenceFetchList = ReferenceList.ToPagedList(PageIndex, PageSize);

            ReferencePagedList.Count = ReferenceFetchList.Count;
            ReferencePagedList.PageIndex = ReferenceFetchList.PageIndex;
            ReferencePagedList.PageSize = ReferenceFetchList.PageSize;
            ReferencePagedList.Result = ReferenceFetchList.Result.Select(x => new ReferenceDomainModel
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address,
                ContactNo = x.ContactNo,
                Type = x.Type,
                TypeName = x.Type == 0 ? "Internal" : "External"
            });

            return ReferencePagedList;
        }

        public ReferenceDomainModel GetReferenceById(int Id)
        {
            return Context.References.Where(x => x.Id == Id).Select(x => new ReferenceDomainModel
                       {
                           Id = x.Id,
                           Name = x.Name,
                           Address = x.Address,
                           ContactNo = x.ContactNo
                       }).FirstOrDefault();
        }

        public List<ReferenceDomainModel> GetReferenceForDropDown()
        {
            return Context.References
                          .Select(x => new ReferenceDomainModel { Id = x.Id, Name = x.Name })
                          .ToList();
        }
    }
}
