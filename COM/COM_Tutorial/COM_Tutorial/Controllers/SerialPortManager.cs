using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace COM_Tutorial.Controllers 
{
    public class SerialPortManager : INotifyPropertyChanged
    {
        private SerialPort ComPort;
        private List<String> _Bindablecollection;


        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public List<string> Bindablecollection
        {
            get { return _Bindablecollection; }
            set
            {
                if (value != _Bindablecollection)
                {
                    _Bindablecollection = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public SerialPortManager()
        {
            ComPort = new SerialPort();
            Bindablecollection = new List<string>();
        }

        public void GetSerialPorts()
        {
            string[] ArrayComPortsNames = null;
            int index = -1;
            string ComPortName = null;

            ArrayComPortsNames = SerialPort.GetPortNames();
            do
            {
                index += 1;
                Bindablecollection.Add(ArrayComPortsNames[index]);
            }
            while (!((ArrayComPortsNames[index] == ComPortName) ||
                        (index == ArrayComPortsNames.GetUpperBound(0))));
        }
    }
}
