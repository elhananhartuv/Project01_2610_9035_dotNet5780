using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
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
        public string Phone { get; set; }
        public string Address { get; set; }
        public override string ToString()
        {
            string personDetails =
                            "ID: " + Id + "\n" +
                            "LastName: " + LastName + "\n" +
                            "FirstName: " + FirstName + "\n" +
                            "PhoneNumber: " + Phone + "\n";
            return personDetails;
        }
    }
}
