using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartLion.Business;
using AutoMapper;
using SmartLion.Domain.Model;
using SmartLion.Web.Models;
using SmartLion.Core.Attributes;

namespace SmartLion.Web.Controllers
{
    public class StatusController : Controller
    {
        //
        // GET: /Status/



        [RoleAuthorize("SystemAdmin")]
        public ActionResult Index()
        {
            IndexStatusModel model = new IndexStatusModel();
            model.StatusList = StatusManager.Instance.GetStatus(1, 10);
            return View(model);
        }

        [RoleAuthorize("SystemAdmin")]
        public JsonResult List(int PageIndex, string Search = null)
        {
            return Json(StatusManager.Instance.GetStatus(PageIndex, 10, Search), JsonRequestBehavior.AllowGet);
        }

        [RoleAuthorize("SystemAdmin")]
        public ActionResult Create()
        {
            StatusModel model = new StatusModel();
            return View(model);
        }

        [HttpPost]
        [RoleAuthorize("SystemAdmin")]
        public ActionResult Create(string actionType, StatusModel model)
        {
            if (actionType.ToLowerInvariant() == "save")
            {
                if (ModelState.IsValid)
                {
                    Mapper.CreateMap<StatusModel, StatusDomainModel>();
                    StatusDomainModel DomainStatus = Mapper.Map<StatusDomainModel>(model);
                    StatusManager.Instance.AddStatus(DomainStatus);
                }
                else
                {
                    return View(model);
                }
            }
            return RedirectToAction("Index", "Status");
        }

        [RoleAuthorize("SystemAdmin")]
        public ActionResult Edit(int Id)
        {
            StatusDomainModel DomainStatus = StatusManager.Instance.GetStatusById(Id);
            Mapper.CreateMap<StatusDomainModel, StatusModel>();
            StatusModel model = Mapper.Map<StatusModel>(DomainStatus);
            return View(model);
        }

        [HttpPost]
        [RoleAuthorize("SystemAdmin")]
        public ActionResult Edit(string actionType, StatusModel model)
        {
            if (actionType.ToLowerInvariant() == "save")
            {
                if (ModelState.IsValid)
                {
                    Mapper.CreateMap<StatusModel, StatusDomainModel>();
                    StatusDomainModel DomainStatus = Mapper.Map<StatusDomainModel>(model);
                    StatusManager.Instance.UpdateStatus(DomainStatus);
                }
                else
                {
                    return View(model);
                }
            }
            return RedirectToAction("Index", "Status");
        }

        [HttpPost]
        [RoleAuthorize("SystemAdmin")]
        public ActionResult Delete(int Id)
        {
            try
            {
                StatusManager.Instance.DeleteStatus(Id);
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
    }
}
