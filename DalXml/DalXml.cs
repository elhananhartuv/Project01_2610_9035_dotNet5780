using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using DO;
using DalApi;
//using Dal;
using System.Reflection;

namespace Dal
{
    sealed class DalXml : IDal
    {
        PersonHandler personHandler = new PersonHandler();
        HostHandler hostHandler = new HostHandler();
        HostingUnitHandler hostingUnitHandler = new HostingUnitHandler();
        GuestRequestHandler guestRequestHandler = new GuestRequestHandler();
        OrderHandler orderHandler = new OrderHandler();
        Configuration config = new Configuration();

        public static List<Person> persons = new List<Person>();
        public static List<Host> hosts = new List<Host>();
        public static List<HostingUnitXml> hostingUnits = new List<HostingUnitXml>();
        public static List<GuestRequest> guestRequests = new List<GuestRequest>();
        public static List<Order> orders = new List<Order>();

        public event Action<Dictionary<string, object>> configHandler;
        private Dictionary<string, string> configDic;

        public Dictionary<string, string> Config
        {
            get { return configDic; }
            set { configDic = value; }
        }


        private DalXml()
        {
            if (!File.Exists(@"Person.xml"))
                personHandler.Create();
            if (!File.Exists(@"Host.xml"))
                hostHandler.Create();
            if (!File.Exists(@"HostingUnit.xml"))
                hostingUnitHandler.Create();
            if (!File.Exists(@"GuestRequest.xml"))
                guestRequestHandler.Create();
            if (!File.Exists(@"Order.xml"))
                orderHandler.Create();
            configDic = config.LoadConfig();
        }
        public static DalXml Instance { get; } = new DalXml();

        public Person GetPerson(int id)
        {
            personHandler.Load();
            Person findPerson = (from person in persons
                                 where person.Id == id
                                 select person).FirstOrDefault();
            if (findPerson == null)
                throw new Exception("הלקוח אינו רשום למערכת");
            return findPerson.Clone();
        }

        public bool AddPerson(Person item)
        {
            personHandler.Load();
            Person findPerson = (from person in persons
                                 where person.Id == item.Id
                                 select person).FirstOrDefault();
            if (findPerson == null)
            {
                persons.Add(item.Clone());
                personHandler.Save();
                return true;
            }
            else
                throw new Exception("הלקוח כבר קיים במערכת");
        }

        public bool UpdatePerson(Person item)
        {
            personHandler.Load();
            Person findPerson = (from person in persons
                                 where person.Id == item.Id
                                 select person).FirstOrDefault();
            if (findPerson != null)
            {
                persons.Remove(findPerson);
                persons.Add(item.Clone());
                personHandler.Save();
                return true;
            }
            else
                throw new Exception("הלקוח אינו קיים במערכת");
        }

        public GuestRequest GetGuestRequest(int key)
        {
            guestRequestHandler.Load();
            GuestRequest findRequest = (from request in guestRequests
                                        where request.Key == key
                                        select request).FirstOrDefault();
            if (findRequest == null)
                throw new Exception("הבקשה אינה קיימת במערכת");
            return findRequest.Clone();
        }

        /// <summary>
        /// return the key of the new guest request
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int AddGuestRequest(GuestRequest item)
        {
            guestRequestHandler.Load();
            GuestRequest findRequest = (from request in guestRequests
                                        where request.Key == item.Key
                                        select request).FirstOrDefault();
            if (findRequest == null || item.Key == 0)
            {
                Config = config.LoadConfig();
                item.Key = int.Parse(Config["GuestRequestKey"]);
                guestRequests.Add(item.Clone());
                guestRequestHandler.Save();
                setConfig("GuestRequestKey", (item.Key + 1).ToString());
                return item.Key;
            }
            throw new Exception("הבקשה כבר קיימת במערכת");
        }

        /// <summary>
        /// the func checked the status
        /// request with sttus that difference from NEW could not can to update
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool UpdateRequest(GuestRequest item)
        {
            guestRequestHandler.Load();
            GuestRequest findRequest = (from request in guestRequests
                                        where request.Key == item.Key
                                        select request).FirstOrDefault();
            if (findRequest == null)
                throw new Exception("העדכון לא הצליח - הבקשה אינה קיימת במערכת");
            guestRequests.Remove(findRequest);
            guestRequests.Add(item.Clone());
            guestRequestHandler.Save();
            return true;
        }

        public bool UpdateRequestStatus(int key, RequestStatus newStatus)
        {
            guestRequestHandler.Load();
            GuestRequest findRequest = (from request in guestRequests
                                        where request.Key == key
                                        select request).FirstOrDefault();
            if (findRequest != null)
            {
                findRequest.Status = newStatus;
                guestRequestHandler.Save();
                return true;
            }
            throw new Exception("העדכון לא הצליח - הבקשה אינה קיימת במערכת");
        }

