using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using System.Xml.Serialization;
using System.IO;

namespace Dal
{
    class HostingUnitHandler : IXmlHandler
    {
        public string path = @"HostingUnit.xml";
        XmlSerializer x = new XmlSerializer(typeof(List<HostingUnitXml>));

        public void Create()
        {
            FileStream fileOut = new FileStream(path, FileMode.Create);
            x.Serialize(fileOut, DalXml.hostingUnits);
            fileOut.Close();
        }

        public void Load()
        {
            FileStream fileIn = new FileStream(path, FileMode.Open);
            DalXml.hostingUnits = (List<HostingUnitXml>)x.Deserialize(fileIn);
            fileIn.Close();
        }

        public void Save()
        {
            FileStream saveFile = new FileStream(path, FileMode.Open);
            x.Serialize(saveFile, DalXml.hostingUnits);
            saveFile.Close();
        }
    }
}
