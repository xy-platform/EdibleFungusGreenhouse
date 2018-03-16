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
        public MainWindow()
        {
            InitializeComponent();
            SkipPage(pageDatas[0]);
            Width = 1080;
            Height = 645;
        }
        private List<PageData> pageDatas = new List<PageData>
        {
            new PageData(){PageText="食品菌生产大棚" },
            new PageData(){ }


        };
        private void SkipPage(object p)
        {
        }

        //窗体移动
        private void Window_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton==MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void ButtonMin_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;

        private void ButtonMax_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState==WindowState.Normal)
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
        private void ButtonClose_Click(object sender, RoutedEventArgs e)=> Application.Current.Shutdown();

        private void CheckBoxReturn_Click(object sender, RoutedEventArgs e)
        {

        }

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
            if (_MouseDownContent % 2 == 0)
            {
                ButtonMax_Click(null,null);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
