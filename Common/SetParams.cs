using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdibleFungusGreenhouse.Common
{
    /// <summary>
    /// 设置参数表
    /// </summary>
    public class SetParams
    {
        /// <summary>
        /// 关照下限
        /// </summary>
        public int LowLight { set; get; } = 10;

        /// <summary>
        /// 光照上限
        /// </summary>
        public int HighLight { get; set; } = 20;

        /// <summary>
        /// 温度下限
        /// </summary>
        public int LowTemp { get; set; } = 10;

        /// <summary>
        /// 温度上限
        /// </summary>
        public int HighTemp { get; set; } = 20;

        /// <summary>
        /// 湿度下限
        /// </summary>
        public int LowHumi { get; set; } = 10;

        /// <summary>
        /// 湿度上限
        /// </summary>
        public int HighHumi { get; set; } = 20;
    }
}
