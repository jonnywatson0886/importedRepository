using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SciChart.Charting.ViewportManagers;
using SciChart.Charting.Visuals;
using SciChart.Charting.Visuals.Axes;
using SciChart.Data.Model;

namespace chartsTesting
{
    /// <summary>
    /// The following class will apply a scrolling window to the chart unles the user is zooming or panning
    /// </summary>
    public class ScrollingViewportManager : DefaultViewportManager
    {
        private readonly TimeSpan _windowSize;

        public ScrollingViewportManager(TimeSpan windowSize)
        {
            _windowSize = windowSize;
        }

        public override void AttachSciChartSurface(ISciChartSurface scs)
        {
            base.AttachSciChartSurface(scs);
            this.ParentSurface = scs;
        }

        public ISciChartSurface ParentSurface { get; private set; }

        protected override IRange OnCalculateNewXRange(IAxis xAxis)
        {
            // The Current XAxis VisibleRange
            var currentVisibleRange = xAxis.VisibleRange.AsDoubleRange();
            // The MaxXRange is the VisibleRange on the XAxis if we were to zoom to fit all data
            var maxXRange = xAxis.GetMaximumRange().AsDoubleRange();
            double xMax = Math.Max(maxXRange.Max, currentVisibleRange.Max);
            // Scroll showing latest window size
            return new DoubleRange(xMax - _windowSize.TotalMilliseconds, xMax);
        }
    }
}
