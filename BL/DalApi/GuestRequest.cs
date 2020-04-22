using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class GuestRequest
    {
        public int Key { get; set; }//key
        public int ClientId { get; set; }
        public RequestStatus Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime LeaveDate { get; set; }
        public Areas Area { get; set; }
        public string SubArea { get; set; }
        public HostingType Type { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public int Rooms { get; set; }
        public Answer Pool { get; set; }
        public Answer Jacuzzi { get; set; }
        public Answer Garden { get; set; }
        public Answer ChildrensAttractions { get; set; }
        public override string ToString()
        {
            return string.Format("Status request-{0},EntryDate-{1},LeaveDate{2}", Status, EntryDate, LeaveDate);
        }
    }
}
