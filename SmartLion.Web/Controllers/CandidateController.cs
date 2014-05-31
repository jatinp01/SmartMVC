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

namespace SmartLion.Web.Controllers
{
    public class CandidateController : Controller
    {
        //
        // GET: /Candidate/

        public ActionResult Index()
        {
            IndexCandidateModel model = new IndexCandidateModel();
            model.CandidateList = CandidateManager.Instance.GetCandidate(1, 10);
            return View(model);
        }

        public JsonResult List(int PageIndex, string Search = null)
        {
            return Json(CandidateManager.Instance.GetCandidate(PageIndex, 10, Search), JsonRequestBehavior.AllowGet);
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
            Mapper.CreateMap<CandidateDomainModel, CandidateModel>();
            CandidateModel model = Mapper.Map<CandidateModel>(DomainCandidate);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(string actionType, CandidateModel model)
        {
            if (actionType.ToLowerInvariant() == "save")
            {
                if (ModelState.IsValid)
                {
                    Mapper.CreateMap<CandidateModel, CandidateDomainModel>();
                    CandidateDomainModel DomainCandidate = Mapper.Map<CandidateDomainModel>(model);
                    CandidateManager.Instance.UpdateCandidate(DomainCandidate);
                }
                else
                {
                    return View(model);
                }
            }
            return RedirectToAction("Index", "Candidate");
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
