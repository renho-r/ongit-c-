using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace socket.mysource.m
{
    public class SocketParam : INotifyPropertyChanged
    {
        private String ip;
        private String port;

        public String Port
        {
            get { return port; }
            set { port = value; }
        }

        public String Ip
        {
            get { return ip; }
            set { ip = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
