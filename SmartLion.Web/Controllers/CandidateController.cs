using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartLion.Web.Models;
using SmartLion.Business;
using AutoMapper;
using SmartLion.Domain.Model;
using SmartLion.Core;
using System.Globalization;
using SmartLion.Core.Principal;
using SmartLion.Core.Attributes;

namespace SmartLion.Web.Controllers
{
    public class CandidateController : Controller
    {
        //
        // GET: /Candidate/

        public ActionResult Index()
        {
            IndexCandidateModel model = new IndexCandidateModel();
            bool UserFilterOn = (User as UserPrincipal).Role.ToLowerInvariant().Equals("user");
            model.CandidateList = CandidateManager.Instance.GetCandidate(1, 10, (User as UserPrincipal).UserId, UserFilterOn);
            return View(model);
        }

        public JsonResult List(int PageIndex, string Search = null)
        {
            bool UserFilterOn = (User as UserPrincipal).Role.ToLowerInvariant().Equals("user");
            return Json(CandidateManager.Instance.GetCandidate(PageIndex, 10, (User as UserPrincipal).UserId, UserFilterOn, Search), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            DateTime dtDate = DateTime.Today;
            CandidateModel model = new CandidateModel() { Date = String.Format("{0:MM/dd/yyyy}", dtDate), InterviewDate = String.Format("{0:MM/dd/yyyy}", dtDate) };
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(string actionType, CandidateModel model)
        {
            if (actionType.ToLowerInvariant() == "save")
            {
                if (ModelState.IsValid)
                {
                    Mapper.CreateMap<CandidateModel, CandidateDomainModel>();
                    CandidateDomainModel DomainCandidate = Mapper.Map<CandidateDomainModel>(model);
                    DomainCandidate.CreateUserId = (User as UserPrincipal).UserId;
                    CandidateManager.Instance.AddCandidate(DomainCandidate);
                }
                else
                {
                    return View(model);
                }
            }
            return RedirectToAction("Index", "Candidate");
        }

        public ActionResult Edit(int Id)
        {
            CandidateDomainModel DomainCandidate = CandidateManager.Instance.GetCandidateById(Id);
            Mapper.CreateMap<CandidateDomainModel, EditCandidateModel>();
            EditCandidateModel model = Mapper.Map<EditCandidateModel>(DomainCandidate);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(string actionType, EditCandidateModel model)
        {
            if (actionType.ToLowerInvariant() == "save")
            {
                if (ModelState.IsValid)
                {
                    Mapper.CreateMap<EditCandidateModel, CandidateDomainModel>();
                    CandidateDomainModel DomainCandidate = Mapper.Map<CandidateDomainModel>(model);
                    DomainCandidate.ModifiedUserId = (User as UserPrincipal).UserId;
                    CandidateManager.Instance.UpdateCandidate(DomainCandidate);
                }
                else
                {
                    return View(model);
                }
            }
            return RedirectToAction("Index", "Candidate");
        }

        [RoleAuthorize("SystemAdmin,Admin")]
        public PartialViewResult EditStatus(int Id)
        {
            CandidateDomainModel DomainCandidate = CandidateManager.Instance.GetCandidateById(Id);
            Mapper.CreateMap<CandidateDomainModel, EditStatusCandidateModel>();
            EditStatusCandidateModel model = Mapper.Map<EditStatusCandidateModel>(DomainCandidate);
            return PartialView(model);
        }

        [HttpPost]
        [RoleAuthorize("SystemAdmin,Admin")]
        public ActionResult EditStatus(int Id, int StatusId)
        {
            try
            {
                CandidateManager.Instance.UpdateCandidateStatus(Id, StatusId, (User as UserPrincipal).UserId);
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult Delete(int Id)
        {
            try
            {
                CandidateManager.Instance.DeleteCandidate(Id);
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

    }
}
