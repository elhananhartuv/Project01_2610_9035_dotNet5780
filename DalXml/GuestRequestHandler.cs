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
    class GuestRequestHandler : IXmlHandler
    {
        public string path = @"GuestRequest.xml";
        XmlSerializer x = new XmlSerializer(typeof(List<GuestRequest>));

        public void Create()
        {
            FileStream fileOut = new FileStream(path, FileMode.Create);
            x.Serialize(fileOut, DalXml.guestRequests);
            fileOut.Close();
        }

        public void Load()
        {
            FileStream fileIn = new FileStream(path, FileMode.Open);
            DalXml.guestRequests = (List<GuestRequest>)x.Deserialize(fileIn);
            fileIn.Close();
        }

        public void Save()
        {
            FileStream saveFile = new FileStream(path, FileMode.Open);
            x.Serialize(saveFile, DalXml.guestRequests);
            saveFile.Close();
        }
    }
}
