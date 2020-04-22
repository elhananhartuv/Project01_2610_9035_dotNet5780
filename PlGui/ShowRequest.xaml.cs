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
    /// Interaction logic for ShowRequest.xaml
    /// </summary>
    public partial class ShowRequest : Window
    {
        BlApi.IBl bl;
        GuestRequest request;
        public ShowRequest(BlApi.IBl bl, GuestRequest request)
        {
            InitializeComponent();
            this.bl = bl;
            this.request = request;
            this.DataContext = request;
            areaComboBox.ItemsSource = Enum.GetValues(typeof(Areas));
            typeComboBox.ItemsSource = Enum.GetValues(typeof(HostingType));
            var p = Enum.GetValues(typeof(Answer));
            gardenComboBox.ItemsSource = p;
            childrensAttractionsComboBox.ItemsSource = p;
            poolComboBox.ItemsSource = p;
            jacuzziComboBox.ItemsSource = p;
        }

        private void closeWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
