using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class Person
    {
        public int Id { get; set; }//key
        public IdType IdType { get; set; }
        public PersonStatus Status { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
