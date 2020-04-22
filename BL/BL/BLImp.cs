using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
using System.Net.Mail;

namespace BL
{
    class BLImp : IBl
    {
        readonly DalApi.IDal dal = DalApi.DalFactory.GetDal();

        public void AddHost(Host toadd)
        {
            try
            {
                dal.AddHost(Converters.Conv_BO_To_DO(toadd));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int AddHostingUinit(HostingUnit toAdd)
        {
            try
            {
                dal.AddHostingUnit(Converters.Conv_BO_To_DO(toAdd));
                return toAdd.Key;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void AddOrder(Order newOrder)
        {
            try
            {
                HostingUnit ExistsUnit = Converters.Conv_DO_To_BO(dal.GetHostingUnit(newOrder.HostingUnitKey));
                Host ExistsHost = Converters.Conv_DO_To_BO(dal.GetHost(newOrder.HostId));
                if (ExistsUnit == null || ExistsHost == null)
                {
                    throw new Exception("The unit or the host dosent exsist");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            try
            {
                GuestRequest request = GetRequest(newOrder.GuestRequestKey);
                HostingUnit unit = GetHoustingUinit(newOrder.HostingUnitKey);
                bool valid = CheckingAvailability(unit, request);
                if (!valid)
                {
                    throw new Exception("The dates you requested are not available ");
                }
                dal.AddOrder(Converters.Conv_BO_To_DO(newOrder));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void AddPerson(Person item)
        {
            try
            {
                dal.AddPerson(Converters.Conv_BO_To_DO(item));

            }
            catch (Exception e)
            {

                throw e;
            }

        }

        public int AddRequest(GuestRequest item)
        {
            if ((item.LeaveDate - item.EntryDate).Days < 1)
                throw new Exception("The exit must be dropped one day after entering");
            try
            {
                dal.AddGuestRequest(Converters.Conv_BO_To_DO(item));
            }
            catch (Exception e)
            {
                throw e;
            }
            return item.Key;
        }
        /// <summary>
        /// return all the units that available to date start+numofdays
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="numOfDays"></param>
        /// <returns></returns>
        public IEnumerable<HostingUnit> AvailableUnits(DateTime dateStart, int numOfDays)
        {
            GuestRequest temp = new GuestRequest()
            { EntryDate = dateStart, LeaveDate = dateStart.AddDays(numOfDays) };

            try
            {
                List<HostingUnit> lst = GetAllHostingUnits().ToList();
                lst.RemoveAll(x => CheckingAvailability(x, temp));
                return lst;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public void DisableHoustingUnit(int key)
        {
            try
            {
                IEnumerable<Order> lst = GetOrdersByUnits(key);
                if (lst.Count() == 0)
                {
                    dal.DisableHostingUnit(key);
                }
                else
                {
                    foreach (var item in lst)
                    {
                        if (item.Status == OrderStatus.MAIL_SENT || item.Status == OrderStatus.PROCESSING
                            || item.Status == OrderStatus.NO_CLIENT_RESPONSE)
                            throw new Exception("have open order, cennot delete this unit");
                    }
                    dal.DisableHostingUnit(key);
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public IEnumerable<GuestRequest> GetGuestRequestByPredicate(Func<GuestRequest, bool> func)
        {
            try
            {
                var requests = dal.GetGuestRequests(x => func(Converters.Conv_DO_To_BO(x)))
                    .Select(x => Converters.Conv_DO_To_BO(x));
                return requests;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Host GetHost(int id)
        {
            try
            {
                return Converters.Conv_DO_To_BO(dal.GetHost(id));
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public HostingUnit GetHoustingUinit(int key)
        {
            try
            {
                DO.HostingUnit tmp = dal.GetHostingUnit(key);
                return Converters.Conv_DO_To_BO(tmp);

            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public Order GetOrder(int key)
        {
            try
            {
                return Converters.Conv_DO_To_BO(dal.GetOrder(key));
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        public Person GetPerson(int id)
        {
            return Converters.Conv_DO_To_BO(dal.GetPerson(id));
        }

        public GuestRequest GetRequest(int key)
        {
            return Converters.Conv_DO_To_BO(dal.GetGuestRequest(key));
        }

        public int NumOfOrders(GuestRequest request)
        {
            return GetOrdersByRequestKey(request.Key).Count();
        }

        public int OfferSending(HostingUnit unit)
        {
            return dal.GetOrders(x => x.HostingUnitKey == unit.Key).Count();
        }

        public void UpdateHost(Host updateorder)
        {

            try
            {
                dal.UpdateHost(Converters.Conv_BO_To_DO(updateorder));
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public void UpdateHostingUnit(HostingUnit updateor)
        {
            try
            {
                dal.UpdateHostingUnit(Converters.Conv_BO_To_DO(updateor));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void UpdateStatusOrder(BO.OrderStatus updateorder, int key)
        {
            try
            {
                BO.Order tmp1 = Converters.Conv_DO_To_BO(dal.GetOrder(key));
                if (tmp1.Status == OrderStatus.IGNORED_CLOSED || tmp1.Status == OrderStatus.APPROVED)
                    throw new Exception("אין אפשרות לשנות את הסטטוס לאחר סגירת עסקה");
                BO.Host temp2 = Converters.Conv_DO_To_BO(dal.GetHost(tmp1.HostId));
                if (updateorder == OrderStatus.MAIL_SENT)
                {
                    if (!temp2.CollectionClearance)
                        throw new Exception("אין אפשרות לשלוח מייל ללקוח מבלי לחתום על הרשאה לחיוב חשבון");
                }
                if (updateorder == OrderStatus.APPROVED)
                {
                    BO.Order update = Converters.Conv_DO_To_BO(dal.GetOrder(key));
                    BO.HostingUnit unit = Converters.Conv_DO_To_BO(dal.GetHostingUnit(update.HostingUnitKey));
                    BO.GuestRequest request = Converters.Conv_DO_To_BO(dal.GetGuestRequest(update.GuestRequestKey));
                    SetDairy(unit, request);
                    dal.UpdateHostingUnit(Converters.Conv_BO_To_DO(unit));
                    update.ClientName = dal.GetPerson(update.ClientId).FirstName + dal.GetPerson(update.ClientId).LastName;
                    update.Commission = 10; //סכום העמלה
                    dal.UpdateOrder(Converters.Conv_BO_To_DO(update));
                    List<Order> lst = GetOrdersByRequestKey(request.Key).ToList();
                    foreach (var i in lst)//change all orders to Irrelevant
                    {
                        if (i.Key != update.Key)
                        {
                            dal.UpdateStatusOrder(i.Key, DO.OrderStatus.IRRELEVANT);
                        }
                    }
                    try
                    {
                        SendEmail(tmp1);
                    }
                    catch (Exception)
                    {
                        throw new Exception("Check your Internet connection");
                    }

                    ////////////////////////////////////////////////////////////////////////////////////////
                }
                dal.UpdateStatusOrder(key, (DO.OrderStatus)updateorder);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void UpdatePerson(Person toupdate)
        {
            try
            {
                dal.UpdatePerson(Converters.Conv_BO_To_DO(toupdate));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void UpdateRequest(GuestRequest toupdate)
        {
            try
            {
                dal.UpdateRequest(Converters.Conv_BO_To_DO(toupdate));
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public void UpdateRequestStatus(BO.RequestStatus status, int key)
        {
            try
            {
                dal.UpdateRequestStatus(key, (DO.RequestStatus)status);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// the funk return all guest request
        /// </summary>
        /// <returns></returns>
        public IEnumerable<GuestRequest> GetGuestRequests()
        {
            var requests = dal.GetGuestRequests().Select(x => Converters.Conv_DO_To_BO(x));
            return requests;
        }

        /// <summary>
        /// the func return all the orders that they have the same host
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<BO.Order> GetOrdersByHost(int id)
        {
            try
            {
                var orders = dal.GetOrders(x => x.HostId == id).Select(x => Converters.Conv_DO_To_BO(x));
                return orders;
            }
            catch (Exception e)
            {
                throw e;
            }
            //List<BO.Order> tmp = new List<BO.Order>();
            //foreach(var i in dal.GetOrders(i=>i.HostId== id))
            //{
            // tmp.Add(Converters.ConverFrom_DO_To_BoOrder(i));
            //}
            //return tmp;
        }
        /// <summary>
        /// the func return all the orders that they have the same hostingunit key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IEnumerable<BO.Order> GetOrdersByUnits(int key)
        {
            try
            {
                var orders = dal.GetOrders(x => x.HostingUnitKey == key).Select(x => Converters.Conv_DO_To_BO(x));
                return orders;
            }
            catch (Exception e)
            {
                throw e;
            }
            //List<BO.Order> tmp = new List<BO.Order>();
            //foreach(var i in dal.GetOrders(i=>i.HostId== id))
            //{
            // tmp.Add(Converters.ConverFrom_DO_To_BoOrder(i));
            //}
            //return tmp;
        }

        public IEnumerable<HostingUnit> GetHostingUnitsByHostId(int id)
        {
            try
            {
                var units = dal.GetHostingUnits(x => x.HostId == id).Select(x => Converters.Conv_DO_To_BO(x));
                return units;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<GuestRequest> GetGuestRequests(int id)
        {
            try
            {
                var requests = dal.GetGuestRequests(x => x.ClientId == id).Select(x => Converters.Conv_DO_To_BO(x));
                return requests;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int NumOfHosts()
        {
            return dal.NumOfHost();
        }

        public int DifDate(DateTime start)
        {
            return (DateTime.Now - start).Days;
        }

        public int DifDate(DateTime start, DateTime end)
        {
            return (end - start).Days;
        }

        public void DisableHost(int hostid)
        {
            try
            {
                dal.DisableHost(hostid);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// the func return list of orders by the guest request key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IEnumerable<Order> GetOrdersByRequestKey(int key)
        {
            try
            {
                var orders = dal.GetOrders(x => x.GuestRequestKey == key).Select(x => Converters.Conv_DO_To_BO(x));
                return orders;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        List<GuestRequest> IBl.GetGuestRequestByPredicate(Func<GuestRequest, bool> func)
        {
            List<GuestRequest> lst = GetGuestRequests().ToList();
            lst.RemoveAll(x => !func(x));
            return lst;
        }

        public List<Order> GetSuccededOrderder(HostingUnit item)
        {
            List<Order> lst = GetOrdersByUnits(item.Key).ToList();
            lst.RemoveAll(x => x.Status != OrderStatus.APPROVED);
            return lst;
        }

        public bool CheckingAvailability(HostingUnit unit, GuestRequest request)
        {
            for (DateTime entery = request.EntryDate; entery != request.LeaveDate; entery = entery.AddDays(1))
                if (unit.Diary[entery.Month - 1, entery.Day])
                    return false;
            return true;
        }

        public bool SetDairy(HostingUnit unit, GuestRequest request)
        {
            if (!CheckingAvailability(unit, request))
                return false;
            for (DateTime entery = request.EntryDate; entery != request.LeaveDate; entery = entery.AddDays(1))
            {
                unit.Diary[entery.Month - 1, entery.Day] = true;
            }
            return true;
        }

        public IEnumerable<HostingUnit> GetAllHostingUnits()
        {
            try
            {
                var units = dal.GetHostingUnits(x => x is DO.HostingUnit).Select(x => Converters.Conv_DO_To_BO(x));
                return units;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// פונקציה שמחזירה את כל ההזמנות שתאריך שליחת המיל שלהם היה לפני כמות הימים שהפונקציה קיבלה 
        /// </summary>
        /// <param name="numOfDays"></param>
        /// <returns></returns>
        public IEnumerable<Order> Validity(int numOfDays)
        {
            try
            {
                List<Order> lst = GetOrdersList().ToList();
                DateTime item = DateTime.Now.AddDays(-numOfDays);
                lst.RemoveAll(x => x.SentDate < item);
                return lst;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Order> GetOrdersList()
        {

            try
            {
                var orders = dal.GetOrders(x => x is DO.Order).Select(x => Converters.Conv_DO_To_BO(x));
                return orders;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public void UpdateOrder(Order updateor)
        {
            try
            {
                dal.UpdateOrder(Converters.Conv_BO_To_DO(updateor));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<IGrouping<int, GuestRequest>> GetGroupGuestRequestsByNumOfVacationers()
        {
            return from guestR in dal.GetGuestRequests()
                   let guestBo = Converters.Conv_DO_To_BO(guestR)
                   group guestBo by guestBo.Adults + guestBo.Children
                    into allGropsGuestRequests
                   select allGropsGuestRequests;
            // throw new NotImplementedException();
        }

        public IEnumerable<IGrouping<int, Host>> GetGroupHostByNumOfHostingUnits()
        {
            return from host in dal.GetHostList()
                   let hostBo = Converters.Conv_DO_To_BO(host)
                   group hostBo by hostBo.MyUnits.Count()
                         into allGropsHost
                   select allGropsHost;
            // throw new NotImplementedException();
        }

        public IEnumerable<IGrouping<Areas, HostingUnit>> GetGroupHostingUnitsByArea()
        {
            return from hostingUnit in dal.GetHostingUnits(x => x is DO.HostingUnit)
                   let hostingUnitBo = Converters.Conv_DO_To_BO(hostingUnit)
                   group hostingUnitBo by hostingUnitBo.Area
                    into allGropsHostingUnit
                   select allGropsHostingUnit;
        }

        public IEnumerable<IGrouping<Areas, GuestRequest>> GetGroupGuestRequestsByArea()
        {
            return from guestR in dal.GetGuestRequests()
                   let guestBo = Converters.Conv_DO_To_BO(guestR)
                   group guestBo by guestBo.Area
                    into allGropsGuestRequests
                   select allGropsGuestRequests;
        }

        public void SendEmail(Order order)
        {
            try
            {
                GuestRequest guest = GetRequest(order.GuestRequestKey);
                Person client = GetPerson(order.ClientId);
                MailMessage mail = new MailMessage();
                mail.To.Add(client.Email);
                mail.From = new MailAddress("yedidi08@gmail.com", "צימרים כיד המלך");
                mail.Subject = "פרטי הזמנת חופשה";
                mail.Body = string.Format(@" שלום {0} הזמנתך נקלטה במערכת
פרטי ההזמנה: 
מתאריך:  {1} 
עד תאריך:    {2}
לאישור צור עמנו קשר 
מחכים לבואך!!!", order.ClientName, guest.EntryDate.ToShortDateString(), guest.LeaveDate.ToShortDateString());
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Credentials = new System.Net.NetworkCredential("dotnet5780@gmail.com", "rede24@@");
                smtp.EnableSsl = true;
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
