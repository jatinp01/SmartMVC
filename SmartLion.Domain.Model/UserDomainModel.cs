using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartLion.Domain.Model
{
    public class UserDomainModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public int Status { get; set; }
        public string StatusName { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }

    public class RoleModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
