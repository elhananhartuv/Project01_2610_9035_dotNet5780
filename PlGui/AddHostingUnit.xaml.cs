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
    /// Interaction logic for AddHostingUnit.xaml
    /// </summary>
    public partial class AddHostingUnit : Window
    {
        BlApi.IBl bl;
        HostingUnit unit;
        List<HostingUnit> units;
        ListView view;
        public AddHostingUnit(BlApi.IBl bl, int id, List<HostingUnit> units, ListView view)
        {
            InitializeComponent();
            this.bl = bl;
            unit = new HostingUnit();
            unit.Diary = new bool[12, 31];
            this.units = units;
            this.view = view;
            addHostingUnitGrid.DataContext = unit;
            unit.HostId = id;
            areaComboBox.ItemsSource = Enum.GetValues(typeof(BO.Areas));

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource hostingUnitViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("hostingUnitViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // hostingUnitViewSource.Source = [generic data source]
        }

        private void AddHostingUnit_bt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int key = bl.AddHostingUinit(unit);
                units.Add(bl.GetHoustingUinit(key));
                view.Items.Refresh();
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
