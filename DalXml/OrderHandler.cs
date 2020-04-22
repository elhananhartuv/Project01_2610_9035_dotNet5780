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
    class OrderHandler : IXmlHandler
    {
        public string path = @"Order.xml";
        XmlSerializer x = new XmlSerializer(typeof(List<Order>));

        public void Create()
        {
            FileStream fileOut = new FileStream(path, FileMode.Create);
            x.Serialize(fileOut, DalXml.orders);
            fileOut.Close();
        }

        public void Load()
        {
            FileStream fileIn = new FileStream(path, FileMode.Open);
            DalXml.orders = (List<Order>)x.Deserialize(fileIn);
            fileIn.Close();
        }

        public void Save()
        {
            FileStream saveFile = new FileStream(path, FileMode.Open);
            x.Serialize(saveFile, DalXml.orders);
            saveFile.Close();
        }
    }
}
