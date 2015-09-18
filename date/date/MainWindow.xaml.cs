using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace date
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        Data data = new Data();
        public MainWindow()
        {
            InitializeComponent();
            Binding bind = new Binding();
            bind.Source = data;
            bind.Path = new PropertyPath("NumData");
            this.textBox.SetBinding(TextBox.TextProperty, bind);

            bind = new Binding();
            bind.Source = data;
            bind.Path = new PropertyPath("StringData");
            this.textBoxShow.SetBinding(TextBox.TextProperty, bind);
        }

        private void showTime(object sender, RoutedEventArgs e)
        {
            if(null == data.NumData || "".Equals(data.NumData))
            {
                MessageBox.Show("请输入UNIX时间戳!");
                return;
            }
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            try
            {
                dt = dt.AddMilliseconds(long.Parse(data.NumData)).ToLocalTime();
                data.StringData = dt.ToString("yyyy-MM-dd HH:mm:ss.fff");
            }
            catch (ArgumentOutOfRangeException aoore) 
            {
                MessageBox.Show("UNIX时间戳超出范围!");
                return;
            }
        }
        private void exit(object sender, RoutedEventArgs e){
            Application.Current.MainWindow.Close();
        }
    }

    public class Data : INotifyPropertyChanged
    {
        private String numData;
        private String stringData;

        public String NumData
        {
            get { return numData; }
            set
            {
                numData = value;
                if (PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("NumData"));
                }
            }
        }
        public String StringData
        {
            get { return stringData; }
            set
            {
                stringData = value;
                if (PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("StringData"));
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
