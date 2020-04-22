using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    interface IXmlHandler
    {
        void Create();
        void Load();
        void Save();
    }
}
