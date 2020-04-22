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
    /// Interaction logic for ShowHostingUnit.xaml
    /// </summary>
    public partial class ShowHostingUnit : Window
    {
        BlApi.IBl bl;
        HostingUnit unit;
        public ShowHostingUnit(BlApi.IBl bl, HostingUnit unit)
        {
            InitializeComponent();
            this.bl = bl;
            this.unit = unit;
            areaComboBox.ItemsSource = Enum.GetValues(typeof(Areas));
            showUnitGrid.DataContext = unit;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource hostingUnitViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("hostingUnitViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // hostingUnitViewSource.Source = [generic data source]
        }

        private void closeWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
