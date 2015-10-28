using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using socket.mysource.m;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Windows.Threading;


namespace socket
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SocketParam socketParam = new SocketParam();
        SendMsg sendMsg = new SendMsg();
        Socket netSocket = null;

        public MainWindow()
        {
            initParam();
            InitializeComponent();
            Binding bind = new Binding();
            bind.Source = socketParam;
            bind.Path = new PropertyPath("Ip");
            this.socketIp.SetBinding(TextBox.TextProperty, bind);

            bind = new Binding();
            bind.Source = socketParam;
            bind.Path = new PropertyPath("Port");
            this.socketPort.SetBinding(TextBox.TextProperty, bind);

            bind = new Binding();
            bind.Source = sendMsg;
            bind.Path = new PropertyPath("SendMsgData");
            this.textSendMsg.SetBinding(TextBox.TextProperty, bind);
        }

        private void initParam()
        {
            this.socketParam.Ip = System.Configuration.ConfigurationSettings.AppSettings["ip"];
            this.socketParam.Port = System.Configuration.ConfigurationSettings.AppSettings["port"];
        }

        private void ClickConnect(object sender, RoutedEventArgs e)
        {
            IPAddress ip = IPAddress.Parse(this.socketParam.Ip);
            IPEndPoint ipe = new IPEndPoint(ip, int.Parse(this.socketParam.Port));
            netSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                netSocket.Connect(ipe);
            }
            catch (System.Net.Sockets.SocketException snss)
            {
                MessageBox.Show("连接失败!");
                return;
            }

            Thread thread = new Thread(new ThreadStart(this.receiveMsgTask));
            thread.Name = "renhoNetSocket";
            thread.IsBackground = true;
            thread.Start();
            MessageBox.Show("连接成功!");
        }

        private void BtnSendClick(object sender, RoutedEventArgs e)
        {
            if (null == netSocket)
            {
                MessageBox.Show("请先连接...");
                return;
            } 
            else 
            { 
                String sendMsg0 = this.sendMsg.SendMsgData;
                if(null == sendMsg0 || "".Equals(sendMsg0)) {
                    MessageBox.Show("没有需要发送的内容!");
                    return;
                }
                byte[] bs = Encoding.UTF8.GetBytes(sendMsg0);
                netSocket.Send(bs, bs.Length, 0);
            }
        }

        public void receiveMsgTask()
        {
            while (true)
            {
                byte[] bytes = new byte[1024];
                string data = "";

                //等待接收消息
                try
                {
                    int bytesRec = netSocket.Receive(bytes);
                    data += Encoding.UTF8.GetString(bytes, 0, bytesRec);
                    ShowText("receive from server:" + data);
                    //textReceiveMsg.Dispatcher.BeginInvoke(setReceiveText, DispatcherPriority.Normal, new string[] { data });
                    //this.textReceiveMsg.Text = data;
                    //ReceiveText("收到消息：" + data);
                    Console.WriteLine(data);
                }
                catch (System.Net.Sockets.SocketException ode)
                {
                    MessageBox.Show("连接已中断!");
                    netSocket = null;
                    break;
                }
            }
        }

        public delegate void ShowTextHandler(string text);
        ShowTextHandler setReceiveText;

        private void ShowText(string text)
        {
            if (System.Threading.Thread.CurrentThread != textReceiveMsg.Dispatcher.Thread)
            {
                if (setReceiveText == null)
                {
                    setReceiveText = new ShowTextHandler(ShowText);
                }
                textReceiveMsg.Dispatcher.BeginInvoke(setReceiveText, DispatcherPriority.Normal, new string[] { text });
            }
            else
            {
                textReceiveMsg.AppendText(text + "\n");
            }
        }

        private void ClickClose(object sender, RoutedEventArgs e)
        {
            if(null != netSocket) 
            {
                netSocket.Close();
            }
        }

        private void BtnCleanClick(object sender, RoutedEventArgs e)
        {
            textReceiveMsg.Text = "";
        }

    }

    class SendMsg 
    {
        private String sendMsgData;

        public String SendMsgData
        {
            get { return sendMsgData; }
            set { sendMsgData = value; }
        }
    }
}
