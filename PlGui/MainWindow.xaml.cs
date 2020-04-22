using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        Person person;
        Host host;
        List<GuestRequest> lstRequest;
        List<HostingUnit> lstUnits;
        List<Order> lstOrders;
        List<Person> clients;
        List<Host> hosts;
        List<Order> orders;
        List<HostingUnit> Units;
        BackgroundWorker email;
        Order order;
      
        public MainWindow()
        {
            InitializeComponent();
            person = new Person();
            host = new Host();
            host.HostInfo = new Person();
            host.AccountDetails = new BankBranch();
            lstRequest = new List<GuestRequest>();
            lstUnits = new List<HostingUnit>();
            lstOrders = new List<Order>();
            orders = new List<Order>();
            clients = bl.GetPersons().ToList();
            ////
            orders = bl.GetOrdersList().ToList();
            Units = bl.GetAllHostingUnits().ToList();
            hosts = bl.GetHostList().ToList();

            /*       בנייה של גריד הרשמה לאורח      */
            AddPersonGuestGrid.DataContext = person;
            /*      סוף!      */

            /*       בנייה של גריד הרשמה למארח      */
            AddPersonHostGrid.DataContext = host;
            hostListView.ItemsSource = hosts;
            personListView.ItemsSource = clients;
            hostingUnitListView.ItemsSource = Units;
            orderManagerListView.ItemsSource = orders;
            order = new Order();
            email = new BackgroundWorker();
            email.DoWork += MailSend_DoWork;
            email.RunWorkerCompleted += MailSend_RunWorkerCompleted;
        }

        private void TabControl_Main_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabControl tabControl = sender as TabControl;
            if (tabControl.SelectedIndex == 1)
            {
                RegistarGrid.Visibility = Visibility.Visible;
                AddPersonGuestGrid.Visibility = Visibility.Collapsed;
                AddPersonHostGrid.Visibility = Visibility.Collapsed;
            }
            //if (tabControl.SelectedIndex == 2)
            {
                //GuestPasswordBorder.Visibility = Visibility.Visible;
                //tb_GuestId.Clear();
                //tb_GuestPass.Clear();
                //RequestsViewList.Visibility = Visibility.Collapsed;
            }
            if (tabControl.SelectedIndex == 3)
            {
                //if (TabControl_Main.Items.IndexOf(sender) != -1)
                //{
                //HostPasswordBorder.Visibility = Visibility.Visible;
                //UnitsGrid.Visibility = Visibility.Collapsed;
                //OrdersGrid.Visibility = Visibility.Collapsed;
                //tb_HostId.Clear();
                //pb_HostPass.Clear();                
                //}
            }
            if (TabControl_Main.SelectedIndex == 4)
            {

            }
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
            RegistarGrid.Visibility = Visibility.Collapsed;
            AddPersonGuestGrid.Visibility = Visibility.Visible;
            idTextBox.Clear();
        }

        private void bt_HostRegis_Click(object sender, RoutedEventArgs e)
        {
            RegistarGrid.Visibility = Visibility.Collapsed;
            AddPersonHostGrid.Visibility = Visibility.Visible;
            idTextBox1.Clear();
            bankNumberTextBox.Clear();
            branchNumberTextBox.Clear();
            accountNumberTextBox.Clear();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Data.CollectionViewSource guestRequestViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("guestRequestViewSource")));
            //Load data by setting the CollectionViewSource.Source property:
            // guestRequestViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource personViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("personViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // personViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource hostViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("hostViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // hostViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource orderViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("orderViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // orderViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource hostingUnitViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("hostingUnitViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // hostingUnitViewSource.Source = [generic data source]
        }



        /// <summary>
        /// clear the textboxs of login as person and as host
        /// </summary>
        private void ClearTextBox()
        {
            firstNameTextBox.Clear();
            lastNameTextBox.Clear();
            idTextBox.Clear();
            addressTextBox.Clear();
            phoneTextBox.Clear();
            emailTextBox.Clear();
            passwordPasswordBox.Clear();
            ////////////////////////
            firstNameTextBox1.Clear();
            lastNameTextBox1.Clear();
            idTextBox1.Clear();
            addressTextBox1.Clear();
            emailTextBox1.Clear();
            passwordPasswordBox1.Clear();
            webSiteTextBox.Clear();
            bankNumberTextBox.Clear();
            branchNumberTextBox.Clear();
            accountNumberTextBox.Clear();
            branchAddressTextBox.Clear();
            phoneTextBox2.Clear();
            collectionClearanceCheckBox.IsChecked = false;
        }
        private void pb_GuestLog_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tb_GuestId.Text.Length == 0)
                {
                    MessageBox.Show("אנא הזן תז");
                    return;
                }
                Person person = bl.GetPerson(int.Parse(tb_GuestId.Text));
                if (person.Password != tb_GuestPass.Password)
                {
                    MessageBox.Show("הסיסמה שגויה אנא הזן שנית:");
                    return;
                }
                GuestPasswordBorder.Visibility = Visibility.Collapsed;
                lstRequest = bl.GetGuestRequests(int.Parse(tb_GuestId.Text)).ToList();
                lstRequest.RemoveAll(x => x.Status == RequestStatus.IRRELEVANT);
                RequestsViewList.ItemsSource = lstRequest;
                RequestsViewList.Visibility = Visibility.Visible;
            }
            catch (Exception)
            {
                MessageBox.Show("הפרטים שמילאת אינם רשומים במערכת:");
            }
        }

        /// <summary>
        /// הירשמות כאורח
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void registGuestButton_Click(object sender, RoutedEventArgs e)
        {
            if (idTextBox.Text == "0" || firstNameTextBox.Text.Length == 0 || lastNameTextBox.Text.Length == 0
                || emailTextBox.Text.Length == 0 || passwordPasswordBox.Password.Length == 0)
            {
                MessageBox.Show("אחד השדות או יותר חסרים :");
                return;
            }
            try
            {
                person.Password = passwordPasswordBox.Password;
                bl.AddPerson(person);
                MessageBox.Show("נרשמת בהצלחה למערכת");
                ClearTextBox();
                TabControl_Main.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                ClearTextBox();
                TabControl_Main.SelectedIndex = 0;
            }

        }

        private void registHostButton_Click(object sender, RoutedEventArgs e)
        {
            if (idTextBox1.Text == "0" || firstNameTextBox1.Text.Length == 0 || lastNameTextBox1.Text.Length == 0
                || emailTextBox1.Text.Length == 0 || passwordPasswordBox1.Password.Length == 0)
            {
                MessageBox.Show("אחד השדות או יותר חסרים :");
                return;
            }
            try
            {
                host.HostInfo.Password = passwordPasswordBox1.Password;
                try
                {
                    bl.AddPerson(host.HostInfo);
                }
                catch (Exception)
                {

                }
                bl.AddHost(host);
                MessageBox.Show("נרשמת בהצלחה למערכת");
                ClearTextBox();
                TabControl_Main.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                ClearTextBox();
                TabControl_Main.SelectedIndex = 0;
            }
        }

        private void bt_Add_Request_Click(object sender, RoutedEventArgs e)
        {
            AddGuestRequest window = new AddGuestRequest(bl, int.Parse(tb_GuestId.Text), lstRequest, this.RequestsViewList);
            window.Show();
            this.RequestsViewList.ItemsSource = lstRequest;
            this.RequestsViewList.Items.Refresh();
        }

        private void DeleteRequestButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            BO.GuestRequest guestRequest = (BO.GuestRequest)button.DataContext;
            try
            {
                bl.UpdateRequestStatus(RequestStatus.IRRELEVANT, guestRequest.Key);//update the staus as Irlevent
            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.Message);
            }
            lstRequest = bl.GetGuestRequestByPredicate(x => x.ClientId == int.Parse(tb_GuestId.Text) && x.Status != RequestStatus.IRRELEVANT);
            this.RequestsViewList.ItemsSource = lstRequest;
            this.RequestsViewList.Items.Refresh();
        }

        private void UpdateRequestButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            BO.GuestRequest guestRequest = (BO.GuestRequest)button.DataContext;
            UpdateRequest window = new UpdateRequest(bl, guestRequest);
            window.Show();
            this.RequestsViewList.ItemsSource = lstRequest;
            this.RequestsViewList.Items.Refresh();
        }

        private void ShowRequestButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            BO.GuestRequest guestRequest = (BO.GuestRequest)button.DataContext;
            ShowRequest window = new ShowRequest(bl, guestRequest);
            window.Show();
        }

        private void logout_Click(object sender, RoutedEventArgs e)
        {
            GuestPasswordBorder.Visibility = Visibility.Visible;
            RequestsViewList.Visibility = Visibility.Hidden;
            tb_GuestId.Clear();
            tb_GuestPass.Clear();
        }

        private void previousButton_Click(object sender, RoutedEventArgs e)
        {
            RegistarGrid.Visibility = Visibility.Visible;
            AddPersonGuestGrid.Visibility = Visibility.Collapsed;
        }

        private void previousHostButton_Click(object sender, RoutedEventArgs e)
        {
            RegistarGrid.Visibility = Visibility.Visible;
            AddPersonHostGrid.Visibility = Visibility.Collapsed;
        }

        private void showUnitsButton_Click(object sender, RoutedEventArgs e)
        {
            UnitsGrid.Visibility = Visibility.Visible;
            OrdersGrid.Visibility = Visibility.Collapsed;
            lstUnits = bl.GetHostingUnitsByHostId(int.Parse(tb_HostId.Text)).ToList();
            lstUnits.RemoveAll(x => x.Status == ActivityStatus.INACTIVE);
            UnitsViewList.ItemsSource = lstUnits;
            UnitsViewList.Items.Refresh();
        }

        private void showOrdersButton_Click(object sender, RoutedEventArgs e)
        {
            UnitsGrid.Visibility = Visibility.Collapsed;
            OrdersGrid.Visibility = Visibility.Visible;
            lstOrders = bl.GetOrdersByHost(int.Parse(tb_HostId.Text)).ToList();
            this.OrdersViewList.ItemsSource = lstOrders;
            this.OrdersViewList.Items.Refresh();
        }

        private void bt_HostLog_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((tb_HostId.Text.Length == 0))
                {
                    MessageBox.Show("אנא הזן תז");
                    return;
                }
                Host host = bl.GetHost(int.Parse(tb_HostId.Text));
                if (host.HostInfo.Password != pb_HostPass.Password)
                {
                    MessageBox.Show("הסיסמה שגויה אנא הזן שנית:");
                    return;
                }
                myButtons.Visibility = Visibility.Visible;
                HostPasswordBorder.Visibility = Visibility.Collapsed;

            }
            catch
            {
                MessageBox.Show("הפרטים שהזנת אינם קיימים במערכת");
            }
        }

        private void bt_Add_Units_Click(object sender, RoutedEventArgs e)
        {
            AddHostingUnit window = new AddHostingUnit(bl, int.Parse(tb_HostId.Text), lstUnits, this.UnitsViewList);
            window.Show();
            this.UnitsViewList.ItemsSource = lstUnits;
            this.UnitsViewList.Items.Refresh();
        }

        private void logoutHost_Click(object sender, RoutedEventArgs e)
        {
            HostPasswordBorder.Visibility = Visibility.Visible;
            UnitsGrid.Visibility = Visibility.Collapsed;
            OrdersGrid.Visibility = Visibility.Collapsed;
            myButtons.Visibility = Visibility.Collapsed;
            tb_HostId.Clear();
            pb_HostPass.Clear();
        }

        private void logoutOrder_Click(object sender, RoutedEventArgs e)
        {
            HostPasswordBorder.Visibility = Visibility.Visible;
            UnitsGrid.Visibility = Visibility.Collapsed;
            OrdersGrid.Visibility = Visibility.Collapsed;
            myButtons.Visibility = Visibility.Collapsed;
            tb_HostId.Clear();
            pb_HostPass.Clear();
        }

        private void DeleteUnit_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            HostingUnit unit = (HostingUnit)button.DataContext;
            bl.DisableHoustingUnit(unit.Key);
            lstUnits = bl.GetHostingUnitsByHostId(unit.HostId).ToList();
            lstUnits.RemoveAll(x => x.Status == ActivityStatus.INACTIVE);
            this.UnitsViewList.ItemsSource = lstUnits;
            this.UnitsViewList.Items.Refresh();
        }

        private void showUnit_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            HostingUnit unit = button.DataContext as HostingUnit;
            ShowHostingUnit window = new ShowHostingUnit(bl, unit);
            window.Show();
        }

        private void updateUnit_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            HostingUnit unit = button.DataContext as HostingUnit;
            UpdateHostingUnit window = new UpdateHostingUnit(bl, unit);
            window.Show();
        }

        private void bt_Add_Orders_Click(object sender, RoutedEventArgs e)
        {
            AddOrder window = new AddOrder(bl, int.Parse(tb_HostId.Text), OrdersViewList, lstOrders);
            window.Show();
        }

        private void emailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool result = ValidatorExtensions.IsValidEmailAddress(emailTextBox.Text);
        }

        private void UpDateOrder_bt_Click(object sender, RoutedEventArgs e)
        {

        }

        private void updateComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            comboBox.ItemsSource = Enum.GetValues(typeof(OrderStatus));
            //comboBox.Items.Remove(1);
        }

        private void updateComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            MessageBoxResult messageBox = MessageBox.Show("האם הינך בטוח שברצונך לשנות את סטטוס ההזמנה?", "שאלה", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes, MessageBoxOptions.RtlReading);
            if (messageBox == MessageBoxResult.No)
                return;
            order = comboBox.DataContext as Order;
            OrderStatus Status = (OrderStatus)comboBox.SelectedItem;
            try
            {
                if (Status == OrderStatus.MAIL_SENT)
                {
                    email.RunWorkerAsync();
                }
                else
                    bl.UpdateStatusOrder(Status, order.Key);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            finally
            {
                lstOrders = bl.GetOrdersByHost(int.Parse(tb_HostId.Text)).ToList();
                this.OrdersViewList.ItemsSource = lstOrders;
                this.OrdersViewList.Items.Refresh();
            }
        }
        //******************************************
        private void MailSend_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ((int)e.Result == 1)
            {
                MessageBox.Show("ההזמנה נשלחה בהצלחה");
                //order.SentDate = DateTime.Today;
                //bl.UpdateOrder(order);
                //this.OrdersViewList.ItemsSource = bl.GetOrdersByHost(int.Parse(tb_HostId.Text)).ToList();
            }
            else
            {
                MessageBox.Show("קרתה תקלה בשליחת ההזמנה");
                order.Status = OrderStatus.PROCESSING;
                bl.UpdateOrder(order);
                this.OrdersViewList.ItemsSource = bl.GetOrdersByHost(int.Parse(tb_HostId.Text)).ToList();
            }
        }

        private void MailSend_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                //order.Status = BO.OrderStatus.MAIL_SENT;
                bl.UpdateStatusOrder(BO.OrderStatus.MAIL_SENT, order.Key);
                e.Result = 1;
            }
            catch
            {
                e.Result = 0;
            }
        }
        //*****************************************
        private void watch_Order_bt_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Order order = button.DataContext as Order;
            ShowOrder window = new ShowOrder(bl, order);
            window.Show();
        }

        private void MenuItem_Checked(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            MenuItem item = menuItem.Parent as MenuItem;
            //foreach (var i in item)
            //{
            //    if ()
            //    {

            //    }
            //}
            if ((string)menuItem.Header == "ירושלים")
            {
                UnitsViewList.ItemsSource = from Group in bl.GetGroupHostingUnitsByArea()
                                            where Group.Key == Areas.ירושלים
                                            select Group into g
                                            from GR in g

                                            select GR;

            }
            if ((string)menuItem.Header == "צפון")
            {
                UnitsViewList.ItemsSource = from Group in bl.GetGroupHostingUnitsByArea()
                                            where Group.Key == Areas.צפון
                                            select Group into g
                                            from GR in g

                                            select GR;

            }
            if ((string)menuItem.Header == "דרום")
            {
                UnitsViewList.ItemsSource = from Group in bl.GetGroupHostingUnitsByArea()
                                            where Group.Key == Areas.דרום
                                            select Group into g
                                            from GR in g

                                            select GR;

            }
            if ((string)menuItem.Header == "מרכז")
            {
                UnitsViewList.ItemsSource = from Group in bl.GetGroupHostingUnitsByArea()
                                            where Group.Key == Areas.מרכז
                                            select Group into g
                                            from GR in g

                                            select GR;

            }
            if ((string)menuItem.Header == "הכל")
            {
                UnitsViewList.ItemsSource = bl.GetAllHostingUnits().ToList();
            }



        }

        private void showClientsButton_Click(object sender, RoutedEventArgs e)
        {
            clientListGrid.Visibility = Visibility.Visible;
            ordersListGrid.Visibility = Visibility.Collapsed;
            hostingUnitListGrid.Visibility = Visibility.Collapsed;
        }

        private void showHostingUnitButton_Click(object sender, RoutedEventArgs e)
        {
            clientListGrid.Visibility = Visibility.Collapsed;
            ordersListGrid.Visibility = Visibility.Collapsed;
            hostingUnitListGrid.Visibility = Visibility.Visible;
        }

        private void showOrderButton_Click(object sender, RoutedEventArgs e)
        {
            clientListGrid.Visibility = Visibility.Collapsed;
            ordersListGrid.Visibility = Visibility.Visible;
            hostingUnitListGrid.Visibility = Visibility.Collapsed;
        }

    }
}
