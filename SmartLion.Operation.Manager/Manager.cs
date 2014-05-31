using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartLion.Model;
using SmartLion.Model.Context;

namespace SmartLion.Operation.Manager
{
    /// <summary>
    /// Base class for all Business Classes
    /// </summary>
    public abstract class Manager<T> : Singleton<T>, IManager where T : new()
    {
        private SmartLionDBEntities _ctx;

        protected SmartLionDBEntities Ctx
        {
            get
            {
                if (_ctx == null)
                {
                    return DataContextFactory.GetWebRequestScopedDataContext<SmartLionDBEntities>();
                }
                return _ctx;
            }
        }

        public SmartLionDBEntities Context
        {
            get { return Ctx; }
            set { _ctx = value; }
        }

        public int SaveChanges()
        {
            return Ctx.SaveChanges();
        }
    }
}
