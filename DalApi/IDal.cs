using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DalApi
{
    public interface IDal
    {
        Person GetPerson(int id);
        bool AddPerson(Person item);
        bool UpdatePerson(Person item);

        GuestRequest GetGuestRequest(int key);
        int AddGuestRequest(GuestRequest item);
        bool UpdateRequest(GuestRequest item);
        bool UpdateRequestStatus(int key, RequestStatus item);
        
        Host GetHost(int id);
        bool AddHost(Host item);
        bool UpdateHost(Host item);
        bool DisableHost(int id);

        HostingUnit GetHostingUnit(int key);
        int AddHostingUnit(HostingUnit item);
        bool DisableHostingUnit(int key);
        bool UpdateHostingUnit(HostingUnit item);

        Order GetOrder(int key);
        int AddOrder(Order item);
        bool UpdateOrder(Order item);
        bool UpdateStatusOrder(int key, OrderStatus status);

        IEnumerable<HostingUnit> GetHostingUnits(Func<HostingUnit, bool> predicate);
        IEnumerable<GuestRequest> GetGuestRequests();
        IEnumerable<Order> GetOrders(Func<Order, bool> predicate);
        IEnumerable<BankBranch> GetBankBranches();
        IEnumerable<GuestRequest> GetGuestRequests(Func<GuestRequest, bool> predicate);
        IEnumerable<Host> GetHostList();
        int NumOfHost();
        IEnumerable<Person> GetPersons();
        Dictionary<string, string> getConfig();
        void setConfig(string key, string value);
        event Action<Dictionary<string, object>> configHandler;
    }
}
