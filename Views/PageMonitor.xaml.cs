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

namespace EdibleFungusGreenhouse.Views
{
    /// <summary>
    /// Interaction logic for PageMonitor.xaml
    /// </summary>
    public partial class PageMonitor : Page
    {
        public PageMonitor()
        {
            InitializeComponent();
            GridAlert.Visibility = Visibility.Collapsed;
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {

        }
        /// <summary>
        /// 关闭警报框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClose_Click(object sender, RoutedEventArgs e) => GridAlert.Visibility = Visibility.Collapsed;

        private void ChkCamera_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
