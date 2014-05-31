using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartLion.Operation.Manager
{
    public class Singleton<T> where T : new()
    {
        private static volatile object instance;
        private static object syncRoot = new Object();

        public Singleton() { }

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new T();
                    }
                }
                return (T)instance;
            }
        }
    }
}
