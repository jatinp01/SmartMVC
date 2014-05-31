using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;

namespace SmartLion.Core.Principal
{
    public class UserPrincipal : IPrincipal
    {
        public IIdentity Identity { get; private set; }
        public string Role { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }

        public UserPrincipal(string ticket)
        {
            if (!string.IsNullOrEmpty(ticket))
            {
                string[] authValues = ticket.Split('|');
                this.Identity = new GenericIdentity(authValues[0]);
                this.UserName = authValues[0];
                this.Role = authValues[1];
                this.UserId = Convert.ToInt32(authValues[2]);
            }
        }

        public bool IsInRole(string role)
        {
            List<string> roleList = role.Split(',').ToList();
            return roleList.Contains(this.Role);
        }
    }
}