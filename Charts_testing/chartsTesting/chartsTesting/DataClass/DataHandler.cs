using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using chartsTesting.Annotations;
using SciChart.Charting.Model.DataSeries;
using SciChart.Charting.Visuals;
using SciChart.Charting.Visuals.Axes;
using SciChart.Charting.Visuals.RenderableSeries;
using SciChart.Data.Model;

namespace chartsTesting.DataClass
{
    public class DataHandler : INotifyPropertyChanged
    {
        #region  vars

        private DateRange dt;
        private XyDataSeries<DateTime, int> _firstDataSeries;

        public XyDataSeries<DateTime, int> FirstDataSeries
        {

            get => _firstDataSeries;
            set
            {
                if (_firstDataSeries != value)
                {
                    _firstDataSeries = value;
                    OnPropertyChanged();
                }
            }
        }

        private int counter;

        #endregion

        public DataHandler()
        {
            FirstDataSeries = new XyDataSeries<DateTime, int>();
            counter = 0;
        }

        public void addData()
        {
            Random ran = new Random();
            FirstDataSeries.Append(DateTime.Now.Add(TimeSpan.FromMinutes(counter)), ran.Next(1, 50));
            counter++;
        }

        public void bigData()
        {
            Random ran = new Random();
      
            for (int iterator = 0; iterator < 2000; iterator++)
            {
                if (iterator % 5 == 0)
                {
                    int chance = ran.Next(0, 5);
                    if (chance % 5 == 0)
                    {
                        FirstDataSeries.Append(DateTime.Now.Add(TimeSpan.FromMinutes(iterator)), ran.Next(ran.Next(0,10),ran.Next(75,125)));
                    }
                    else
                    {
                        FirstDataSeries.Append(DateTime.Now.Add(TimeSpan.FromMinutes(iterator)), ran.Next(1, 50));
                    }
                }
                else
                {
                    FirstDataSeries.Append(DateTime.Now.Add(TimeSpan.FromMinutes(iterator)), ran.Next(1, 50));
                }

                
                counter++;
            }
        }

            #region eventHandlers
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
