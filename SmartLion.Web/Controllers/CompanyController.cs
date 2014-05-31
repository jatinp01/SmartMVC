using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartLion.Web.Models;
using SmartLion.Business;
using AutoMapper;
using SmartLion.Domain.Model;

namespace SmartLion.Web.Controllers
{
    public class CompanyController : Controller
    {
        //
        // GET: /Company/
        public ActionResult Index()
        {
            IndexCompanyModel model = new IndexCompanyModel();
            model.CompanyList = CompanyManager.Instance.GetCompany(1, 10);
            return View(model);
        }

        public JsonResult List(int PageIndex, string Search = null)
        {
            return Json(CompanyManager.Instance.GetCompany(PageIndex, 10, Search), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            CompanyModel model = new CompanyModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(string actionType, CompanyModel model)
        {
            if (actionType.ToLowerInvariant() == "save")
            {
                if (ModelState.IsValid)
                {
                    Mapper.CreateMap<CompanyModel, CompanyDomainModel>();
                    CompanyDomainModel DomainCompany = Mapper.Map<CompanyDomainModel>(model);
                    CompanyManager.Instance.AddCompany(DomainCompany);
                }
                else
                {
                    return View(model);
                }
            }
            return RedirectToAction("Index", "Company");
        }

        public ActionResult Edit(int Id)
        {
            CompanyDomainModel DomainCompany = CompanyManager.Instance.GetCompanyById(Id);
            Mapper.CreateMap<CompanyDomainModel, CompanyModel>();
            CompanyModel model = Mapper.Map<CompanyModel>(DomainCompany);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(string actionType, CompanyModel model)
        {
            if (actionType.ToLowerInvariant() == "save")
            {
                if (ModelState.IsValid)
                {
                    Mapper.CreateMap<CompanyModel, CompanyDomainModel>();
                    CompanyDomainModel DomainCompany = Mapper.Map<CompanyDomainModel>(model);
                    CompanyManager.Instance.UpdateCompany(DomainCompany);
                }
                else
                {
                    return View(model);
                }
            }
            return RedirectToAction("Index", "Company");
        }

        [HttpPost]
        public ActionResult Delete(int Id)
        {
            try
            {
                CompanyManager.Instance.DeleteCompany(Id);
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
    }
}
