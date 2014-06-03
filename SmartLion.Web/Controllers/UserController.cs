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
using SmartLion.Core.Attributes;

namespace SmartLion.Web.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/
        [RoleAuthorize("SystemAdmin")]
        public ActionResult Index()
        {
            IndexUserModel model = new IndexUserModel();
            model.UserList = UserManager.Instance.GetUsers(1, 10);
            return View(model);
        }

        [RoleAuthorize("SystemAdmin")]
        public JsonResult List(int PageIndex, string Search = null)
        {
            return Json(UserManager.Instance.GetUsers(PageIndex, 10, Search), JsonRequestBehavior.AllowGet);
        }

        [RoleAuthorize("SystemAdmin")]
        public ActionResult Create()
        {
            AddUserModel model = new AddUserModel() { Status = 1 };
            return View(model);
        }

        [HttpPost]
        [RoleAuthorize("SystemAdmin")]
        public ActionResult Create(string actionType, AddUserModel model)
        {
            if (actionType.ToLowerInvariant() == "save")
            {
                if (ModelState.IsValid)
                {
                    Mapper.CreateMap<AddUserModel, UserDomainModel>();
                    UserDomainModel DomainUser = Mapper.Map<UserDomainModel>(model);
                    UserManager.Instance.AddUser(DomainUser);
                }
                else
                {
                    return View(model);
                }
            }
            return RedirectToAction("Index", "User");
        }

        [RoleAuthorize("SystemAdmin")]
        public ActionResult Edit(int Id)
        {
            UserDomainModel DomainUser = UserManager.Instance.GetUserById(Id);
            Mapper.CreateMap<UserDomainModel, EditUserModel>();
            EditUserModel model = Mapper.Map<EditUserModel>(DomainUser);
            return View(model);
        }

        [HttpPost]
        [RoleAuthorize("SystemAdmin")]
        public ActionResult Edit(string actionType, EditUserModel model)
        {
            if (actionType.ToLowerInvariant() == "save")
            {
                if (ModelState.IsValid)
                {
                    Mapper.CreateMap<EditUserModel, UserDomainModel>();
                    UserDomainModel DomainUser = Mapper.Map<UserDomainModel>(model);
                    UserManager.Instance.UpdateUser(DomainUser);
                }
                else
                {
                    return View(model);
                }
            }
            return RedirectToAction("Index", "User");
        }

        [HttpPost]
        [RoleAuthorize("SystemAdmin")]
        public ActionResult Delete(int Id)
        {
            try
            {
                UserManager.Instance.DeleteUser(Id);
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [RoleAuthorize("SystemAdmin")]
        public JsonResult IsUserNameExist(string UserName)
        {
            bool isNotExist = !UserManager.Instance.UserExist(UserName);
            return Json(isNotExist, JsonRequestBehavior.AllowGet);
        }
    }
}
