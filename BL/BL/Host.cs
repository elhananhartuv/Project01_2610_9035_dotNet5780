using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Host
    {
        public Person HostInfo { get; set; }
        public string WebSite { get; set; }
        public BankBranch AccountDetails { get; set; }
        public bool CollectionClearance { get; set; }
        public IEnumerable<HostingUnit> MyUnits { get; set; }
        public IEnumerable<Order> MyOrders { get; set; }
    }
}
