using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartLion.Web.Models;
using AutoMapper;
using SmartLion.Domain.Model;
using SmartLion.Business;
using System.Web.Security;
using SmartLion.Core.Attributes;

namespace SmartLion.Web.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        [AllowAnonymous]
        public ActionResult Index()
        {
            LoginModel model = new LoginModel();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                Mapper.CreateMap<LoginModel, UserDomainModel>();
                UserDomainModel DomainUser = Mapper.Map<UserDomainModel>(model);
                int Id = UserManager.Instance.Login(DomainUser);
                if (Id > 0)
                {
                    string Role = UserManager.Instance.GetUserRole(model.UserName);
                    FormsAuthentication.SetAuthCookie(model.UserName + "|" + Role + "|" + Convert.ToString(Id), model.RememberMe);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
    }
}
