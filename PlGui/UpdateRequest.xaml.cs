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
    /// Interaction logic for UpdateRequest.xaml
    /// </summary>
    public partial class UpdateRequest : Window
    {
        GuestRequest updateor;
        BlApi.IBl bl;
        public UpdateRequest(BlApi.IBl bl, GuestRequest guest)
        {
            InitializeComponent();
            this.bl = bl;
            updateor = guest;
            this.DataContext = updateor;
            areaComboBox.ItemsSource = Enum.GetValues(typeof(Areas));
            typeComboBox.ItemsSource = Enum.GetValues(typeof(HostingType));
            var p = Enum.GetValues(typeof(Answer));
            gardenComboBox.ItemsSource = p;
            childrensAttractionsComboBox.ItemsSource = p;
            poolComboBox.ItemsSource = p;
            jacuzziComboBox.ItemsSource = p;
        }

        private void sendRequestButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.UpdateRequest(updateor);
                MessageBox.Show("פרטי הבקשה עודכנו בהצלחה");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();
        }

    }
}
