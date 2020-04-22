using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Order
    {
        public int Key { get; set; }
        public int HostingUnitKey { get; set; }
        public int GuestRequestKey { get; set; }
        //public GuestRequest ClientRequest { get; set; }
        //public HostingUnit Unit { get; set; }
        public int HostId { get; set; }
        public int ClientId { get; set; }//exsit in the guest request
        public string ClientName { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime OrderDate { get; set; }//create date
        public DateTime SentDate { get; set; }
        public DateTime CloseDate { get; set; }//Confirmation date
        public int TotalPrice { get; set; }//extra
        public int Commission { get; set; }//סכום העמלה לאחר סגירת עסקה

    }
}
