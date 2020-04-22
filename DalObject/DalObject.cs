using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;
using DS;

namespace Dal
{
    internal sealed class DalObject : IDal
    {
        private DalObject() { }
        public static DalObject Instance { get; } = new DalObject();

        public event Action<Dictionary<string, object>> configHandler;

        public Person GetPerson(int id)
        {
            Person findPerson = (from person in DataSource.personList
                                 where person.Id == id
                                 select person).FirstOrDefault();
            if (findPerson == null)
                throw new Exception("the Person is not exsist");
            return findPerson.Clone();
        }

        public bool AddPerson(Person item)
        {
            Person findPerson = (from person in DataSource.personList
                                 where person.Id == item.Id
                                 select person).FirstOrDefault();
            if (findPerson == null)
            {
                DataSource.personList.Add(item.Clone());
                return true;
            }
            else
                throw new Exception("the Person is already exsist");
        }

        public bool UpdatePerson(Person item)
        {
            Person findPerson = (from person in DataSource.personList
                                 where person.Id == item.Id
                                 select person).FirstOrDefault();
            if (findPerson != null)
            {
                DataSource.personList.Remove(findPerson);
                DataSource.personList.Add(item.Clone());
                return true;
            }
            else
                throw new Exception("the Person is not exsist");
        }

        public GuestRequest GetGuestRequest(int key)
        {
            GuestRequest findRequest = (from request in DataSource.guestRequestList
                                        where request.Key == key
                                        select request).FirstOrDefault();
            if (findRequest == null)
                throw new Exception("the request is not exsist");
            return findRequest.Clone();
        }

        /// <summary>
        /// return the key of the new guest request
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int AddGuestRequest(GuestRequest item)
        {
            GuestRequest findRequest = (from request in DataSource.guestRequestList
                                        where request.Key == item.Key
                                        select request).FirstOrDefault();
            if (findRequest == null)
            {
                item.Key = ++Configuretion.setGuestRequestKey;
                DataSource.guestRequestList.Add(item.Clone());
                return item.Key;
            }
            throw new Exception("the request is already exsist");
        }

        /// <summary>
        /// the func checked the status
        /// request with sttus that difference from NEW could not can to update
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool UpdateRequest(GuestRequest item)
        {
            GuestRequest findRequest = (from request in DataSource.guestRequestList
                                        where request.Key == item.Key
                                        select request).FirstOrDefault();
            if (findRequest == null)
                throw new Exception("the update is not succeded - the request is not exist");
            DataSource.guestRequestList.Remove(findRequest);
            DataSource.guestRequestList.Add(item.Clone());
            return true;
        }

        public bool UpdateRequestStatus(int key, RequestStatus newStatus)
        {
            GuestRequest findRequest = (from request in DataSource.guestRequestList
                                        where request.Key == key
                                        select request).FirstOrDefault();
            if (findRequest != null)
            {
                findRequest.Status = newStatus;
                return true;
            }
            throw new Exception("the update is not succeded - the request is not exist");
        }

        public Host GetHost(int id)
        {
            Host findHost = (from host in DataSource.hostsList
                             where host.Id == id
                             select host).FirstOrDefault();
            if (findHost == null)
                throw new Exception("the Host is not exsist");
            return findHost.Clone();
        }

        public bool AddHost(Host item)
        {
            Host findHost = (from host in DataSource.hostsList
                             where host.Id == item.Id
                             select host).FirstOrDefault();
            if (findHost == null)
            {
                DataSource.hostsList.Add(item.Clone());
                return true;
            }
            if (findHost != null && findHost.Status == ActivityStatus.INACTIVE)
            {
                findHost.Status = ActivityStatus.ACTIVE;
                return true;
            }
            throw new Exception("the Host is already exsist");
        }

        public bool UpdateHost(Host item)
        {
            Host findHost = (from host in DataSource.hostsList
                             where host.Id == item.Id
                             select host).FirstOrDefault();
            if (findHost != null)
            {
                DataSource.hostsList.Remove(findHost);
                DataSource.hostsList.Add(item.Clone());
                return true;
            }
            throw new Exception("the Host is not exsist");
        }

        public bool DisableHost(int id)
        {
            Host findHost = (from host in DataSource.hostsList
                             where host.Id == id
                             select host).FirstOrDefault();
            if (findHost != null)
            {
                findHost.Status = ActivityStatus.INACTIVE;
                return true;
            }
            throw new Exception("the Host is not exsist");
        }

