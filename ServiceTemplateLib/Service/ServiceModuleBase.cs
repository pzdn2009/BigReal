using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace LogisticsTracking.Lib.Service
{
    public abstract class ServiceModuleBase : IService
    {
        protected abstract void DoWork();
        private Timer timer;

        public void Start()
        {
            if (DoWorkAtStart)
            {
                Work();

                if (RunOnlyOnce)
                {
                    return;
                }
            }

            if (timer != null)
            {
                return;
            }

            timer = new Timer();
            timer.Enabled = true;
            timer.Interval = Interval;
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            timer.Enabled = false;

            Work();

            if (RunOnlyOnce)
            {
                Stop();
                return;
            }

            timer.Enabled = true;
        }

        private void Work()
        {
            DoWork();
        }

        public void Stop()
        {
            if (timer != null)
            {
                timer.Enabled = false;
                timer.Dispose();
            }
        }

        public int Interval
        {
            get;
            set;
        }

        public bool DoWorkAtStart
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public bool RunOnlyOnce
        {
            get;
            set;
        }
    }
}
