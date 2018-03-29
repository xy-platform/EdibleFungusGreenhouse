# WPF 食用菌生产大棚项目
---
## 前言
本工程使用温湿度传感器、烟雾传感器、火焰传感器、摄像头、桌面超高频读卡器、报警灯、ADAM4150数字量采集模块等作为核心设备，对食用菌生产大棚进行监控，使用RFID技术对食用菌进行跟踪溯源，本项目将实现如下功能：<br>

(1)为食用菌成品绑定RFID，实现食品的跟踪溯源。<br>
(2)通过读取光照传感器的数据，智能化开启日光灯。<br>
(3)通过读取温湿度传感器的数据，智能化开启空调和暖气（以风扇和LED灯模拟）。<br>
(4)通过读取火焰、烟雾传感器的数据，判断食用菌生产大棚是否处于危险状态，如果处于危险状态则开启报警灯并且在程序界面上反馈给用户提示信息。

支持：.NET Framework 4.5<br>
支持： Windows XP - windows 7 - Windows 10<br>

Support:.NET Framework 4.5<br>
Support: Windows XP - Windows 7 - Windows 10

硬件环境：<br>

![image](https://github.com/hongjiapeng/EdibleFungusGreenhouse/blob/master/Images/device.png) 

开发工具：Visual Studio Community 2017

## 注意
1.项目中有些地方用到了[C#6.0新特性](https://www.oschina.net/translate/new-features-in-csharp)，自动属性初始化器。<br>


## 演示动画

#### 产品管理页面
![image](https://github.com/hongjiapeng/EdibleFungusGreenhouse/blob/master/Images/management.gif)   
#### 内部环境页面
![image](https://github.com/hongjiapeng/EdibleFungusGreenhouse/blob/master/Images/environment.gif)   
#### 安全监控页面
![image](https://github.com/hongjiapeng/EdibleFungusGreenhouse/blob/master/Images/monitor.gif)



## 版本更新

##### 1.4（2018.3.23）
1.新增“安全监控”代码<br>
2.修改检测到火焰或烟雾直接弹出警报代码，根据“有烟无火”、“有火无烟”，“无火无烟”三种状态给用户具体、详细的提示信息。<br>
3.新增监控页面摄像头方向控制功能，界面如下。<br>
![image](https://github.com/hongjiapeng/EdibleFungusGreenhouse/blob/master/Images/dir_control.png) 

##### 1.3（2018.3.19）
1.完成产品管理界面，扫描电子标签功能，录制此页面gif<br>

##### 1.2（2018.3.18）
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
##### 1.1（2018.3.8）
1.修改App.config文件，添加数字量采集器、ZigBee四通道采集器端口等键值对信息<br>
2.新增 主页、产品管理、内部环境、安全监控Page

##### 1.0（2018.3.4）
1.窗体无边框设计<br>
2.自定义控件（最大化、最小化、关闭按钮等）<br>



