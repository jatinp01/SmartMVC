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
    public class CompanyManager : Manager<CompanyManager>
    {
        public void AddCompany(CompanyDomainModel DomainCompany)
        {
            var AddCompany = new Company
            {
                Name = DomainCompany.Name,
                Address = DomainCompany.Address,
                ContactNo = DomainCompany.ContactNo,
                Email = DomainCompany.Email,
                CreateDate = DateTime.Now
            };

            Ctx.Companies.AddObject(AddCompany);
            SaveChanges();
        }

        public void UpdateCompany(CompanyDomainModel DomainCompany)
        {
            var UpdateCompany = Ctx.Companies
                                .Where(x => x.Id == DomainCompany.Id)
                                .FirstOrDefault();
            UpdateCompany.Name = string.IsNullOrEmpty(DomainCompany.Name) ? UpdateCompany.Name : DomainCompany.Name;
            UpdateCompany.Address = string.IsNullOrEmpty(DomainCompany.Address) ? UpdateCompany.Address : DomainCompany.Address;
            UpdateCompany.ContactNo = string.IsNullOrEmpty(DomainCompany.ContactNo) ? UpdateCompany.ContactNo : DomainCompany.ContactNo;
            UpdateCompany.Email = string.IsNullOrEmpty(DomainCompany.Email) ? UpdateCompany.Email : DomainCompany.Email;
            UpdateCompany.ModifiedDate = DateTime.Now;
            SaveChanges();
        }

        public void DeleteCompany(int Id)
        {
            var DeleteCompany = Ctx.Companies
                            .Where(x => x.Id == Id)
                            .FirstOrDefault();

            Ctx.Companies.DeleteObject(DeleteCompany);
            SaveChanges();
        }

        public PagedList<CompanyDomainModel> GetCompany(int PageIndex, int PageSize, string Search = null)
        {
            PagedList<CompanyDomainModel> CompanyPagedList = new PagedList<CompanyDomainModel>();

            var CompanyList = Context.Companies
                                  .OrderBy(o => o.CreateDate);

            if (!string.IsNullOrEmpty(Search))
            {
                CompanyList = CompanyList
                                  .Where(x => x.Name.Contains(Search) || x.Address.Contains(Search) ||
                                   x.ContactNo.Contains(Search) || x.Email.Contains(Search))
                                  .OrderBy(o => o.CreateDate);
            }

            var CompanyFetchList = CompanyList.ToPagedList(PageIndex, PageSize);

            CompanyPagedList.Count = CompanyFetchList.Count;
            CompanyPagedList.PageIndex = CompanyFetchList.PageIndex;
            CompanyPagedList.PageSize = CompanyFetchList.PageSize;
            CompanyPagedList.Result = CompanyFetchList.Result.Select(x => new CompanyDomainModel
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address,
                ContactNo = x.ContactNo,
                Email = x.Email
            });

            return CompanyPagedList;
        }

        public CompanyDomainModel GetCompanyById(int Id)
        {
            return Context.Companies.Where(x => x.Id == Id).Select(x => new CompanyDomainModel
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address,
                ContactNo = x.ContactNo,
                Email = x.Email
            }).FirstOrDefault();
        }

        public List<CompanyDomainModel> GetCompanyForDropDown()
        {
            return Context.Companies
                          .Select(x => new CompanyDomainModel { Id = x.Id, Name = x.Name })
                          .ToList();
        }
    }
}
