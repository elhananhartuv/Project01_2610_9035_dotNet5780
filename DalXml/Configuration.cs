using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal
{
    class Configuration
    {
        XElement ConfigRoot;
        string path = @"Configuration.xml";
        public Configuration()
        {
            try
            {
                ConfigRoot = XElement.Load(path);
            }
            catch (Exception)
            {
                ConfigRoot = new XElement("Configuration");
                ConfigRoot.Add(new XElement("HostingUnitKey", 10000001));
                ConfigRoot.Add(new XElement("GuestRequestKey", 10000001));
                ConfigRoot.Add(new XElement("OrderKey", 10000001));
                ConfigRoot.Add(new XElement("Commission", 10));
                ConfigRoot.Save(path);
            }
        }
        public Dictionary<string, string> LoadConfig()
        {
            return (from items in ConfigRoot.Elements()
                    select items).ToDictionary(x => "" + x.Name, x => x.Value);
        }
        public void SaveConfig(Dictionary<string, string> dictionary)
        {
            ConfigRoot = new XElement("Configuration");
            XElement xElement;
            foreach (var item in dictionary)
            {
                xElement = new XElement(item.Key, item.Value);
                ConfigRoot.Add(xElement);
            }
            ConfigRoot.Save(path);
        }
    }
}
