using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SmartLion.Core.Attributes;
using System.Web.Routing;
using SmartLion.Core.Principal;

namespace SmartLion.Core.Filter
{
    public class RequireAuthenticationAttribute : AuthorizeAttribute
    {
        protected virtual UserPrincipal CurrentUser
        {
            get { return HttpContext.Current.User as UserPrincipal; }
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var skipAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) ||
                                    filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(
                                        typeof(AllowAnonymousAttribute), true);
            if (!skipAuthorization)
            {
                var roleAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(RoleAuthorizeAttribute), true) ||
                                    filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(
                                        typeof(RoleAuthorizeAttribute), true);
                if (roleAuthorization)
                {
                    string allowedRoles = filterContext.ActionDescriptor.GetCustomAttributes(typeof(RoleAuthorizeAttribute), true)
                                          .Cast<RoleAuthorizeAttribute>().Single().SystemRoles;

                    if (CurrentUser.IsInRole(allowedRoles))
                    {
                        base.OnAuthorization(filterContext);
                    }
                    else
                    {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary 
                        {
                            { "action", "Index" },
                            { "controller", "UnAuthorize" }
                        });
                    }
                }
                else
                {
                    base.OnAuthorization(filterContext);
                }
            }
        }
    }
}
