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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BlApi;

using BO;

namespace PlGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BlApi.IBl bl = BlApi.BlFactory.GetBLObj();
        List<GuestRequest> lstRequest;
     
        public MainWindow()
        {

            GuestRequest request = new GuestRequest();
            InitializeComponent();
            AddGuestRequest.DataContext = request;
            /* בנייה של גריד הוספת בקשת אירוח */
            areaComboBox.ItemsSource = Enum.GetValues(typeof(BO.Areas));
            typeComboBox.ItemsSource = Enum.GetValues(typeof(BO.HostingType));
            poolComboBox.ItemsSource = Enum.GetValues(typeof(BO.Answer));
            jacuzziComboBox.ItemsSource = Enum.GetValues(typeof(BO.Answer));
            gardenComboBox.ItemsSource = Enum.GetValues(typeof(BO.Answer));
            childrensAttractionsComboBox.ItemsSource = Enum.GetValues(typeof(BO.Answer));
          

            lstRequest = new List<GuestRequest>();
            lstRequest = bl.GetGuestRequests(212282610).ToList();//list that contiad all request of this person
            RequestsViewList.ItemsSource = lstRequest;
        }

        private void TabControl_Main_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Bt_Login_Click(object sender, RoutedEventArgs e)
        {
            TabControl_Main.SelectedIndex = 1;
        }

        private void Bt_Guest_Click(object sender, RoutedEventArgs e)
        {
            TabControl_Main.SelectedIndex = 2;
        }

        private void Bt_Host_Click(object sender, RoutedEventArgs e)
        {
            TabControl_Main.SelectedIndex = 3;
        }

        private void Bt_Manager_Click(object sender, RoutedEventArgs e)
        {
            TabControl_Main.SelectedIndex = 4;
        }

        private void bt_GuestRegis_Click(object sender, RoutedEventArgs e)
        {
            GuestLogBorder.Visibility = Visibility.Visible;
            HostLogBorder.Visibility = Visibility.Collapsed;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GuestLogBorder.Visibility = Visibility.Collapsed;
            HostLogBorder.Visibility = Visibility.Collapsed;
            System.Windows.Data.CollectionViewSource guestRequestViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("guestRequestViewSource")));
            //Load data by setting the CollectionViewSource.Source property:
            // guestRequestViewSource.Source = [generic data source]
        }

        private void bt_HostRegis_Click(object sender, RoutedEventArgs e)
        {
           
            HostLogBorder.Visibility = Visibility.Visible;
            GuestLogBorder.Visibility = Visibility.Collapsed;
        }
        /// <summary>
        /// add client to system
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_GuestRegist_Click(object sender, RoutedEventArgs e)
        {
            BO.Person person = new BO.Person
            {
                FirstName = tbFirstName.Text,
                LastName = tbLastName.Text
            };
            if (tbId.Text.Length == 0)
            {
                MessageBox.Show("אחד השדות או יותר חסרים:");
                return;
            }
            if (tbId.Text.Length > 0)
                person.Id = int.Parse(tbId.Text);
            person.Password = tbPassword1.Password;
            person.Phone = tbPhone.Text;
            person.Status = BO.PersonStatus.ACTIVE;
            try
            {
                bl.GetPerson(person.Id);
                MessageBox.Show("המשתמש קיים במערכת ");
                ClearTextBox();
                GuestLogBorder.Visibility = Visibility.Hidden;
            }
            catch (Exception)
            {
                bl.AddPerson(person);
                MessageBox.Show("נרשמת בהצלחה למערכת");
                ClearTextBox();
                GuestLogBorder.Visibility = Visibility.Hidden;
            }
        }
        /// <summary>
        /// add host to system
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_HostRegist_Click(object sender, RoutedEventArgs e)
        {
            BO.Host host = new BO.Host();
            if (tbIdHost.Text.Length == 0 || tbAccountNum.Text.Length == 0 || tbBranchNum.Text.Length == 0 || tbBankNum.Text.Length == 0)
            {
                MessageBox.Show("אחד השדות או יותר חסרים :");
                return;
            }
            host.AccountDetails = new BankBranch()
            {
                AccuontNumber = int.Parse(tbAccountNum.Text),
                BranchNumber = int.Parse(tbBranchNum.Text),
                BankNumber = int.Parse(tbBankNum.Text),
            };


            host.CollectionClearance = cb_permission.IsChecked.Value;
            host.WebSite = tbWebSite.Text;
            host.MyOrders = new List<BO.Order>();
            host.MyUnits = new List<BO.HostingUnit>();
            host.HostInfo = new Person()
            {
                Email = tbMailHost.Text,
                Address = "",
                FirstName = tbFirstNameHost.Text,
                Id = int.Parse(tbIdHost.Text),
                LastName = tbLastNameHost.Text,
                Phone = tbPhoneHost.Text,
                Password = tbPassword1Host.Password,
                Status = BO.PersonStatus.ACTIVE,
            };


            try
            {
                bl.GetHost(host.HostInfo.Id);
                MessageBox.Show("המשתמש קיים במערכת ");
                ClearTextBox();
                HostLogBorder.Visibility = Visibility.Hidden;


            }
            catch 
            {
                try
                {
                    Person per = bl.GetPerson(host.HostInfo.Id);
                    bl.UpdatePerson(host.HostInfo);
                    bl.AddHost(host);
                }
                catch
                {
                    bl.AddPerson(host.HostInfo);
                    bl.AddHost(host);
                }
                       
                MessageBox.Show("נרשמת בהצלחה למערכת");
                ClearTextBox();
                HostLogBorder.Visibility = Visibility.Hidden;
               
            }
        }
        /// <summary>
        /// clear the textboxs of login as person and as host
        /// </summary>
        private void ClearTextBox()
        {
            tbAccountNum.Clear();
            tbBankNum.Clear();
            tbBranchNum.Clear();
            tbFirstNameHost.Clear();
            tbIdHost.Clear();
            tbPhoneHost.Clear();
            tbPassword1Host.Clear();
            tbPassword2Host.Clear();
            tbPhoneHost.Clear();
            tb_HostId.Clear();
            tbLastNameHost.Clear();
            tbId.Clear();
            tbLastName.Clear();
            tbMail.Clear();
            tbPassword1.Clear();
            tbPassword2.Clear();
            tbPhone.Clear();
            tbWebSite.Clear();
            tbFirstName.Clear();
            tbMailHost.Clear();
            cb_permission.IsChecked = false;
        }
        private void pb_GuestLog_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Person person = bl.GetPerson(int.Parse(tb_GuestId.Text));
                if (person.Password != tb_GuestPass.Password)
                {
                    MessageBox.Show("הסיסמה שגויה אנא הזן שנית:");
                    return;
                }
                GuestBorder.Visibility = Visibility.Visible;
                GuestPasswordBorder.Visibility = Visibility.Collapsed;
                //lst = bl.GetGuestRequests(int.Parse(tb_GuestId.Text)).ToList();//list that contiad all request of this person
               
            }
            catch (Exception)
            {
                MessageBox.Show("הפרטים שמילאת אינם רשומים במערכת:");
            }
        }

        private void bt_AddRequest_Click(object sender, RoutedEventArgs e)
        {
            AddGuestRequest.Visibility = Visibility.Visible;
        }

        private void bt_Add_Request_Click(object sender, RoutedEventArgs e)
        {
         


        }
    }
}