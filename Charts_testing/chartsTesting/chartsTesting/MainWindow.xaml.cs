
using System;
using System.Timers;
using System.Windows;
using System.Windows.Threading;
using chartsTesting.DataClass;
using SciChart.Charting.ChartModifiers;
using SciChart.Charting.Model.DataSeries;
using SciChart.Charting.Visuals.Axes;
using SciChart.Data.Model;

namespace chartsTesting
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region vars
        private NumericAxis _yAxis;
        private DateTimeAxis _xAxis;
        private readonly DataHandler _dataHandler;
        private readonly DispatcherTimer _timer;
        private DateRange _dt;
        

        #endregion
        public MainWindow()
        {
            InitializeComponent();
   
            _dt = new DateRange(DateTime.Now, DateTime.Now.AddHours(6));
            _timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Tick += Timer_Tick;
            _dataHandler = new DataHandler();
            createGraph();
            _dataHandler.bigData();
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            _dataHandler.addData();
     
        }

        private void createGraph()
        {
            boxFace.Text = TestChart.RenderSurface.GetType().Name.ToString();
            _yAxis = new NumericAxis() {AxisTitle = "values" };
            _yAxis.VisibleRange = new DoubleRange(0,125);
            _xAxis = new DateTimeAxis() { AxisTitle = "time"};
            _xAxis.VisibleRange = new DateRange(DateTime.Now,  DateTime.Now.AddHours(6));

            TestChart.XAxis = _xAxis;
            TestChart.YAxis = _yAxis;

            // Specify Interactivity Modifiers
            TestChart.ChartModifier = new ModifierGroup(new RubberBandXyZoomModifier(), new ZoomExtentsModifier());

            TestChart.DataContext = _dataHandler;
            ScrollingViewportManager scroll = new ScrollingViewportManager(TimeSpan.FromHours(6));
            TestChart.ViewportManager = scroll;
        }
    }
}
