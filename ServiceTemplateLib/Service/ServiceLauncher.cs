using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogisticsTracking.Lib.Service
{
    public static class ServiceLaucher
    {
        private static Dictionary<string, ServiceHostProxy> dictServices = new Dictionary<string, ServiceHostProxy>();
        private static IEnumerable<ServiceConfigElement> configElements = null;

        private static void EnsureConfigs()
        {
            if (configElements == null)
            {
                configElements = ServiceConfiguration.GetConfig();
            }
        }

        #region 服务控制

        public static void StartAll()
        {
            EnsureConfigs();

            foreach (ServiceConfigElement serviceElement in configElements)
            {
                StartService(serviceElement);
            }
        }

        public static void Start(string serviceName)
        {
            EnsureConfigs();

            var element = configElements.Where(zw => zw.Name == serviceName).FirstOrDefault();
            if (element != null)
            {
                StartService(element);
            }
        }

        public static void Stop(string serviceName)
        {
            if (configElements == null) return;

            var element = configElements.Where(zw => zw.Name == serviceName).FirstOrDefault();
            if (element != null)
            {
                StopService(element);
            }
        }

        public static void StopAll()
        {
            if (configElements == null) return;

            foreach (ServiceConfigElement serviceElement in configElements)
            {
                StopService(serviceElement);
            }
        }

        public static ServiceStatusInfo GetStatus(string serviceName)
        {
            //if (configElements == null) return ServiceStatusInfo.Empty;

            return QueryStatus(serviceName);
        }

        public static IEnumerable<ServiceStatusInfo> GetStatuses(string serviceName)
        {
            IList<ServiceStatusInfo> list = null;
            if (configElements == null) return list;

            list = new List<ServiceStatusInfo>();
            foreach (var element in configElements)
            {
                list.Add(QueryStatus(element.Name));
            }
            return list;
        }

        #endregion

        public static void RegisterAndStartService(string serviceName, IService service)
        {
            EnsureConfigs();

            try
            {
                var config = configElements.FirstOrDefault(zw => zw.Name == serviceName);
                if (config == null)
                {
                    throw new Exception(string.Format("没有服务：{0}的配置", serviceName));
                }

                ServiceHostProxy proxy = new ServiceHostProxy(config, service);
                dictServices.Add(serviceName, proxy);
                proxy.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static void StartService(ServiceConfigElement serviceElement)
        {
            try
            {
                lock (dictServices)
                {
                    ServiceHostProxy proxy = null;
                    if (dictServices.ContainsKey(serviceElement.Name))
                    {
                        proxy = dictServices[serviceElement.Name];
                    }
                    else
                    {
                        proxy = new ServiceHostProxy(serviceElement);
                        dictServices[serviceElement.Name] = proxy;
                    }
                    proxy.Start();
                }
                ServiceLogger.Debug(string.Format("Service {0} started", serviceElement.Name), "StartService");
            }
            catch (System.Exception ex)
            {
                string errorMsg = string.Format("Service Start Error: {0}", ex.ToString());
                ServiceLogger.Error(errorMsg, "StartService");
            }
        }

        private static void StopService(ServiceConfigElement serviceElement)
        {
            try
            {
                lock (dictServices)
                {
                    if (dictServices.ContainsKey(serviceElement.Name))
                    {
                        dictServices[serviceElement.Name].Stop();
                        dictServices.Remove(serviceElement.Name);
                    }
                }
                ServiceLogger.Debug(string.Format("Service {0} stop", serviceElement.Name), "StopService");
            }
            catch (System.Exception ex)
            {
                string errorMsg = string.Format("Service Stop Error: {0}", ex.ToString());
                ServiceLogger.Error(errorMsg, "StopService");
            }
        }

        private static ServiceStatusInfo QueryStatus(string serviceName)
        {
            try
            {
                lock (dictServices)
                {
                    if (dictServices.ContainsKey(serviceName))
                    {
                        var proxy = dictServices[serviceName];
                        return proxy.Stauts;
                    }
                }
                ServiceLogger.Debug(string.Format("Service {0} started", serviceName), "QueryStatus");
            }
            catch (Exception ex)
            {
                string errorMsg = string.Format("Service Start Error: {0}", ex.ToString());
                ServiceLogger.Error(errorMsg, "QueryStatus");
            }
            return null;
        }
    }
}
