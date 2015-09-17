using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace wpffile
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void clickBtn(object sender, RoutedEventArgs e)
        {
            String path = this.textBox.Text;
            StreamReader sr = new StreamReader(path, Encoding.Default);
            String line;
            String readStr = "";
            while ((line = sr.ReadLine()) != null)
            {
                readStr += line + "\n";
            }
            this.tbb.Text = readStr;
        }

        private void click_selectFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\user\renho";//注意这里写路径时要用c:\\而不是c:\
            openFileDialog.Filter = "文本文件|*.*|C#文件|*.cs|所有文件|*.*";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            openFileDialog.ShowDialog();
            if (openFileDialog.FileName != string.Empty)
            {
                this.textBox.Text = openFileDialog.FileName;
            }
        }
    }
}