        public HostingUnit GetHostingUnit(int key)
        {
            HostingUnit findHostingUnit = (from unit in DataSource.hostingUnitsList
                                           where unit.Key == key
                                           select unit).FirstOrDefault();
            if (findHostingUnit != null && findHostingUnit.Status == ActivityStatus.ACTIVE)
                return findHostingUnit.Clone();
            throw new Exception("This Hosting unit us not exist");///need to check if hosting unit status is active?????
        }

        public int AddHostingUnit(HostingUnit item)
        {
            HostingUnit findHostingUnit = (from unit in DataSource.hostingUnitsList
                                           where unit.Key == item.Key
                                           select unit).FirstOrDefault();
            if (findHostingUnit == null)
            {
                item.Key = ++Configuretion.setHostingUnitKey;
                DataSource.hostingUnitsList.Add(item.Clone());
                return item.Key;
            }
            if (findHostingUnit != null && findHostingUnit.Status == ActivityStatus.INACTIVE)
            {
                findHostingUnit.Status = ActivityStatus.ACTIVE;
                return findHostingUnit.Key;
            }
            throw new Exception("This Hosting unit is alrady exist");          
        }

        public bool DisableHostingUnit(int key)
        {
            HostingUnit findHostingUnit = (from unit in DataSource.hostingUnitsList
                                           where unit.Key == key
                                           select unit).FirstOrDefault();
            if (findHostingUnit != null)
            {
                findHostingUnit.Status = ActivityStatus.INACTIVE;
                return true;
            }
            throw new Exception("This Hosting unit is not exist");
        }

        public bool UpdateHostingUnit(HostingUnit item)
        {
            HostingUnit findHostingUnit = (from unit in DataSource.hostingUnitsList
                                           where unit.Key == item.Key
                                           select unit).FirstOrDefault();
            if (findHostingUnit != null)
            {
                DataSource.hostingUnitsList.Remove(findHostingUnit);
                DataSource.hostingUnitsList.Add(item.Clone());
                return true;
            }
            throw new Exception("This Hosting unit is not exist");
        }

        public Order GetOrder(int key)
        {
            Order findOrder = (from order in DataSource.ordersList
                               where order.Key == key
                               select order).FirstOrDefault();
            if (findOrder != null)
                return findOrder.Clone();
            throw new Exception("The order is not exist");
        }

        public int AddOrder(Order item)
        {
            Order findOrder = (from order in DataSource.ordersList
                               where order.Key == item.Key
                               select order).FirstOrDefault();
            if (findOrder == null)
            {
                item.Key = ++Configuretion.setOrderKey;
                DataSource.ordersList.Add(item.Clone());
                return item.Key;
            }
            throw new Exception("The order is alrady exist");
        }

        public bool UpdateOrder(Order item)
        {
            Order findOrder = (from order in DataSource.ordersList
                               where order.Key == item.Key
                               select order).FirstOrDefault();
            if (findOrder != null)
            {
                DataSource.ordersList.Remove(findOrder);
                DataSource.ordersList.Add(item.Clone());
                return true;
            }
            throw new Exception("The Order is not exist");
        }

        public bool UpdateStatusOrder(int key, OrderStatus status)
        {
            Order findOrder = (from order in DataSource.ordersList
                               where order.Key == key
                               select order).FirstOrDefault();
            if (findOrder != null)
            {
                findOrder.Status = status;
                return true;
            }
            throw new Exception("The Order is not exist");
        }

        public IEnumerable<HostingUnit> GetHostingUnits(Func<HostingUnit, bool> predicate)
        {
            return (from unit in DataSource.hostingUnitsList
                    where predicate(unit)
                    select unit.Clone()).ToList();
        }

        public IEnumerable<GuestRequest> GetGuestRequests()
        {
            return DataSource.guestRequestList.Select(e => e.Clone()).ToList();
            /*
            List<GuestRequest> lst = new List<GuestRequest>();
            foreach (var i in DataSource.guestRequestList)
                lst.Add(i.Clone());
            return lst;
            */
        }

        public IEnumerable<Order> GetOrders(Func<Order, bool> predicate)
        {
            return (from order in DataSource.ordersList
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
            return (from request in DataSource.guestRequestList
                    where predicate(request)
                    select request.Clone()).ToList();
        }

        public IEnumerable<Host> GetHostList()
        {
            List<Host> lst = new List<Host>();
            foreach (var i in DataSource.hostsList)
                lst.Add(i.Clone());
            return lst;
        }

        public int NumOfHost()
        {
            return DataSource.hostsList.Count();
        }

        public IEnumerable<Person> GetPersons()
        {
            return (from person in DataSource.personList                   
                    select person.Clone()).ToList();
        }

        public Dictionary<string, string> getConfig()
        {
            throw new NotImplementedException();
        }

        public void setConfig(string key, string value)
        {
            throw new NotImplementedException();
        }
    }
}
