using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartLion.Domain.Model;
using AutoMapper;
using SmartLion.Web.Models;
using SmartLion.Business;
using SmartLion.Core.Principal;

namespace SmartLion.Web.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        public ActionResult Index()
        {
            UserDomainModel DomainUser = UserManager.Instance.GetUserById((User as UserPrincipal).UserId);
            Mapper.CreateMap<UserDomainModel, AccountModel>();
            AccountModel model = Mapper.Map<AccountModel>(DomainUser);
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(string actionType, AccountModel model)
        {
            if (actionType.ToLowerInvariant() == "save")
            {
                if (ModelState.IsValid)
                {
                    Mapper.CreateMap<AccountModel, UserDomainModel>();
                    UserDomainModel DomainUser = Mapper.Map<UserDomainModel>(model);
                    UserManager.Instance.UpdateUser(DomainUser);
                }
                else
                {
                    return View(model);
                }
            }
            return RedirectToAction("Index", "Account");
        }

        [ChildActionOnly]
        public ActionResult LogOn()
        {
            UserDomainModel DomainUser = UserManager.Instance.GetUserById((User as UserPrincipal).UserId);
            Mapper.CreateMap<UserDomainModel, AddUserModel>();
            AddUserModel model = Mapper.Map<AddUserModel>(DomainUser);
            return PartialView(model);
        }

    }
}
