using LogisticsTracking.Lib.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceTemplateLib
{
    public class Demo : ServiceModuleBase
    {
        protected override void DoWork()
        {
            Console.WriteLine("test...");
        }
    }
}
