# WPF 食用菌生产大棚项目
---
##前言
本工程使用温湿度传感器、烟雾传感器、火焰传感器等对食用菌生产大棚进行监控，使用RFID技术对食用菌进行跟踪溯源。

支持：.NET Framework 3.5-4.5<br>
支持： Windows XP - windows 7 - Windows 10<br>

Support:.NET Framework 3.5 - 4.7<br>
Support: Windows XP - Windows 7 - Windows 10





##版本更新
#####1.2（2018.3.18）
1.修改双击标题栏实现最大化、最小化事件，实现了“300毫秒检测”<br>
2.修改窗体移动事件<br>

``` ```

  			try
            {
                DragMove();
            }
            catch { }
``` ```
修改为：

``` ```

			if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
``` ```  
#####1.1（2018.3.8）
1.修改App.config文件，添加数字量采集器、ZigBee四通道采集器端口等键值对信息<br>
2.新增 主页、产品管理、内部环境、安全监控Page

#####1.0（2018.3.4）
1.窗体无边框设计<br>
2.自定义控件（最大化、最小化、关闭按钮等）<br>



