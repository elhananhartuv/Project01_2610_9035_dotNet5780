using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;
using BO;
using BlApi;


namespace PlConsole
{
    class PlConsole
    {
        static void Main(string[] args)
        {
            BlApi.IBl bl = BlApi.BlFactory.GetBLObj();
            Order tmp = bl.GetOrder(5);
            List<HostingUnit> lst = bl.GetAllHostingUnits().ToList();
            Console.WriteLine(tmp);
            foreach (var i in lst)
            {
                Console.WriteLine(i);
            }

            Console.WriteLine((bl.GetPerson(212282610)));
            try
            {
                bl.UpdateStatusOrder(OrderStatus.APPROVED, 5);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            IEnumerable<IGrouping<Areas, BO.GuestRequest>> g = bl.GetGroupGuestRequestsByArea();
            foreach (var item in g)
            {
                foreach (var y in item)
                {
                    Console.WriteLine(y);
                }
            }

        }
    }
}
