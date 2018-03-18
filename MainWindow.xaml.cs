using EdibleFungusGreenhouse.Common;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Utils;

namespace EdibleFungusGreenhouse
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region  构造方法

        public MainWindow()
        {
            InitializeComponent();

            SkipPage(pageDatas[0]);

            Dispatcher.ShutdownStarted += (o, e) => { Window_Unloaded(null, null); };
            Global.gOnSkipPage += new Global.DgOnSkipPage(OnSkipPage);

            Width = 1080;
            Height = 645;
        }

        #endregion

        #region 私有成员
        /// <summary>
        /// 页面数据集合
        /// </summary>
        private List<PageData> pageDatas = new List<PageData>
        {
            new PageData(){PageText="食品菌生产大棚",PagePath="/Views/PageHome.xaml",IsReturn=false },
            new PageData(){PageText="产品管理",PagePath="/Views/PageManager.xaml",IsReturn=true },
            new PageData(){PageText="内部环境",PagePath="/Views/PageEnvironment.xaml",IsReturn=true },
            new PageData(){PageText="安全监控",PagePath="/Views/PageMonitor.xaml",IsReturn=true }
        };
        #endregion

        #region 事件处理

        //private void Dispatcher_ShutdownStarted(object sender, EventArgs e)
        //{
        //    Window_Unloaded(null, null);
        //}

        /// <summary>
        /// 页面跳转
        /// </summary>
        /// <param name="paged">页面数据</param>
        private void SkipPage(PageData paged)
        {
            LabelTitle.Content = paged.PageText;
            CheckBoxReturn.IsChecked = paged.IsReturn;
            CheckBoxReturn.IsEnabled = paged.IsReturn;
            FrameMain.Source = ResourcesHelper.Instance.GetResourceUri(paged.PagePath);
        }

        /// <summary>
        /// 窗体移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
            try
            {
                DragMove();
            }
            catch { }
        }

        /// <summary>
        /// 最小化窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonMin_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;

        /// <summary>
        /// 最大化窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonMax_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                //去除阴影效果
                BorderThickness = new Thickness(0);
                WindowState = WindowState.Maximized;
            }
            else
            {
                //附加阴影效果
                BorderThickness = new Thickness(8);
                WindowState = WindowState.Normal;
            }
        }

        /// <summary>
        /// 关闭程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClose_Click(object sender, RoutedEventArgs e) => Application.Current.Shutdown();

        /// <summary>
        /// 返回键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBoxReturn_Click(object sender, RoutedEventArgs e) => SkipPage(pageDatas[0]);

        /// <summary>
        /// 鼠标点击次数
        /// </summary>
        private int _MouseDownContent = 0;

        /// <summary>
        /// 标题栏双击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridTitle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _MouseDownContent += 1;
            System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(300),
                IsEnabled = true
            };
            timer.Tick += (s, ev) => 
            {
                timer.IsEnabled = false;_MouseDownContent = 0;
            };
            if (_MouseDownContent % 2 == 0)
            {
                ButtonMax_Click(null, null);
            }
        }

        /// <summary>
        /// 开启数据采集
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e) => DeviceHelper.Instance.Start();

        /// <summary>
        /// 当元素从加载元素的元素树中移除时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            Global.gOnSkipPage -= OnSkipPage;
            // 关闭数据采集
            DeviceHelper.Instance.Dispose();
        }

        /// <summary>
        /// 页面跳转
        /// </summary>
        /// <param name="pageName">页面名称</param>
        private void OnSkipPage(string pageName)
        {
            PageData page = pageDatas.Find(item => item.PageText.Equals(pageName));
            SkipPage(page);
        }
        #endregion
    }
}
