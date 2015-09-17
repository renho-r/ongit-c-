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

namespace wpf
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public String tValue = "123";
        public MainWindow()
        {
            InitializeComponent();
            textBox.SetBinding(TextBox.TextProperty, new Binding(".") { Source = tValue, Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            String s = tValue;
            MessageBox.Show("refresh:" + s);
            tValue = "renho";
        }
    }
}
