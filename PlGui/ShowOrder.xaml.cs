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
    /// Interaction logic for ShowOrder.xaml
    /// </summary>
    public partial class ShowOrder : Window
    {
        BlApi.IBl bl;
        Order order;
        public ShowOrder(BlApi.IBl bl, Order order)
        {
            InitializeComponent();
            this.bl = bl;
            this.order = order;
            showOrderGrid.DataContext = order;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource orderViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("orderViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // orderViewSource.Source = [generic data source]
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
