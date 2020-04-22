using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// <summary>
    /// Interaction logic for AvilableUnits.xaml
    /// </summary>
    public partial class AvilableUnits : Window
    {
        BlApi.IBl bl;
        int hostId;
        List<BO.HostingUnit> lstUnits;
        GuestRequest request;
        int numOfDays;

        public AvilableUnits(BlApi.IBl bl, int _id, int guestRequestKey)
        {
            InitializeComponent();

            this.bl = bl;
            request = bl.GetRequest(guestRequestKey);
            hostId = _id;
            numOfDays = (request.LeaveDate - request.EntryDate).Days;
            hostId = _id;
            lstUnits = bl.AvailableUnits(request.EntryDate, numOfDays).ToList();
            lstUnits.RemoveAll(x => x.HostId != hostId);
            hostingUnitListView.ItemsSource = lstUnits;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource hostingUnitViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("hostingUnitViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // hostingUnitViewSource.Source = [generic data source]
        }

        private void addOrderButton_Click(object sender, RoutedEventArgs e)//add order
        {
            int key;
            Button button = sender as Button;
            HostingUnit unit = button.DataContext as HostingUnit;
            Order order = new Order()
            {
                ClientId = request.ClientId,
                HostingUnitKey = unit.Key,
                GuestRequestKey = request.Key,
                HostingUnitName = unit.HostingUnitName,
                //Commission = 10,
                ClientName = unit.HostingUnitName,
                OrderDate = DateTime.Now,
                Status = BO.OrderStatus.PROCESSING,
                TotalPrice = (request.LeaveDate - request.EntryDate).Days * unit.PricePerNight,
                HostId = unit.HostId,
                //CloseDate = DateTime.Now.AddDays(7.0),
                //Key = 0,
                //SentDate = DateTime.Now.AddDays(7.0)               
            };

            // this.hostingUnitListView.ItemsSource
            try
            {
                key = bl.AddOrder(order);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Close();
            }
        }
    }
}