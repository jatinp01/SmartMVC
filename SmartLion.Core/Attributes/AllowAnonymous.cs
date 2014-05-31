using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartLion.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class AllowAnonymousAttribute : Attribute
    {
    }
}
