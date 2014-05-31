using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartLion.Model;

namespace SmartLion.Operation.Manager
{
    /// <summary>
    /// Unified structure of business layer entities.
    /// </summary>
    public interface IManager
    {
        /// <summary>
        /// Allows the caller to override the internal context. This is used internally for setup. Should remain <c>null</c>.
        /// </summary>
        SmartLionDBEntities Context { get; set; }

        /// <summary>
        /// Convenient access to Context.SaveChanges with error handling and logging.
        /// </summary>
        /// <returns></returns>
        int SaveChanges();
    }
}
