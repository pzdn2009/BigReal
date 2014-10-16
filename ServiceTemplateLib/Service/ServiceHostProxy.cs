using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace LogisticsTracking.Lib.Service
{
    internal class ServiceHostProxy
    {
        private Thread thread;
        private IService service;
        private ServiceConfigElement configElement;
        public ServiceStatusInfo Stauts
        {
            get
            {
                return new ServiceStatusInfo() { ThreadState = thread.ThreadState };
            }
        }

        public ServiceHostProxy(ServiceConfigElement configElement)
        {
            this.configElement = configElement;
            this.service = CreateWorkItem();

            this.service.Interval = configElement.Interval;
            this.service.Name = configElement.Name;
            this.service.RunOnlyOnce = configElement.RunOnlyOnce;
            this.service.DoWorkAtStart = configElement.DoWorkAtStart;
        }

        public ServiceHostProxy(IService service)
        {
            this.service = service;
        }

        public ServiceHostProxy(ServiceConfigElement configElement,IService service)
        {
            this.configElement = configElement;
            this.service = service;

            this.service.Interval = configElement.Interval;
            this.service.Name = configElement.Name;
            this.service.RunOnlyOnce = configElement.RunOnlyOnce;
            this.service.DoWorkAtStart = configElement.DoWorkAtStart;
        }

        private IService CreateWorkItem()
        {
            var types = configElement.Type.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

            return (IService)Assembly.Load(types[1]).CreateInstance(types[0]);
        }

        public void Start()
        {
            if (thread == null)
            {
                thread = new Thread(new ThreadStart(service.Start));
                thread.Start();
            }
        }

        public void Stop()
        {
            if (thread != null)
            {
                service.Stop();
                thread.Interrupt();
                thread = null;
            }
        }
    }
}