        public Host GetHost(int id)
        {
            hostHandler.Load();
            Host findHost = (from host in hosts
                             where host.Id == id
                             select host).FirstOrDefault();
            if (findHost == null)
                throw new Exception("המארח אינו קיים במערכת");
            return findHost.Clone();
        }

        public bool AddHost(Host item)
        {
            hostHandler.Load();
            Host findHost = (from host in hosts
                             where host.Id == item.Id
                             select host).FirstOrDefault();
            if (findHost == null)
            {
                hosts.Add(item.Clone());
                hostHandler.Save();
                return true;
            }
            if (findHost != null && findHost.Status == DO.ActivityStatus.INACTIVE)
            {
                findHost.Status = DO.ActivityStatus.ACTIVE;
                hostHandler.Save();
                return true;
            }
            throw new Exception("המארח כבר קיים במערכת");
        }

        public bool UpdateHost(Host item)
        {
            hostHandler.Load();
            Host findHost = (from host in hosts
                             where host.Id == item.Id
                             select host).FirstOrDefault();
            if (findHost != null)
            {
                hosts.Remove(findHost);
                hosts.Add(item.Clone());
                hostHandler.Save();
                return true;
            }
            throw new Exception("המארח אינו קיים במערכת");
        }

        public bool DisableHost(int id)
        {
            hostHandler.Load();
            Host findHost = (from host in hosts
                             where host.Id == id
                             select host).FirstOrDefault();
            if (findHost != null)
            {
                findHost.Status = DO.ActivityStatus.INACTIVE;
                hostHandler.Save();
                return true;
            }
            throw new Exception("המארח אינו קיים במערכת");
        }

        public HostingUnit GetHostingUnit(int key)
        {
            hostingUnitHandler.Load();
            HostingUnitXml findHostingUnit = (from unit in hostingUnits
                                              where unit.Key == key
                                              select unit).FirstOrDefault();
            if (findHostingUnit != null && findHostingUnit.Status == ActivityStatus.ACTIVE)
                return Tools.Conv_Xml_To_DO(findHostingUnit.Clone());
            throw new Exception("היחידה אינה קיימת במערכת");///need to check if hosting unit status is active?????          
        }

        public int AddHostingUnit(HostingUnit item)
        {
            hostingUnitHandler.Load();
            HostingUnitXml findHostingUnit = (from unit in hostingUnits
                                              where unit.Key == item.Key
                                              select unit).FirstOrDefault();
            if (findHostingUnit == null)
            {
                Config = config.LoadConfig();
                item.Key = int.Parse(Config["HostingUnitKey"]);
                hostingUnits.Add(Tools.conv_DO_To_Xml(item.Clone()));
                hostingUnitHandler.Save();
                setConfig("HostingUnitKey", (item.Key + 1).ToString());
                return item.Key;
            }
            if (findHostingUnit != null && findHostingUnit.Status == ActivityStatus.INACTIVE)
            {
                findHostingUnit.Status = ActivityStatus.ACTIVE;
                hostingUnitHandler.Save();
                return findHostingUnit.Key;
            }
            throw new Exception("יחידת האירוח כבר קיימת במערכת");
        }

        public bool DisableHostingUnit(int key)
        {
            hostingUnitHandler.Load();
            HostingUnitXml findHostingUnit = (from unit in hostingUnits
                                              where unit.Key == key
                                              select unit).FirstOrDefault();
            if (findHostingUnit != null)
            {
                findHostingUnit.Status = ActivityStatus.INACTIVE;
                hostingUnitHandler.Save();
                return true;
            }
            throw new Exception("היחידה אינה קיימת במערכת");
        }

        public bool UpdateHostingUnit(HostingUnit item)
        {
            hostingUnitHandler.Load();
            HostingUnitXml findHostingUnit = (from unit in hostingUnits
                                              where unit.Key == item.Key
                                              select unit).FirstOrDefault();
            if (findHostingUnit != null)
            {
                hostingUnits.Remove(findHostingUnit);
                hostingUnits.Add(Tools.conv_DO_To_Xml(item.Clone()));
                hostingUnitHandler.Save();
                return true;
            }
            throw new Exception("היחידה אינה קיימת במערכת");
        }

        public Order GetOrder(int key)
        {
            orderHandler.Load();
            Order findOrder = (from order in orders
                               where order.Key == key
                               select order).FirstOrDefault();
            if (findOrder != null)
                return findOrder.Clone();
            throw new Exception("ההזמנה אינה קיימת במערכת");
        }

