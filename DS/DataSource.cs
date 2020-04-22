using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DS
{
    static public class DataSource
    {
        public static List<BankBranch> bankBranchesList = new List<BankBranch>();
        public static List<GuestRequest> guestRequestList = new List<GuestRequest>();
        public static List<Host> hostsList = new List<Host>();
        public static List<HostingUnit> hostingUnitsList = new List<HostingUnit>();
        public static List<Order> ordersList = new List<Order>();
        public static List<Person> personList = new List<Person>();

        static DataSource()
        {
            //hard coding our datasource
            personList = new List<Person>()
            {
                new Person()
                {
                    Id = 201649035,
                    IdType =IdType.ID,
                    Status = PersonStatus.ACTIVE,
                    Password = "1",
                    FirstName = "Mendi",
                    LastName = "Shneorson",
                    Phone = "100",
                    Email = "elchananhrtuv@gmail.com",
                },
                new Person()
                {
                    Id = 212282610,
                    IdType =IdType.ID,
                    Status = PersonStatus.ACTIVE,
                    Password = "1111",
                    FirstName = "Yehonatan",
                    LastName = "Eliyahu",
                    Phone = "100",
                    Email = "yedidi08@gmail.com",
                },
                new Person()
                {
                    Id = 1,
                    IdType =IdType.ID,
                    Status = PersonStatus.ACTIVE,
                    Password = "1",
                    FirstName = "yossi",
                    LastName = "Jaguar",
                    Phone = "102",
                    Email = "yedidi08@gmail.com",
                },
                new Person()
                {
                    Id = 201649035,
                    IdType =IdType.ID,
                    Status = PersonStatus.ACTIVE,
                    Password = "1",
                    FirstName = "dan",
                    LastName = "ziber",
                    Phone = "100",
                    Email = "yedidi08@gmail.com",
                }
            };
            hostsList = new List<Host>()
            {
                new Host()
                {
                     Id = 1,
                     AccountDetails=new BankBranch ()
                     {
                          AccountNumber=111,
                          BankName="Hapolim",
                          BranchAddress ="28",
                          BranchCity="jh",
                          BranchNumber=196,
                          Phone="0502026636"
                     },
                     CollectionClearance = true,
                     WebSite=  "xxx",
                     Status= ActivityStatus.ACTIVE
                },
                new Host()
                {
                    Id  = 204290689,
                    AccountDetails=new BankBranch ()
                    {
                         AccountNumber=111,
                         BankName="Hapolim",
                         BranchAddress ="28",
                         BranchCity="jh",
                         BranchNumber=196,
                         Phone="0502026636"
                    },
                    CollectionClearance = true,
                    WebSite=  "xxx",
                    Status= ActivityStatus.ACTIVE
                }
            };
            hostingUnitsList = new List<HostingUnit>()
            {
                    new HostingUnit()
                {
                    Key = 111,
                    HostId = 2,
                    HostingUnitName = "blue sky",
                    Diary = new bool[12, 31],
                    Status= ActivityStatus.ACTIVE,
                    Area= Areas.ירושלים,
                    pricePerNight = 150
                },
                new HostingUnit()
                {
                    Key = 222,
                    HostId = 2,
                    HostingUnitName = "blue sky",
                    Diary = new bool[12, 31],
                    Status= ActivityStatus.ACTIVE,
                    Area= Areas.ירושלים,
                    pricePerNight = 150
                },
                new HostingUnit()
                {
                    Key = 333,
                    HostId = 1,
                    HostingUnitName = "blue sky",
                    Diary = new bool[12, 31],
                    Status= ActivityStatus.ACTIVE,
                    Area= Areas.ירושלים,
                    pricePerNight = 150
                },
                new HostingUnit()
                {
                    Key = 123,
                    HostId = 1,
                    HostingUnitName = "blue sky",
                    Diary = new bool[12, 31],
                    Status= ActivityStatus.ACTIVE,
                    Area= Areas.ירושלים,
                    pricePerNight = 150
                },
                new HostingUnit()
                {
                    Key = 122,
                    HostId = 1,
                    HostingUnitName = "cool rooms",
                    Diary = new bool[12, 31],
                    Status= ActivityStatus.ACTIVE,
                    Area= Areas.מרכז,
                    pricePerNight = 350
                }
            };
            guestRequestList = new List<GuestRequest>()
            {
                new GuestRequest()
                {
                    Key = 1,
                    Status = RequestStatus.OPEN,
                    CreateDate = DateTime.Now,
                    EntryDate = new DateTime(2020, 2, 20),
                    LeaveDate = new DateTime(2020, 2, 25),
                    Area = Areas.מרכז,
                    SubArea = "0",
                    Type = HostingType.צימר,
                    Adults = 2,
                    Children = 0,
                    Pool = Answer.כן,
                    Jacuzzi = Answer.כן,
                    Garden = Answer.כן,
                    ChildrensAttractions = Answer.לא,
                    ClientId= 212282610,

                },
                new GuestRequest()
                {
                    Key = 3,
                    Status = RequestStatus.OPEN,
                    CreateDate = DateTime.Now,
                    EntryDate = new DateTime(2020, 2, 20),
                    LeaveDate = new DateTime(2020, 2, 25),
                    Area = Areas.ירושלים,
                    SubArea = "0",
                    Type = HostingType.צימר,
                    Adults = 6,
                    Children = 0,
                    Pool = Answer.כן,
                    Jacuzzi = Answer.כן,
                    Garden = Answer.כן,
                    ChildrensAttractions = Answer.לא,
                    ClientId= 212282610,

                },
                new GuestRequest()
                {
                    Key = 2,
                    Status = RequestStatus.OPEN,
                    CreateDate = DateTime.Now,
                    EntryDate = new DateTime(2020, 2, 24),
                    LeaveDate = new DateTime(2020, 2, 28),
                    Area = Areas.ירושלים,
                    SubArea = "0",
                    Type = HostingType.צימר,
                    Adults = 2,
                    Children = 5,
                    Pool = Answer.כן,
                    Jacuzzi = Answer.כן,
                    Garden = Answer.כן,
                    ChildrensAttractions = Answer.לא,
                    ClientId= 212282610
                },
                  new GuestRequest()
                {
                    Key = 3,
                    Status = RequestStatus.OPEN,
                    CreateDate = DateTime.Now,
                    EntryDate = new DateTime(2020, 2, 20),
                    LeaveDate = new DateTime(2020, 2, 25),
                    Area = Areas.ירושלים,
                    SubArea = "0",
                    Type = HostingType.צימר,
                    Adults = 6,
                    Children = 0,
                    Pool = Answer.כן,
                    Jacuzzi = Answer.כן,
                    Garden = Answer.כן,
                    ChildrensAttractions = Answer.לא,
                    ClientId= 212282610,

                },
                    new GuestRequest()
                {
                    Key = 4,
                    Status = RequestStatus.OPEN,
                    CreateDate = DateTime.Now,
                    EntryDate = new DateTime(2020, 2, 20),
                    LeaveDate = new DateTime(2020, 2, 25),
                    Area = Areas.ירושלים,
                    SubArea = "0",
                    Type = HostingType.צימר,
                    Adults = 6,
                    Children = 0,
                    Pool = Answer.כן,
                    Jacuzzi = Answer.כן,
                    Garden = Answer.כן,
                    ChildrensAttractions = Answer.לא,
                    ClientId= 212282610,

                },
                      new GuestRequest()
                {
                    Key = 5,
                    Status = RequestStatus.OPEN,
                    CreateDate = DateTime.Now,
                    EntryDate = new DateTime(2020, 2, 20),
                    LeaveDate = new DateTime(2020, 2, 25),
                    Area = Areas.ירושלים,
                    SubArea = "0",
                    Type = HostingType.צימר,
                    Adults = 6,
                    Children = 0,
                    Pool = Answer.כן,
                    Jacuzzi = Answer.כן,
                    Garden = Answer.כן,
                    ChildrensAttractions = Answer.לא,
                    ClientId= 201649035,

                },
                        new GuestRequest()
                {
                    Key = 6,
                    Status = RequestStatus.OPEN,
                    CreateDate = DateTime.Now,
                    EntryDate = new DateTime(2020, 2, 20),
                    LeaveDate = new DateTime(2020, 2, 25),
                    Area = Areas.ירושלים,
                    SubArea = "0",
                    Type = HostingType.צימר,
                    Adults = 6,
                    Children = 0,
                    Pool = Answer.כן,
                    Jacuzzi = Answer.כן,
                    Garden = Answer.כן,
                    ChildrensAttractions = Answer.לא,
                    ClientId= 201649035,

                },
                          new GuestRequest()
                {
                    Key = 7,
                    Status = RequestStatus.OPEN,
                    CreateDate = DateTime.Now,
                    EntryDate = new DateTime(2020, 2, 20),
                    LeaveDate = new DateTime(2020, 2, 25),
                    Area = Areas.ירושלים,
                    SubArea = "0",
                    Type = HostingType.צימר,
                    Adults = 6,
                    Children = 0,
                    Pool = Answer.כן,
                    Jacuzzi = Answer.כן,
                    Garden = Answer.כן,
                    ChildrensAttractions = Answer.לא,
                    ClientId= 201649035,

                },
                            new GuestRequest()
                {
                    Key = 8,
                    Status = RequestStatus.OPEN,
                    CreateDate = DateTime.Now,
                    EntryDate = new DateTime(2020, 2, 20),
                    LeaveDate = new DateTime(2020, 2, 25),
                    Area = Areas.ירושלים,
                    SubArea = "0",
                    Type = HostingType.צימר,
                    Adults = 6,
                    Children = 0,
                    Pool = Answer.כן,
                    Jacuzzi = Answer.כן,
                    Garden = Answer.כן,
                    ChildrensAttractions = Answer.לא,
                    ClientId= 201649035,

                },


            };
            bankBranchesList = new List<BankBranch>()
            {
                new BankBranch()
                {
                    BankName= "BOA",
                    AccountNumber= 11,
                    BranchAddress = "11213 Brooklyn NY",
                    BranchCity = "NYC baby",
                    BranchNumber= 196
                },
                new BankBranch()
                {
                    BankName= "BOA",
                    AccountNumber= 12,
                    BranchAddress = "11213 crown",
                    BranchCity = "atlanta",
                    BranchNumber= 999
                },
                new BankBranch()
                {
                    BankName= "BOA",
                    AccountNumber= 12,
                    BranchAddress = "11213 hollywood",
                    BranchCity = "los angels",
                    BranchNumber= 396
                },
                new BankBranch()
                {
                    BankName= "BOA",
                    AccountNumber= 12,
                    BranchAddress = "167 miami beach",
                    BranchCity = "miami",
                    BranchNumber= 543
                },
                new BankBranch()
                {
                    BankName= "BOA",
                    AccountNumber= 12,
                    BranchAddress = "768 vhj",
                    BranchCity = "boston",
                    BranchNumber= 770
                },
            };
            ordersList = new List<Order>()
            {
                new Order()
                {
                    Key = 5,
                    HostingUnitKey = 123,
                    GuestRequestKey = 1,
                    Status = OrderStatus.PENDING,
                    OrderDate =new DateTime(2019, 12, 23),

                    HostId=1,
                    CliendId=212282610,
                    Commission =0,
                    ClientName="elhanan",
                    HostingUnitName="נוף הגליל"
                },
                 new Order()
                {
                    Key = 6,
                    HostingUnitKey = 123,
                    GuestRequestKey = 1,
                    Status = OrderStatus.PENDING,
                    OrderDate =new DateTime(2019, 12, 23),

                    HostId=1,
                    CliendId=212282610,
                    Commission =0,
                    ClientName="elhanan",
                    HostingUnitName="נוף הגליל"
                },
                new Order()
                {
                    Key = 7,
                    HostingUnitKey = 122,
                    GuestRequestKey = 2,
                    Status = OrderStatus.PENDING,
                    OrderDate =new DateTime(2019, 12, 23),
                    HostId=1,
                    CliendId=201649035,
                    Commission =10,
                    ClientName="Yedidya",
                    HostingUnitName="שושנה ופרח"
                }
            };
        }
    }
}


