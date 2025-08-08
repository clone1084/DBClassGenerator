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

namespace DBDataLibrary.DbUtils
{
    public class CacheBootstrapper
    {
        [ImportMany(typeof(ICrudClass))]
        public IEnumerable<ICrudClass> Cacheables { get; set; } = [];

        public void Compose()
        {
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly())); // o directory catalog
            catalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetAssembly(typeof(ICrudClass)))); // o directory catalog

            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }

        public void LoadAllCaches(IDbConnection connection, ILog log, string baseLogMessage)
        {
            foreach (var cacheable in Cacheables)
            {
                if (!cacheable.IsCached())
                    continue;

                log.Debug($"Loading cache for {cacheable.TableName}");
                cacheable.ReLoadCache(connection, log, baseLogMessage);                
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
                        cbs.LoadAllCaches(connection, log, baseLogMessage);
                    }
                    catch (Exception ex)
                    {
                        log.Error("[CacheRefreshScheduler] Errore durante il refresh della cache", ex);
                    }

                    await Task.Delay(interval, _cts.Token);
                }
            }, _cts.Token);
        }

        //public static void Stop()
        //{
        //    _cts.Cancel();
        //    _backgroundTask?.Wait();
        //}
        public static void Stop()
        {
            _cts.Cancel();
            try
            {
                _backgroundTask?.Wait();
            }
            catch (AggregateException ex)
            {
                foreach (var inner in ex.InnerExceptions)
                {
                    if (inner is not TaskCanceledException)
                        throw;
                }
            }
        }

    }
}
