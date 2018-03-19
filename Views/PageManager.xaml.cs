using EdibleFungusGreenhouse.Common;
using Srr1100U;
using System;
using System.Windows;
using System.Windows.Controls;
using Utils;

namespace EdibleFungusGreenhouse.Views
{
    /// <summary>
    /// Interaction logic for PageManager.xaml
    /// </summary>
    public partial class PageManager : Page
    {
        #region 构造方法
        public PageManager()
        {
            InitializeComponent();

            CheckBoxScan.IsChecked = false;
            Dispatcher.ShutdownStarted += (s, e) => { Page_Unloaded(null, null); };
            GridScanResult.Visibility = Visibility.Collapsed;
            GridScan.Visibility = Visibility.Visible;
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 超高频桌面发卡器
        /// </summary>
        private SrrReader reader = new SrrReader(Config.srrReaderPort);

        private void Page_Unloaded(object sender, RoutedEventArgs e) => reader.CloseDevice();

        /// <summary>
        /// 开始扫描
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonBeginScan(object sender, RoutedEventArgs e)
        {
            if (reader.ConnDevice() != 0)
            {
                MessageBox.Show("设备连接失败！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            CheckBoxScan.IsChecked = true;
            //数据回调
            reader.Read((rfid) =>
            {
                Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background,
                    (Action)(() =>
                    {
                        Produce produce = ProduceHelper.Instance.GetProduceByCode(rfid);
                        if (produce != null)
                        {
                            LabCardcode.Content = produce.Cardcode;
                            LabName.Content = produce.Name;
                            LabCollectTime.Content = produce.CollectTime;
                            LabSaveTemperature.Content = produce.SaveTemperature;
                            LabShelfLife.Content = produce.ShelfLife;

                            GridScanResult.Visibility = Visibility.Visible;
                            GridScan.Visibility = Visibility.Collapsed;
                        }
                    }));
            });
        }

        /// <summary>
        /// 关闭扫描结果界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonCloseScan(object sender, RoutedEventArgs e)
        {
            GridScanResult.Visibility = Visibility.Collapsed;
            GridScan.Visibility = Visibility.Visible;
        } 
        #endregion
    }
}
