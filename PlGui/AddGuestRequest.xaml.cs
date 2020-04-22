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
    /// Interaction logic for AddGuestRequest.xaml
    /// </summary>
    public partial class AddGuestRequest : Window
    {
        BlApi.IBl bl;
        GuestRequest request;
        int IdPerson;
        List<GuestRequest> lst;
        ListView lstView;
        public AddGuestRequest(BlApi.IBl bl, int id, List<GuestRequest> lst, ListView lstView)
        {
            InitializeComponent();
            this.bl = bl;
            IdPerson = id;
            this.lst = lst;
            this.lstView = lstView;
            request = new GuestRequest();
            request.ClientId = IdPerson;
            AddRequestGrid.DataContext = request;
            areaComboBox.ItemsSource = Enum.GetValues(typeof(BO.Areas));
            typeComboBox.ItemsSource = Enum.GetValues(typeof(BO.HostingType));
            poolComboBox.ItemsSource = Enum.GetValues(typeof(BO.Answer));
            jacuzziComboBox.ItemsSource = Enum.GetValues(typeof(BO.Answer));
            gardenComboBox.ItemsSource = Enum.GetValues(typeof(BO.Answer));
            childrensAttractionsComboBox.ItemsSource = Enum.GetValues(typeof(BO.Answer));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Data.CollectionViewSource guestRequestViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("guestRequestViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // guestRequestViewSource.Source = [generic data source]
        }

        private void sendRequestButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                request.CreateDate = DateTime.Now;
                int key = bl.AddRequest(request);
                lst.Add(bl.GetRequest(key));
                lstView.Items.Refresh();
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

        private void leaveDateDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            this.request.LeaveDate = (DateTime)leaveDateDatePicker.SelectedDate;
        }

        private void entryDateDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            this.request.EntryDate = (DateTime)entryDateDatePicker.SelectedDate;
            leaveDateDatePicker.SelectedDate = request.EntryDate.AddDays(1);
        }
    }
}
