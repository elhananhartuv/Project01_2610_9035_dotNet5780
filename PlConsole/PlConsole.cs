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
         

            
            try
            {
                bl.UpdateStatusOrder(OrderStatus.MAIL_SENT, 5);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

         

        }
    }
}
