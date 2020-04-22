using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Dal
{
    public enum HostingType
    {
        צימר,
        מלון,
        קמפינג,
        וילה
    }
    public enum ActivityStatus//extra
    {
        ACTIVE,
        INACTIVE
    }
    public enum Areas
    {
        הכל,
        צפון,
        דרום,
        מרכז,
        ירושלים
    }
    public class HostingUnitXml
    {
        public ActivityStatus Status { get; set; }//extra

        public HostingType Type { get; set; }
        public int Rooms { get; set; }
        public bool Pool { get; set; }
        public bool Jacuzzi { get; set; }
        public bool Garden { get; set; }
        public bool ChildrensAttractions { get; set; }
        public int Key { get; set; }//key
        public int HostId { get; set; }//id of owner
        public string HostingUnitName { get; set; }
        [XmlIgnore]
        public bool[,] Diary { get; set; }
        [XmlArray("Calander")]
        public bool[] DiaryToXml
        {
            get { return Diary.Flatten(); }
            set { Diary = value.Expand(12); }
        }
        public int NumOfOffers { get; set; }//extra - סופר את מספר ההצעות שנשלחו עבור יחידת אירוח זו
        public int pricePerNight { get; set; }
        public Areas Area { get; set; }
        public override string ToString()
        {
            return base.ToString();
        }
    }
    public static class Tools
    {
        public static T[] Flatten<T>(this T[,] arr)
        {
            int rows = arr.GetLength(0);
            int columns = arr.GetLength(1);
            T[] arrFlattened = new T[rows * columns];
            for (int j = 0; j < rows; j++)
                for (int i = 0; i < columns; i++)
                {                 
                    arrFlattened[i + j * rows] = arr[j, i];
                }
            return arrFlattened;
        }
        public static T[,] Expand<T>(this T[] arr, int rows)
        {
            int length = arr.GetLength(0);
            int columns = length / rows;
            T[,] arrExpanded = new T[rows, columns];
            for (int j = 0; j < rows; j++)
                for (int i = 0; i < columns; i++)
                    arrExpanded[j, i] = arr[i + j * rows];
            return arrExpanded;
        }
        public static DO.HostingUnit Conv_Xml_To_DO(HostingUnitXml item)
        {
            DO.HostingUnit temp = new DO.HostingUnit();
            temp.Area = (DO.Areas)item.Area;
            temp.ChildrensAttractions = item.ChildrensAttractions;
            temp.Diary = item.Diary;
            temp.Garden = item.Garden;
            temp.HostId = item.HostId;
            temp.HostingUnitName = item.HostingUnitName;
            temp.Jacuzzi = item.Jacuzzi;
            temp.Key = item.Key;
            temp.NumOfOffers = item.NumOfOffers;
            temp.Pool = item.Pool;
            temp.pricePerNight = item.pricePerNight;
            temp.Rooms = item.Rooms;
            temp.Status = (DO.ActivityStatus)item.Status;
            temp.Type = (DO.HostingType)item.Type;
            return temp;
        }

        public static HostingUnitXml conv_DO_To_Xml(DO.HostingUnit item)
        {
            HostingUnitXml temp = new HostingUnitXml();
            temp.Area = (Dal.Areas)item.Area;
            temp.ChildrensAttractions = item.ChildrensAttractions;
            temp.Diary = item.Diary;
            temp.Garden = item.Garden;
            temp.HostId = item.HostId;
            temp.HostingUnitName = item.HostingUnitName;
            temp.Jacuzzi = item.Jacuzzi;
            temp.Key = item.Key;
            temp.NumOfOffers = item.NumOfOffers;
            temp.Pool = item.Pool;
            temp.pricePerNight = item.pricePerNight;
            temp.Rooms = item.Rooms;
            temp.Status = (Dal.ActivityStatus)item.Status;
            temp.Type = (Dal.HostingType)item.Type;
            return temp;
        }

    }
}
