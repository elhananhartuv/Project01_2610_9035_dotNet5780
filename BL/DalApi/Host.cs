using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class Host
    {
        public ActivityStatus Status { get; set; }//extra
        public int Id { get; set; }//key
        public string Name { get; set; }
        public string WebSite { get; set; }
        public BankBranch AccountDetails { get; set; }
        public bool CollectionClearance { get; set; }
        public override string ToString()
        {
            return String.Format(" name {0} ID {1},website {2} AccountDetails{3} ", Name, Id, WebSite, AccountDetails);
        }
    }
}
