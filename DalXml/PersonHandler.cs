using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DO;

namespace Dal
{
    public class PersonHandler : IXmlHandler
    {
        public string path = @"Person.xml";
        XmlSerializer x = new XmlSerializer(typeof(List<Person>));
        
        public void Create()
        {
            FileStream fileOut = new FileStream(path, FileMode.Create);
            x.Serialize(fileOut, DalXml.persons);
            fileOut.Close();
        }

        public void Load()
        {
            FileStream fileIn = new FileStream(path, FileMode.Open);
            DalXml.persons = (List<Person>)x.Deserialize(fileIn);
            fileIn.Close();
        }

        public void Save()
        {
            FileStream saveFile = new FileStream(path, FileMode.Open);
            x.Serialize(saveFile, DalXml.persons);
            saveFile.Close();
        }
    }
}
