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

        public static DeviceHelper GetInstance()
        {
            if (instance == null)
            {
                instance = new DeviceHelper();
            }
            return instance;
        }

        ///// <summary>
        ///// 实例对象
        ///// </summary>
        //public static DeviceHelper Instance
        //{
        //    get
        //    {
        //        if (null == instance)
        //        {
        //            lock (typeof(DeviceHelper))
        //            {
        //                if (null == instance)
        //                {
        //                    instance = new DeviceHelper();
        //                }
        //            }
        //        }
        //        return instance;
        //    }
        //}
        #endregion

        #endregion

        #region 公有成员
        private SetParams _SetParams;
        public SetParams SetParams
        {
            set
            {
                if (value!=null)
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
        private System.Timers.Timer timer = null;

        private System.Timers.Timer Timer
        {
            get
            {
                if (timer==null)
                {
                    timer = new System.Timers.Timer();
                }
                return timer;
            }
        }

        public void Dispose()
        {
            Stop();
        }

        /// <summary>
        /// 结束数据采集
        /// </summary>
        private void Stop()
        {
            //TmrGetDeviceData.Dispose();
        }
    }
}
