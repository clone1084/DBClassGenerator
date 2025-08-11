using DBDataLibrary.Attributes;
using DBDataLibrary.CRUD;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DBDataLibrary.Utils
{
    public class CacheBootstrapper
    {
        [ImportMany(typeof(ICrudClass))]
        public IEnumerable<ICrudClass> Cacheables { get; set; } = [];

        public void Compose()
        {
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly())); 
            
            var crudAssembly = Assembly.GetAssembly(typeof(ICrudClass));
            if (crudAssembly != null)
                catalog.Catalogs.Add(new AssemblyCatalog(crudAssembly)); 

            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }

        public void LoadAllCaches(IDbConnection connection, ILog log, string baseLogMessage, CancellationToken cancellationToken)
        {
            foreach (var cacheable in Cacheables)
            {
                if (!cacheable.IsCached())
                    continue;

                if (cancellationToken.IsCancellationRequested)
                {
                    log.Debug($"{baseLogMessage} Cache refresh cancelled.");
                    return;
                }

                log.Debug($"{baseLogMessage} Loading cache for {cacheable.TableName}");
                cacheable.ReLoadCache(connection, log, baseLogMessage, cancellationToken);                
            }
        }
    }

    public static class CacheRefreshScheduler
    {
        private static CancellationTokenSource _cts = new();
        private static Task? _backgroundTask;

        public static void Start(IDbConnection connection, ILog log, TimeSpan interval, string baseLogMessage)
        {
            _backgroundTask = Task.Run(async () =>
            {
                CacheBootstrapper cbs = new CacheBootstrapper();
                cbs.Compose();
                while (!_cts.Token.IsCancellationRequested)
                {
                    try
                    {
                        cbs.LoadAllCaches(connection, log, baseLogMessage, _cts.Token);
                    }
                    catch (Exception ex)
                    {
                        log.Error("[CacheRefreshScheduler] Errore durante il refresh della cache", ex);
                    }

                    await Task.Delay(interval, _cts.Token);
                }
            }, _cts.Token);
        }

        public static void Stop()
        {
            _cts.Cancel();
        }
    }
}
