using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BL
{
    class Converters
    {
        static BLImp Instance = new BLImp();

        public static BO.BankBranch Conv_DO_To_BO(DO.BankBranch item)
        {
            BO.BankBranch temp = new BO.BankBranch();
            temp.BankName = item.BankName;
            temp.BankNumber = item.BankNumber;
            temp.BranchAddress = item.BranchAddress;
            temp.BranchCity = item.BranchCity;
            temp.BranchNumber = item.BranchNumber;
            temp.Phone = item.Phone;
            temp.AccountNumber = item.AccountNumber;
            return temp;
        }

        public static DO.BankBranch Conv_BO_To_DO(BO.BankBranch item)
        {
            DO.BankBranch temp = new DO.BankBranch();
            temp.BankName = item.BankName;
            temp.BankNumber = item.BankNumber;
            temp.BranchAddress = item.BranchAddress;
            temp.BranchCity = item.BranchCity;
            temp.BranchNumber = item.BranchNumber;
            temp.Phone = item.Phone;
            temp.AccountNumber = item.AccountNumber;
            return temp;
        }

        public static BO.GuestRequest Conv_DO_To_BO(DO.GuestRequest item)
        {
            BO.GuestRequest temp = new BO.GuestRequest();
            temp.Adults = item.Adults;
            temp.Area = (BO.Areas)item.Area;
            temp.Children = item.Children;
            temp.ChildrensAttractions = (BO.Answer)item.ChildrensAttractions;
            temp.ClientId = item.ClientId;
            temp.CreateDate = item.CreateDate;
            temp.EntryDate = item.EntryDate;
            temp.Garden = (BO.Answer)item.Garden;
            temp.Jacuzzi = (BO.Answer)item.Jacuzzi;
            temp.Key = item.Key;
            temp.LeaveDate = item.LeaveDate;
            temp.Pool = (BO.Answer)item.Pool;
            temp.Rooms = item.Rooms;
            temp.Status = (BO.RequestStatus)item.Status;
            temp.SubArea = item.SubArea;
            temp.Type = (BO.HostingType)item.Type;
            return temp;
        }
        public static DO.GuestRequest Conv_BO_To_DO(BO.GuestRequest item)
        {
            DO.GuestRequest temp = new DO.GuestRequest();
            temp.Adults = item.Adults;
            temp.Area = (DO.Areas)item.Area;
            temp.Children = item.Children;
            temp.ChildrensAttractions = (DO.Answer)item.ChildrensAttractions;
            temp.ClientId = item.ClientId;
            temp.CreateDate = item.CreateDate;
            temp.EntryDate = item.EntryDate;
            temp.Garden = (DO.Answer)item.Garden;
            temp.Jacuzzi = (DO.Answer)item.Jacuzzi;
            temp.Key = item.Key;
            temp.LeaveDate = item.LeaveDate;
            temp.Pool = (DO.Answer)item.Pool;
            temp.Rooms = item.Rooms;
            temp.Status = (DO.RequestStatus)item.Status;
            temp.SubArea = item.SubArea;
            temp.Type = (DO.HostingType)item.Type;
            return temp;
        }

        public static BO.Host Conv_DO_To_BO(DO.Host item)
        {
            Host temp = new Host();
            temp.HostInfo = Instance.GetPerson(item.Id);
            temp.AccountDetails = Conv_DO_To_BO(item.AccountDetails);
            temp.CollectionClearance = item.CollectionClearance;
            temp.WebSite = item.WebSite;
            temp.MyOrders = Instance.GetOrdersByHost(item.Id);
            temp.MyUnits = Instance.GetHostingUnitsByHostId(item.Id);
            return temp;
        }

        public static DO.Host Conv_BO_To_DO(BO.Host item)
        {
            DO.Host temp = new DO.Host();
            temp.AccountDetails = Conv_BO_To_DO(item.AccountDetails);
            temp.CollectionClearance = item.CollectionClearance;
            temp.WebSite = item.WebSite;
            temp.Id = item.HostInfo.Id;
            temp.Status = DO.ActivityStatus.ACTIVE;
            return temp;
        }

        public static BO.HostingUnit Conv_DO_To_BO(DO.HostingUnit item)
        {
            BO.HostingUnit temp = new BO.HostingUnit();

            temp.Key = item.Key;
            temp.HostingUnitName = item.HostingUnitName;
            temp.NumOfOffers = item.NumOfOffers;
            temp.HostId = item.HostId;
            temp.PricePerNight = item.pricePerNight;
            temp.Status = (BO.ActivityStatus)item.Status;
            temp.Diary = item.Diary;
            temp.Area = (BO.Areas)item.Area;
            temp.Garden = item.Garden;
            temp.Jacuzzi = item.Jacuzzi;
            temp.Pool = item.Pool;
            temp.Rooms = item.Rooms;
            temp.Type = (BO.HostingType)item.Type;
            return temp;
        }

        public static DO.HostingUnit Conv_BO_To_DO(BO.HostingUnit item)
        {
            DO.HostingUnit temp = new DO.HostingUnit();
            temp.Key = item.Key;
            temp.HostingUnitName = item.HostingUnitName;
            temp.NumOfOffers = item.NumOfOffers;
            temp.HostId = item.HostId;
            temp.pricePerNight = item.PricePerNight;
            temp.Status = (DO.ActivityStatus)item.Status;
            temp.Diary = item.Diary;
            temp.Area = (DO.Areas)item.Area;
            temp.Garden = item.Garden;
            temp.Jacuzzi = item.Jacuzzi;
            temp.Pool = item.Pool;
            temp.Rooms = item.Rooms;
            temp.Type = (DO.HostingType)item.Type;
            return temp;
        }

        public static BO.Manager Conv_DO_To_BO(DO.Manager item)
        {

            BO.Manager temp = new BO.Manager();
            temp.ManagerInfo = Instance.GetPerson(item.Id);
            return temp;
        }

        public static DO.Manager Conv_BO_To_DO(BO.Manager item)
        {

            DO.Manager temp = new DO.Manager();
            temp.Id = item.ManagerInfo.Id;
            temp.NumOfHosts = Instance.NumOfHosts();
            return temp;
        }

        public static BO.Order Conv_DO_To_BO(DO.Order item)
        {
            BO.Order temp = new BO.Order();
            temp.CloseDate = item.CloseDate;
            temp.GuestRequestKey = item.GuestRequestKey;
            temp.HostingUnitKey = item.HostingUnitKey;
            temp.Key = item.Key;
            temp.OrderDate = item.OrderDate;
            temp.SentDate = item.SentDate;
            temp.Status = (BO.OrderStatus)item.Status;
            temp.TotalPrice = item.TotalPrice;
            //temp.HostId = (Instance.GetHoustingUinit(item.HostingUnitKey)).HostId;
            temp.HostId = item.HostId;
            temp.ClientName = item.ClientName;
            temp.ClientId = item.CliendId;
            temp.Commission = item.Commission;
            temp.HostingUnitName = item.HostingUnitName;
            return temp;
        }

        public static DO.Order Conv_BO_To_DO(BO.Order item)
        {
            DO.Order temp = new DO.Order();
            temp.CloseDate = item.CloseDate;
            temp.GuestRequestKey = item.GuestRequestKey;
            temp.HostingUnitKey = item.HostingUnitKey;
            temp.Key = item.Key;
            temp.OrderDate = item.OrderDate;
            temp.SentDate = item.SentDate;
            temp.Status = (DO.OrderStatus)item.Status;
            temp.TotalPrice = item.TotalPrice;
            temp.CliendId = item.ClientId;
            temp.ClientName = item.ClientName;
            temp.Commission = item.Commission;
            temp.HostId = item.HostId;
            return temp;
        }

        public static BO.Person Conv_DO_To_BO(DO.Person item)
        {
            BO.Person temp = new BO.Person();
            temp.Id = item.Id;
            temp.Email = item.Email;
            temp.FirstName = item.FirstName;
            temp.LastName = item.LastName;
            temp.IdType = (BO.IdType)item.IdType;
            temp.Password = item.Password;
            temp.Phone = item.Phone;
            temp.Status = (BO.PersonStatus)item.Status;
            temp.Address = item.Address;
            return temp;
        }

        public static DO.Person Conv_BO_To_DO(BO.Person item)
        {
            DO.Person temp = new DO.Person();
            temp.Id = item.Id;
            temp.Email = item.Email;
            temp.FirstName = item.FirstName;
            temp.LastName = item.LastName;
            temp.IdType = (DO.IdType)item.IdType;
            temp.Password = item.Password;
            temp.Phone = item.Phone;
            temp.Status = (DO.PersonStatus)item.Status;
            temp.Address = item.Address;
            return temp;
        }

    }
}
