using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class Order
    {
        public int Key { get; set; }//key
        public int HostId { get; set; }
        public string ClientName { get; set; }
        public int HostingUnitKey { get; set; }
        public int GuestRequestKey { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime OrderDate { get; set; }//create date
        public DateTime SentDate { get; set; }
        public DateTime CloseDate { get; set; }
        public int TotalPrice { get; set; }//extra
        public int CliendId { get; set; }
        public int Commission { get; set; }//סכום העמלה לאחר סגירת עסקה
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
