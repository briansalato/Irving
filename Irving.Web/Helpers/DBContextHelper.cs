using System;
using Irving.Web.DAL;
using System.Collections;
using System.Data.Entity.Infrastructure;
using System.Web;
using System.Threading;
namespace Irving.Web.Helpers
{
    public static class DBContextHelper
    {
        // accessed via lock(_threadObjectContexts), only required for multi threaded non web applications
        private static readonly Hashtable _threadDataContexts = new Hashtable();

        public static IIrvingDbContext GetDataContext(string contextKey)
        {
            IIrvingDbContext dataContext = GetCurrentDataContext(contextKey);
            if (dataContext == null) // create and store the object context
            {
                dataContext = new IrvingDbContext();
                StoreCurrentObjectContext(dataContext, contextKey);
            }
            return dataContext;
        }

        private static void StoreCurrentObjectContext(IIrvingDbContext dataContext, string contextKey)
        {
            if (HttpContext.Current == null)
                StoreCurrentThreadDataContext(dataContext, contextKey);
            else
                StoreCurrentHttpContextDataContext(dataContext, contextKey);
        }

        private static void StoreCurrentHttpContextDataContext(IIrvingDbContext dataContext, string contextKey)
        {
            if (HttpContext.Current.Items.Contains(contextKey))
                HttpContext.Current.Items[contextKey] = dataContext;
            else
                HttpContext.Current.Items.Add(contextKey, dataContext);
        }

        private static void StoreCurrentThreadDataContext(IIrvingDbContext dataContext, string contextKey)
        {
            lock (_threadDataContexts.SyncRoot)
            {
                if (_threadDataContexts.Contains(contextKey))
                    _threadDataContexts[contextKey] = dataContext;
                else
                    _threadDataContexts.Add(contextKey, dataContext);
            }
        }

        private static IIrvingDbContext GetCurrentDataContext(string contextKey)
        {
            return HttpContext.Current == null ?
                GetCurrentThreadDataContext(contextKey) :
                GetCurrentHttpContextDataContext(contextKey);
        }

        private static IIrvingDbContext GetCurrentThreadDataContext(string contextKey)
        {
            IIrvingDbContext dataContext = null;

            Thread threadCurrent = Thread.CurrentThread;

            if (threadCurrent.Name == null)
            {
                threadCurrent.Name = contextKey;
            }
            else
            {
                object threadDataContext = null;
                lock (_threadDataContexts.SyncRoot)
                {
                    threadDataContext = _threadDataContexts[contextKey];
                }
                if (threadDataContext != null)
                {
                    dataContext = (IIrvingDbContext)threadDataContext;
                }
            }

            return dataContext;
        }

        private static IIrvingDbContext GetCurrentHttpContextDataContext(string contextKey)
        {
            IIrvingDbContext dataContext = default(IIrvingDbContext);

            if (HttpContext.Current.Items.Contains(contextKey))
            {
                dataContext = (IIrvingDbContext)HttpContext.Current.Items[contextKey];
            }

            return dataContext;
        }
    }
}
