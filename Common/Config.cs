using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdibleFungusGreenhouse.Common
{
    /// <summary>
    /// 配置信息
    /// </summary>
    public static class Config
    {
        /// <summary>
        /// ADAM4150数字采集器端口
        /// </summary>
        public static string adam4150Port = ConfigurationManager.AppSettings["Adam4150Port"];
        
        /// <summary>
        /// Zigbee四输入采集器端口
        /// </summary>
        public static string input4Port = ConfigurationManager.AppSettings["Input4Port"];

        /// <summary>
        /// 超高频桌面发卡器端口
        /// </summary>
        public static string srrReaderPort = ConfigurationManager.AppSettings["SrrReaderPort"];

        /// <summary>
        /// 摄像头IP
        /// </summary>
        public static string cameraIp = ConfigurationManager.AppSettings["CameraIp"];

        /// <summary>
        /// 摄像头用户名
        /// </summary>
        public static string cameraUser = ConfigurationManager.AppSettings["CameraUser"];

        /// <summary>
        /// 摄像头密码
        /// </summary>
        public static string cameraPassword = ConfigurationManager.AppSettings["CameraPassword"];
    }
}
