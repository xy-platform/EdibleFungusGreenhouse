using EdibleFungusGreenhouse.Common;
using System;
using System.Windows;
using System.Windows.Controls;

using IPCameraDll;

namespace EdibleFungusGreenhouse.Views
{
    /// <summary>
    /// Interaction logic for PageMonitor.xaml
    /// </summary>
    public partial class PageMonitor : Page
    {
        #region 构造方法
        public PageMonitor()
        {
            InitializeComponent();
            GridAlert.Visibility = Visibility.Collapsed;
            DeviceHelper.Instance.OnReadCompleted += Instance_OnReadCompleted;
        }

        #endregion

        #region 私有变量
        private IPCamera camera;
        /// <summary>
        /// 摄像头
        /// </summary>
        public IPCamera Camera
        {
            get
            {
                if (camera == null)
                {
                    camera = new IpCameraHelper(
                        Config.cameraIp,
                        Config.cameraUser,
                        Config.cameraPassword,
                        new Action<ImageEventArgs>((arg) =>
                        {
                            if ((bool)ChkCamera.IsChecked)
                            {
                                ImageCamera.Source = arg.FrameReadyEventArgs.BitmapImage;
                            }
                        }));
                }
                return camera;
            }
        }
        #endregion

        #region 私有方法

        /// <summary>
        /// 设备数据回调
        /// </summary>
        /// <param name="infos">设备数据</param>
        private void Instance_OnReadCompleted(DeviceInfo infos)
        {
            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, (Action)(() =>
             {
                 LabTempValue.Content = infos.TemValue;
                 LabHumiValue.Content = infos.HumiValue;
                 LabFireState.Content = infos.FlameSensorStateText;
                 LabSmokeState.Content = infos.SmokeSensorStateText;

                 if (infos.IsAlarmNow)
                 {
                     GridAlarm.Visibility = Visibility.Visible;
                     //有火无烟
                     if (infos.FlameSensorStateText.Equals("有") && infos.SmokeSensorStateText.Equals("正常"))
                     {
                         LabPrompt.Content = "大棚内有火焰，请立即采取灭火措施！";
                     }
                     //有烟无火
                     if (infos.FlameSensorStateText.Equals("无") && infos.SmokeSensorStateText.Equals("超标"))
                     {
                         LabPrompt.Content = "大棚内有烟雾，请立即采取措施，检查阀门系统！";
                     }
                     //有火有烟
                     if (infos.FlameSensorStateText.Equals("有") && infos.SmokeSensorStateText.Equals("超标"))
                     {
                         LabPrompt.Content = "大棚内有火焰烟雾，请立即采取灭火措施，并检查阀门系统！";
                     }
                 }
                 else
                 {
                     GridAlarm.Visibility = Visibility.Collapsed;
                 }
             }));
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            DeviceHelper.Instance.OnReadCompleted -= Instance_OnReadCompleted;
            Camera.StopProcessing();

        }
        /// <summary>
        /// 关闭警报框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClose_Click(object sender, RoutedEventArgs e) => GridAlert.Visibility = Visibility.Collapsed;

        /// <summary>
        /// 打开或关闭摄像头
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChkCamera_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)ChkCamera.IsChecked)
            {
                Camera.StartProcessing();
            }
            else
            {
                Camera.StopProcessing();
                ImageCamera.Source = null;
            }
        }
        #endregion

        #region 摄像头方向控制

        /// <summary>
        /// 开始向下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButonDown_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (camera != null)
            {
                camera.PanDown();
            }

        }

        /// <summary>
        /// 停止向下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButonDown_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (camera != null)
            {
                camera.PanDown();
            }
        }

        ///// <summary>
        ///// 开始向上
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        private void ButonUp_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (camera != null)
            {
                camera.PanUp();
            }
        }

        ///// <summary>
        ///// 停止向上
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        private void ButonUp_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (camera != null)
            {
                camera.PanUp();
            }
        }

        /// <summary>
        /// 开始向左
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButonLeft_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (camera!=null)
            {
                camera.PanLeft();
            }
        }

        /// <summary>
        /// 停止向左
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButonLeft_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (camera != null)
            {
                camera.PanLeft();
            }
        }

        /// <summary>
        /// 停止向右
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButonRight_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (camera!=null)
            {
                camera.PanRight();
            }
        }

        /// <summary>
        /// 开始向右
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButonRight_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (camera != null)
            {
                camera.PanRight();
            }
        }

        #endregion
    }
}
