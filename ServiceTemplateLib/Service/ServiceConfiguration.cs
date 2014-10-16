using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace LogisticsTracking.Lib.Service
{
    public class ServiceConfiguration
    {
        public static IEnumerable<ServiceConfigElement> GetConfig(string fileName = "")
        {
            var name = String.IsNullOrEmpty(fileName) ? "./Service/ServiceConfig.xml" : fileName;
            XDocument doc = XDocument.Load(name);

            var query = doc.Descendants("Service")
                .Select(zw => new ServiceConfigElement()
                {
                    Name = zw.Element("Name").Value,
                    DoWorkAtStart = zw.Element("DoWorkAtStart").Value.ToLower().Equals("true"),
                    Interval = int.Parse(zw.Element("Interval").Value),
                    RunOnlyOnce = zw.Element("RunOnlyOnce").Value.ToLower().Equals("true"),
                    Type = zw.Element("Type").Value
                });

            return query;
        }
    }
}
