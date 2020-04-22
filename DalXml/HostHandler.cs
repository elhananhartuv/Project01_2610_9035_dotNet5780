using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DO;
using System.IO;

namespace Dal
{
    class HostHandler : IXmlHandler
    {
        public string path = @"Host.xml";
        XmlSerializer x = new XmlSerializer(typeof(List<Host>));

        public void Create()
        {
            FileStream fileOut = new FileStream(path, FileMode.Create);
            x.Serialize(fileOut, DalXml.hosts);
            fileOut.Close();
        }

        public void Load()
        {
            FileStream fileIn = new FileStream(path, FileMode.Open);
            DalXml.hosts = (List<Host>)x.Deserialize(fileIn);
            fileIn.Close();
        }

        public void Save()
        {
            FileStream saveFile = new FileStream(path, FileMode.Open);
            x.Serialize(saveFile, DalXml.hosts);
            saveFile.Close();
        }
    }
}
