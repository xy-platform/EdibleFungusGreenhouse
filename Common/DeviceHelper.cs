using DeviceBase;
using System;

namespace EdibleFungusGreenhouse.Common
{
    public class DeviceHelper : IDisposable
    {
        #region 构造函数

        private DeviceHelper() { }

        #region 单例
        private static DeviceHelper instance;

        //public static DeviceHelper GetInstance()
        //{
        //    if (instance == null)
        //    {
        //        instance = new DeviceHelper();
        //    }
        //    return instance;
        //}

        /// <summary>
        /// 实例对象
        /// </summary>
        public static DeviceHelper Instance
        {
            get
            {
                if (null == instance)
                {
                    lock (typeof(DeviceHelper))
                    {
                        if (null == instance)
                        {
                            instance = new DeviceHelper();
                        }
                    }
                }
                return instance;
            }
        }
        #endregion

        #endregion

        #region 公有成员
        private SetParams _SetParams;
        public SetParams SetParams
        {
            set
            {
                if (value != null)
                {
                    _SetParams.LowLight = value.LowLight;
                    _SetParams.LowTemp = value.LowTemp;
                    _SetParams.LowHumi = value.LowHumi;
                    _SetParams.HighLight = value.HighLight;
                    _SetParams.HighHumi = value.HighHumi;
                    _SetParams.HighTemp = value.HighTemp;
                }
            }
            get
            {
                if (_SetParams == null)
                {
                    _SetParams = new SetParams();
                }
                return _SetParams;
            }
        }

        #endregion
       
        #region 私有成员

        /// <summary>
        /// 设备数据
        /// </summary>
        private DeviceInfo _info = new DeviceInfo();

        /// <summary>
        /// ADAM4150数字量采集器
        /// </summary>
        private ADAM4150 _adam = new ADAM4150(Config.adam4150Port);

        /// <summary>
        /// Zigbee四通道采集器
        /// </summary>
        private Input_4 _input4 = new Input_4(Config.input4Port);

        /// <summary>
        /// 数据采集定时器
        /// </summary>
        private System.Timers.Timer _timerGetData = null;

        private System.Timers.Timer TimerGetData
        {
            get
            {
                if (_timerGetData == null)
                {
                    _timerGetData = new System.Timers.Timer
                    {
                        AutoReset = false,
                        Interval = 2000
                    };
                    _timerGetData.Elapsed += Timer_Tick;
                    _timerGetData.Disposed += _TimerGetData_Disposed;
                }
                return _timerGetData;
            }
        }

       
        #endregion

        #region 私有方法

        /// <summary>
        /// 关闭串口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _TimerGetData_Disposed(object sender, EventArgs e)
        {
            if (_adam != null)
            {
                ControlFan(false);
                ControlHeart(false);
                ControlAlarmLamp(false);
                _adam.Close();
            }
            if (_adam != null)
            {
                _adam.Close();
            }
        }

        /// <summary>
        /// 定时器事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                lock (this)
                {
                    if (_input4.Open())
                    {
                        _info.LightValue = Convert.ToDouble(_input4.lightValue);
                        _info.TemValue = Convert.ToDouble(_input4.temperatureValue);
                        _info.HumiValue = Convert.ToDouble(_input4.humidityValue);
                    }
                    _adam.SetData();
                    _info.FlameSensorState = _adam.DI1;
                    _info.SmokeSensorState = _adam.DI2;

                    ExceedAlarm();
                    ExceedTmepLimit();
                }
            }
            catch
            {
                return;
            }
            finally
            {
                TimerGetData.Enabled = true;
                if (OnReadCompleted != null)
                {
                    OnReadCompleted.Invoke(_info);
                }
            }
        }

        /// <summary>
        /// 判断是否有火焰烟雾报警
        /// </summary>
        private void ExceedAlarm() => ControlAlarmLamp(_info.IsAlarmNow);

        /// <summary>
        /// 判断是否超出温度限制
        /// </summary>
        private void ExceedTmepLimit()
        {
            //高温
            if (_info.TemValue < SetParams.LowTemp)
            {
                ControlHeart(true);
                ControlFan(false);
            }
            //低温
            else if (_info.TemValue > SetParams.HighTemp)
            {
                ControlHeart(false);
                ControlFan(true);
            }
            //标准温度
            else
            {
                ControlHeart(false);
                ControlFan(false);
            }
        }
        #endregion

        #region 共有方法

        /// <summary>
        /// 数据读取完回调
        /// </summary>
        public event DgOnReadCompleted OnReadCompleted;

        public delegate void DgOnReadCompleted(DeviceInfo infos);

        /// <summary>
        /// 控制风扇
        /// </summary>
        /// <param name="v"></param>
        public void ControlFan(bool state)
        {
            if (state)
            {
                if (_adam.OnOff(ADAM4150FuncID.OnDO1))
                {
                    _info.IsFanOpen = state;
                }
            }
            else
            {
                if (_adam.OnOff(ADAM4150FuncID.OffDO1))
                {
                    _info.IsFanOpen = state;
                }
            }
        }

        /// <summary>
        /// 控制加热器
        /// </summary>
        /// <param name="state"></param>
        public void ControlHeart(bool state)
        {
            if (state)
            {
                if (_adam.OnOff(ADAM4150FuncID.OnDO2))
                {
                    _info.IsHeaterOpen = state;
                }
            }
            else
            {
                if (_adam.OnOff(ADAM4150FuncID.OffDO2))
                {
                    _info.IsHeaterOpen = state;
                }
            }
        }

        /// <summary>
        /// 控制报警灯
        /// </summary>
        /// <param name="isAlarmNow"></param>
        public void ControlAlarmLamp(bool isAlarmNow)
        {
            if (isAlarmNow)
            {
                _adam.OnOff(ADAM4150FuncID.OnDO0);
            }
            else
            {
                _adam.OnOff(ADAM4150FuncID.OffDO0);
            }
        }

        /// <summary>
        /// Dispose释放资源
        /// </summary>
        public void Dispose()
        {
            Stop();
        }

        /// <summary>
        /// 开始数据采集
        /// </summary>
        public void Start()
        {
            TimerGetData.Start();
        }

        /// <summary>
        /// 结束数据采集
        /// </summary>
        public void Stop()
        {
            TimerGetData.Dispose();
        } 
        #endregion
    }
}
