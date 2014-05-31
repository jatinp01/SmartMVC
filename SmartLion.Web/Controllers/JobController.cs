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
    public class JobController : Controller
    {
        //
        // GET: /Job/
        public ActionResult Index()
        {
            IndexJobModel model = new IndexJobModel();
            model.JobList = JobManager.Instance.GetJob(1, 10);
            return View(model);
        }

        public JsonResult List(int PageIndex, string Search = null)
        {
            return Json(JobManager.Instance.GetJob(PageIndex, 10, Search), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            JobModel model = new JobModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(string actionType, JobModel model)
        {
            if (actionType.ToLowerInvariant() == "save")
            {
                if (ModelState.IsValid)
                {
                    Mapper.CreateMap<JobModel, JobDomainModel>();
                    JobDomainModel DomainJob = Mapper.Map<JobDomainModel>(model);
                    JobManager.Instance.AddJob(DomainJob);
                }
                else
                {
                    return View(model);
                }
            }
            return RedirectToAction("Index", "Job");
        }

        public ActionResult Edit(int Id)
        {
            JobDomainModel DomainJob = JobManager.Instance.GetJobById(Id);
            Mapper.CreateMap<JobDomainModel, JobModel>();
            JobModel model = Mapper.Map<JobModel>(DomainJob);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(string actionType, JobModel model)
        {
            if (actionType.ToLowerInvariant() == "save")
            {
                if (ModelState.IsValid)
                {
                    Mapper.CreateMap<JobModel, JobDomainModel>();
                    JobDomainModel DomainJob = Mapper.Map<JobDomainModel>(model);
                    JobManager.Instance.UpdateJob(DomainJob);
                }
                else
                {
                    return View(model);
                }
            }
            return RedirectToAction("Index", "Job");
        }

        [HttpPost]
        public ActionResult Delete(int Id)
        {
            try
            {
                JobManager.Instance.DeleteJob(Id);
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

    }
}
