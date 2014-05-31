using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace SmartLion.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class RoleAuthorizeAttribute : AuthorizeAttribute
    {
        public string SystemRoles { get; set; }

        public RoleAuthorizeAttribute(string roles)
        {
            this.SystemRoles = roles;
        }
    }
}
