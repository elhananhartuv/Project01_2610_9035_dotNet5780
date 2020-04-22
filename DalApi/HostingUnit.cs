using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class HostingUnit
    {
        public ActivityStatus Status { get; set; }//extra
       
        public HostingType Type { get; set; }       
        public int Rooms { get; set; }
        public bool Pool { get; set; }
        public bool Jacuzzi { get; set; }
        public bool Garden { get; set; }
        public bool ChildrensAttractions { get; set; }
        public int Key { get; set; }//key
        public int HostId { get; set; }//id of owner
        public string HostingUnitName { get; set; }
        public bool[,] Diary;
        public int NumOfOffers { get; set; }//extra - סופר את מספר ההצעות שנשלחו עבור יחידת אירוח זו
        public int pricePerNight { get; set; }
        public Areas Area { get; set; }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
