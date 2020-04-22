using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BO;

namespace PlGui
{
    public static class ValidatorExtensions
    {
        public static bool IsValidEmailAddress(this string s)
        {
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(s);
        }
    }

    /// <summary>
    /// Interaction logic for AddOrder.xaml
    /// </summary>
    public partial class AddOrder : Window
    {
        BlApi.IBl bl;
        List<GuestRequest> requests;
        int hostId;
        List<Order> lstOrders;
        ListView orderListView;
        public AddOrder(BlApi.IBl bl, int id, ListView ordersListView, List<Order> lstOrders)
        {
            InitializeComponent();
            this.bl = bl;
            hostId = id;
            requests = bl.GetGuestRequests().ToList();
            guestRequestListView.ItemsSource = requests;
            //lstOrders = lst;
            this.orderListView = ordersListView;
            this.lstOrders = lstOrders;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource guestRequestViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("guestRequestViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // guestRequestViewSource.Source = [generic data source]
        }

        private void MenuItem_Checked(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            MenuItem item = menuItem.Parent as MenuItem;

            if ((string)menuItem.Header == "ירושלים")
            {
                guestRequestListView.ItemsSource = from Group in bl.GetGroupGuestRequestsByArea()
                                                   where Group.Key == Areas.ירושלים
                                                   select Group into g
                                                   from GR in g
                                                   where GR.ClientId != hostId
                                                   select GR;

            }
            if ((string)menuItem.Header == "צפון")
            {
                guestRequestListView.ItemsSource = from Group in bl.GetGroupGuestRequestsByArea()
                                                   where Group.Key == Areas.צפון
                                                   select Group into g
                                                   from GR in g
                                                   where GR.ClientId != hostId
                                                   select GR;

            }
            if ((string)menuItem.Header == "דרום")
            {
                guestRequestListView.ItemsSource = from Group in bl.GetGroupGuestRequestsByArea()
                                                   where Group.Key == Areas.דרום
                                                   select Group into g
                                                   from GR in g
                                                   where GR.ClientId != hostId
                                                   select GR;

            }
            if ((string)menuItem.Header == "מרכז")
            {
                guestRequestListView.ItemsSource = from Group in bl.GetGroupGuestRequestsByArea()
                                                   where Group.Key == Areas.מרכז
                                                   select Group into g
                                                   from GR in g
                                                   where GR.ClientId != hostId
                                                   select GR;

            }
            if ((string)menuItem.Header == "הכל")
            {
                guestRequestListView.ItemsSource = bl.GetGuestRequests().Where((x => x.Status != RequestStatus.IRRELEVANT)).ToList();
            }
        }

        private void findUnit_Bt_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            GuestRequest request = button.DataContext as GuestRequest;

            //guestRequestListView
            AvilableUnits window = new AvilableUnits(bl, hostId, request.Key);
            window.Show();
            requests.RemoveAll(x => x.Key == request.Key);
            guestRequestListView.ItemsSource = requests;
            guestRequestListView.Items.Refresh();


        }

        private void MenuItem_Checked_1(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            if ((string)menuItem.Header == "2")
            {
                guestRequestListView.ItemsSource = from Group in bl.GetGroupGuestRequestsByNumOfVacationers()
                                                   where Group.Key == 2
                                                   select Group into g
                                                   from GR in g
                                                   select GR;

            }
            if ((string)menuItem.Header == "2-4")
            {
                guestRequestListView.ItemsSource = from Group in bl.GetGroupGuestRequestsByNumOfVacationers()
                                                   where Group.Key <= 4 && Group.Key > 2
                                                   select Group into g
                                                   from GR in g
                                                   select GR;

            }
            if ((string)menuItem.Header == "4-6")
            {
                guestRequestListView.ItemsSource = from Group in bl.GetGroupGuestRequestsByNumOfVacationers()
                                                   where Group.Key <= 6 && Group.Key > 4
                                                   select Group into g
                                                   from GR in g
                                                   select GR;

            }
            if ((string)menuItem.Header == "6-8")
            {
                guestRequestListView.ItemsSource = from Group in bl.GetGroupGuestRequestsByNumOfVacationers()
                                                   where Group.Key <= 8 && Group.Key > 6
                                                   select Group into g
                                                   from GR in g
                                                   select GR;

            }
            if ((string)menuItem.Header == "8 ומעלה")
            {
                guestRequestListView.ItemsSource = from Group in bl.GetGroupGuestRequestsByNumOfVacationers()
                                                   where Group.Key > 8
                                                   select Group into g
                                                   from GR in g
                                                   select GR;

            }

        }
    }
}
