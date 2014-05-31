using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartLion.Business;
using AutoMapper;
using SmartLion.Domain.Model;
using SmartLion.Web.Models;

namespace SmartLion.Web.Controllers
{
    public class ReferenceController : Controller
    {
        //
        // GET: /Reference/

        public ActionResult Index()
        {
            IndexReferenceModel model = new IndexReferenceModel();
            model.ReferenceList = ReferenceManager.Instance.GetReference(1, 10);
            return View(model);
        }

        public JsonResult List(int PageIndex, string Search = null)
        {
            return Json(ReferenceManager.Instance.GetReference(PageIndex, 10, Search), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            ReferenceModel model = new ReferenceModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(string actionType, ReferenceModel model)
        {
            if (actionType.ToLowerInvariant() == "save")
            {
                if (ModelState.IsValid)
                {
                    Mapper.CreateMap<ReferenceModel, ReferenceDomainModel>();
                    ReferenceDomainModel DomainReference = Mapper.Map<ReferenceDomainModel>(model);
                    ReferenceManager.Instance.AddReference(DomainReference);
                }
                else
                {
                    return View(model);
                }
            }
            return RedirectToAction("Index", "Reference");
        }

        public ActionResult Edit(int Id)
        {
            ReferenceDomainModel DomainReference = ReferenceManager.Instance.GetReferenceById(Id);
            Mapper.CreateMap<ReferenceDomainModel, ReferenceModel>();
            ReferenceModel model = Mapper.Map<ReferenceModel>(DomainReference);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(string actionType, ReferenceModel model)
        {
            if (actionType.ToLowerInvariant() == "save")
            {
                if (ModelState.IsValid)
                {
                    Mapper.CreateMap<ReferenceModel, ReferenceDomainModel>();
                    ReferenceDomainModel DomainReference = Mapper.Map<ReferenceDomainModel>(model);
                    ReferenceManager.Instance.UpdateReference(DomainReference);
                }
                else
                {
                    return View(model);
                }
            }
            return RedirectToAction("Index", "Reference");
        }

        [HttpPost]
        public ActionResult Delete(int Id)
        {
            try
            {
                ReferenceManager.Instance.DeleteReference(Id);
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

    }
}
