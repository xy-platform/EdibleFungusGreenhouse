using EdibleFungusGreenhouse.Common;
using System;
using System.Windows;
using System.Windows.Controls;
using Utils;

namespace EdibleFungusGreenhouse.Views
{
    /// <summary>
    /// Interaction logic for PageEnvironment.xaml
    /// </summary>
    public partial class PageEnvironment : Page
    {
        #region 构造方法
        public PageEnvironment()
        {
            InitializeComponent();

            DeviceHelper.Instance.OnReadCompleted += Instance_OnReadCompleted;
            Dispatcher.ShutdownStarted += (s, e) => { Page_Unloaded(null, null); };
            BorderSet.Visibility = Visibility.Collapsed;

            FanAnimation.OnImageChanged += () => { ImageFan.Source = FanAnimation.Img; };
            HeaterAnimation.OnImageChanged += () => { ImageHeater.Source = FanAnimation.Img; };
            ImageFan.Source = FanAnimation.Img;
            ImageHeater.Source = HeaterAnimation.Img;
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
                 LabTemValue.Content = infos.TemValue;
                 LabHumiValue.Content = infos.HumiValue;
                 LabLightValue.Content = infos.LightValue;
                 CheckBoxFanStatus.IsChecked = infos.IsFanOpen;
                 CheckBoxHeaterStatus.IsChecked = infos.IsHeaterOpen;

                 if (infos.IsFanOpen)
                 {
                     FanAnimation.Start();
                 }
                 else
                 {
                     FanAnimation.Stop();
                 }

                 if (infos.IsHeaterOpen)
                 {
                     HeaterAnimation.Start();
                 }
                 else
                 {
                     HeaterAnimation.Stop();
                 }
             }));
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            DeviceHelper.Instance.OnReadCompleted -= Instance_OnReadCompleted;
            FanAnimation.Stop();
            HeaterAnimation.Stop();
        }

        private void BorderSet_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (BorderSet.Visibility == Visibility.Visible)
            {
                TextBoxLowTemp.Text = DeviceHelper.Instance.SetParams.LowTemp.ToString();
                TextBoxHighTemp.Text = DeviceHelper.Instance.SetParams.HighTemp.ToString();

                TextBoxLowHumi.Text = DeviceHelper.Instance.SetParams.LowHumi.ToString();
                TextBoxHighHumi.Text = DeviceHelper.Instance.SetParams.HighHumi.ToString();

                TextBoxLowLight.Text = DeviceHelper.Instance.SetParams.LowLight.ToString();
                TextBoxHighLight.Text = DeviceHelper.Instance.SetParams.HighLight.ToString();
            }
        }
        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSet_Click(object sender, RoutedEventArgs e)
        {
            if (e.Source.Equals(ButtonSet))
            {
                BorderSet.Visibility = BorderSet.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            }
            else if (e.Source.Equals(ButtonCancel))
            {
                BorderSet.Visibility = Visibility.Collapsed;
            }
            else if (e.Source.Equals(ButtonSave))
            {
                if (!SaveSetParams())
                {
                    MessageBox.Show("保存失败，请检查！");
                }
                else
                {
                    BorderSet.Visibility = Visibility.Collapsed;
                }
            }
        }

        /// <summary>
        /// 保存设置
        /// </summary>
        /// <returns></returns>
        private bool SaveSetParams()
        {
            int[] temps = new int[6];
            //判断输入是否正确
            if (!int.TryParse(TextBoxLowTemp.Text.ToString(), out temps[0]) ||
                !int.TryParse(TextBoxHighTemp.Text.ToString(), out temps[1]) ||
                !int.TryParse(TextBoxLowHumi.Text.ToString(), out temps[2]) ||
                !int.TryParse(TextBoxHighHumi.Text.ToString(), out temps[3]) ||
                !int.TryParse(TextBoxLowLight.Text.ToString(), out temps[4]) ||
                !int.TryParse(TextBoxHighLight.Text.ToString(), out temps[5]))
            {
                return false;
            }

            SetParams pars = new SetParams();
            pars.LowTemp = temps[0];
            pars.HighTemp = temps[1];
            pars.LowHumi = temps[2];
            pars.HighHumi = temps[3];
            pars.LowLight = temps[4];
            pars.HighLight = temps[5];

            // 判断区间是否正确
            if ((pars.LowLight > pars.HighLight) ||
                (pars.LowTemp > pars.HighTemp) ||
                (pars.LowHumi > pars.HighHumi))
            {
                return false;
            }

            DeviceHelper.Instance.SetParams = pars;
            return true;
        } 
        #endregion

        #region 私有变量

        private TmrAnimation _fanAnimation;

        /// <summary>
        /// 风扇动画
        /// </summary>
        private TmrAnimation FanAnimation
        {
            get
            {
                if (_fanAnimation == null)
                {
                    _fanAnimation = new TmrAnimation
                        (0.1,
                        ResourcesHelper.Instance.GetImageResource("Images/fan_1.png"),
                        ResourcesHelper.Instance.GetImageResource("Images/fan_2.png"),
                        ResourcesHelper.Instance.GetImageResource("Images/fan_3.png"),
                        ResourcesHelper.Instance.GetImageResource("Images/fan_4.png"),
                        ResourcesHelper.Instance.GetImageResource("Images/fan_5.png"),
                        ResourcesHelper.Instance.GetImageResource("Images/fan_6.png")
                        );
                }
                return _fanAnimation;
            }
        }

        private TmrAnimation _HeaterAnimation;

        /// <summary>
        /// 加热器动画
        /// </summary>
        private TmrAnimation HeaterAnimation
        {
            get
            {
                if (_HeaterAnimation == null)
                {
                    _HeaterAnimation = new TmrAnimation
                        (
                        1,
                         ResourcesHelper.Instance.GetImageResource("Images/heater_off.png"),
                         ResourcesHelper.Instance.GetImageResource("Images/heater_low.png"),
                         ResourcesHelper.Instance.GetImageResource("Images/heater_on.png")
                        );
                }
                return _HeaterAnimation;
            }
        }
        #endregion
    }
}