        public int AddOrder(Order item)
        {
            orderHandler.Load();
            Order findOrder = (from order in orders
                               where order.Key == item.Key
                               select order).FirstOrDefault();
            if (findOrder == null)
            {
                Config = config.LoadConfig();
                item.Key = int.Parse(Config["OrderKey"]);
                GuestRequest temp = Instance.GetGuestRequest(item.GuestRequestKey);
                item.Commission = int.Parse(Config["Commission"]) * ((temp.LeaveDate - temp.EntryDate).Days);
                orders.Add(item.Clone());
                orderHandler.Save();
                setConfig("OrderKey", (item.Key + 1).ToString());
                return item.Key;
            }
            throw new Exception("ההזמנה כבר קיימת במערכת");
        }

        public bool UpdateOrder(Order item)
        {
            orderHandler.Load();
            Order findOrder = (from order in orders
                               where order.Key == item.Key
                               select order).FirstOrDefault();
            if (findOrder != null)
            {
                orders.Remove(findOrder);
                orders.Add(item.Clone());
                orderHandler.Save();
                return true;
            }
            throw new Exception("ההזמנה אינה קיימת במערכת");
        }

        public bool UpdateStatusOrder(int key, OrderStatus status)
        {
            orderHandler.Load();
            Order findOrder = (from order in orders
                               where order.Key == key
                               select order).FirstOrDefault();
            if (findOrder != null)
            {
                findOrder.Status = status;
                if (status == OrderStatus.MAIL_SENT)
                    findOrder.SentDate = DateTime.Now;
                if (status == OrderStatus.APPROVED)
                    findOrder.CloseDate = DateTime.Now;
                orderHandler.Save();
                return true;
            }
            throw new Exception("ההזמנה אינה קיימת במערכת");
        }

        public IEnumerable<HostingUnit> GetHostingUnits(Func<HostingUnit, bool> predicate)
        {
            hostingUnitHandler.Load();
            return (from unit in hostingUnits
                    where predicate(Tools.Conv_Xml_To_DO(unit))
                    select Tools.Conv_Xml_To_DO(unit)).ToList();
        }

        public IEnumerable<GuestRequest> GetGuestRequests()
        {
            guestRequestHandler.Load();
            return guestRequests.Select(e => e.Clone()).ToList();
            /*
            List<GuestRequest> lst = new List<GuestRequest>();
            foreach (var i in DataSource.guestRequestList)
                lst.Add(i.Clone());
            return lst;
            */
        }

        public IEnumerable<Order> GetOrders(Func<Order, bool> predicate)
        {
            orderHandler.Load();
            return (from order in orders
                    where predicate(order)
                    select order.Clone()).ToList();
        }

        public IEnumerable<BankBranch> GetBankBranches()
        {
            List<BankBranch> lst = new List<BankBranch>()
            {
                new BankBranch
                {
                    BankName = "bank mizrahi",
                    BankNumber = 20,
                    BranchAddress = "tarfon 1",
                    BranchCity = "bnei brak",
                    BranchNumber = 430,
                    Phone = "0506677889"
                },
                new BankBranch
                {
                    BankName = "bank hapoalim",
                    BankNumber = 12,
                    BranchAddress = "kuk 35",
                    BranchCity = "bnei brak",
                    BranchNumber = 856,
                    Phone = "056789456"
                },
                new BankBranch
                {
                    BankName = "bank markantil",
                    BankNumber = 17,
                    BranchAddress = "akiva 23",
                    BranchCity = "bnei brak",
                    BranchNumber = 715,
                    Phone = "0523456789"
                },
                new BankBranch
                {
                    BankName = "bank pagi",
                    BankNumber = 52,
                    BranchAddress = "akiva 75",
                    BranchCity = "bnei brak",
                    BranchNumber = 183,
                    Phone = "056456456"
                },
                new BankBranch
                {
                    BankName = "bank discont",
                    BankNumber = 11,
                    BranchAddress = "kanfey nesharin 1",
                    BranchCity = "jerusalem",
                    BranchNumber = 565,
                    Phone = "0543213213"
                }
            };
            return lst;
            //return DataSource.bankBranchesList.Select(e => e.Clone()).ToList();
        }

        public IEnumerable<GuestRequest> GetGuestRequests(Func<GuestRequest, bool> predicate)
        {
            guestRequestHandler.Load();
            return (from request in guestRequests
                    where predicate(request)
                    select request.Clone()).ToList();
        }

        public IEnumerable<Host> GetHostList()
        {
            hostHandler.Load();
            return (from host in hosts
                    select host.Clone()).ToList();
        }

        public int NumOfHost()
        {
            hostHandler.Load();
            return hosts.Count();
        }

        public IEnumerable<Person> GetPersons()
        {
            personHandler.Load();
            return (from person in persons
                    select person.Clone()).ToList();
        }

        public Dictionary<string, string> getConfig()
        {
            return config.LoadConfig();
        }

        public void setConfig(string key, string value)
        {
            Config[key] = value;
            config.SaveConfig(Config);
        }
    }
}
