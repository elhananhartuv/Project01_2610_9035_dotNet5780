using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BlApi
{
    public interface IBl
    {
        //void AddBank(BankBranchBO toadd);
        //BankBranchBO GetBank(int key);
        // Order CreateOrder();
        /// <summary>
        /// the function return all the the ordrs
        /// </summary>
        /// <returns></returns>
        IEnumerable<Host> GetHostList();
        IEnumerable<Order> GetOrdersList();
        IEnumerable<Order> Validity(int numOfDays);
        bool SetDairy(HostingUnit unit, GuestRequest request);
        bool CheckingAvailability(HostingUnit unit, GuestRequest request);
        IEnumerable<BO.Order> GetOrdersByUnits(int key);
        int NumOfHosts();
        void SendEmail(Order order);
        void AddPerson(Person item);
        void UpdateRequestStatus(RequestStatus status, int key);
        void UpdatePerson(Person toupdate);
        int AddRequest(GuestRequest item);
        void UpdateRequest(GuestRequest toupdate);
        GuestRequest GetRequest(int key);
        Host GetHost(int id);
        void AddHost(Host name);
        void DisableHost(int hostid);
        void UpdateHost(Host updateorder);
        Order GetOrder(int key);
        int AddOrder(Order newOrder);
        void UpdateStatusOrder(OrderStatus updateorder, int key);
        void UpdateOrder(Order updateor);
        HostingUnit GetHoustingUinit(int key);
        IEnumerable<HostingUnit> GetAllHostingUnits();
        int AddHostingUinit(HostingUnit toAdd);
        void DisableHoustingUnit(int key);
        void UpdateHostingUnit(HostingUnit updateor);
        Person GetPerson(int id);
        /// <summary>
        /// return all the hosting units that they have the same owner
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IEnumerable<BO.HostingUnit> GetHostingUnitsByHostId(int id);
        IEnumerable<BO.GuestRequest> GetGuestRequests();
        IEnumerable<BO.GuestRequest> GetGuestRequests(int id);
        IEnumerable<BO.Order> GetOrdersByHost(int id);

        /////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// function that chek housting unit avilable  start date and num of hosting
        /// </summaryD>
        /// <param name="dateStart"></param>
        /// <param name="numOfDays"></param>
        /// <returns></returns>
        IEnumerable<HostingUnit> AvailableUnits(DateTime dateStart, int numOfDays);

        /// <summary>
        /// calculate the diference betwen start to today
        /// </summary>
        /// <param name="start"></param>
        /// <returns></returns>
        int DifDate(DateTime start);

        /// <summary>
        /// calculate the diference betwen start to end
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        int DifDate(DateTime start, DateTime end);




        /// <summary>
        /// return the number of offers
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        int NumOfOrders(GuestRequest request);
        IEnumerable<BO.Order> GetOrdersByRequestKey(int key);
        int OfferSending(HostingUnit unit);
        List<GuestRequest> GetGuestRequestByPredicate(Func<GuestRequest, bool> func);
        //////////////////////////////////////////////////////////////
        IEnumerable<IGrouping<Areas, GuestRequest>> GetGroupGuestRequestsByArea();
        IEnumerable<IGrouping<int, GuestRequest>> GetGroupGuestRequestsByNumOfVacationers();
        IEnumerable<IGrouping<int, Host>> GetGroupHostByNumOfHostingUnits();
        IEnumerable<IGrouping<Areas, HostingUnit>> GetGroupHostingUnitsByArea();
        List<Order> GetSuccededOrderder(HostingUnit item);
        IEnumerable<Person> GetPersons();       
    }
}
