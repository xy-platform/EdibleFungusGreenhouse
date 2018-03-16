using EdibleFungusGreenhouse.Common;
using System.Windows;
using System.Windows.Controls;

namespace EdibleFungusGreenhouse.Views
{
    /// <summary>
    /// Interaction logic for PageHome.xaml
    /// </summary>
    public partial class PageHome : Page
    {
        public PageHome() => InitializeComponent();

        private void Grid_Click(object sender, RoutedEventArgs e)
        {
            //获取或设置引发事件的对象的引用。
            if (e.Source.Equals(ButtonManager))
            {
                Global.gOnSkipPage("产品管理");
            }
            else if (e.Source.Equals(ButtonEnvironment))
            {
                Global.gOnSkipPage("内部环境");
            }
            else if (e.Source.Equals(ButtonMonitor))
            {
                Global.gOnSkipPage("安全监控");
            }
        }
    }
}
