using System;

namespace EdibleFungusGreenhouse.Common
{
    /// <summary>
    /// 设备信息
    /// </summary>
    public class DeviceInfo
    {
        /// <summary>
        /// 温度值
        /// </summary>
        private double? _TemValue;
        public double? TemValue
        {
            get { return _TemValue; }
            set { _TemValue = value != null ? Math.Round(value.Value, 1) : value; }
        }

        /// <summary>
        /// 湿度值
        /// </summary>
        private double? _HumiValue;
        public double? HumiValue
        {
            get { return _HumiValue; }
            set { _HumiValue = value != null ? Math.Round(value.Value, 1) : value; }
        }

        /// <summary>
        /// 光照值
        /// </summary>
        private double? _LightValue { get; set; }
        public double? LightValue
        {
            get
            {
                return _LightValue;
            }
            set
            {
                _LightValue = value != null ? Math.Round(value.Value, 1) : value;
            }
        }

        private bool? _FlameSensorState = null;
        /// <summary>
        /// 火焰传感器状态
        /// </summary>
        public bool? FlameSensorState
        {
            get { return _FlameSensorState; }
            set
            {
                if (_FlameSensorState != value)
                {
                    _FlameSensorState = value;
                }
            }
        }

        private string _FlameSensorStateText = "N/A";
        /// <summary>
        /// 火焰传感器状态信息
        /// </summary>
        public string FlameSensorStateText
        {
            get
            {
                if (FlameSensorState == false)
                {
                    return "无";
                }
                else if (FlameSensorState == true)
                {
                    return "有";
                }
                return _FlameSensorStateText;
            }
        }

        private bool? _SmokeSensorState = null;
        /// <summary>
        /// 烟雾传感器状态
        /// </summary>
        public bool? SmokeSensorState
        {
            get { return _SmokeSensorState; }
            set
            {
                if (_SmokeSensorState != value)
                {
                    _SmokeSensorState = value;
                }
            }
        }

        private string _SmokeSensorStateText = "N/A";
        /// <summary>
        /// 烟雾传感器状态信息
        /// </summary>
        public string SmokeSensorStateText
        {
            get
            {
                if (SmokeSensorState == false)
                {
                    return "正常";
                }
                else if (SmokeSensorState == true)
                {
                    return "超标";
                }
                return _SmokeSensorStateText;
            }
        }

        private bool _IsAlarmNow = false;
        /// <summary>
        /// 是否告警
        /// </summary>
        public bool IsAlarmNow
        {
            get
            {
                if ((SmokeSensorState == true) || (FlameSensorState == true))
                    _IsAlarmNow = true;
                else
                    _IsAlarmNow = false;

                return _IsAlarmNow;
            }
        }
        /// <summary>
        /// 风扇状态
        /// </summary>
        public bool IsFanOpen { get; set; } = false;

        /// <summary>
        /// 加热器状态
        /// </summary>
        public bool IsHeaterOpen { get; set; } = false;
     
    }
}