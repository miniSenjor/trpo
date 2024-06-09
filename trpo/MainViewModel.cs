using OxyPlot.Series;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trpo
{
    internal class MainViewModel
    {
        public PlotModel PlotModel1 { get; set; }
        public PlotModel PlotModel2 { get; set; }

        public MainViewModel()
        {
            PlotModel1 = new PlotModel { Title = "График функции" };

            var series1 = new LineSeries { Title = "Мой график" };
            for (double x = 0; x <= 100; x += 1)
            {
                double y = Math.Log(x);

                series1.Points.Add(new DataPoint(x, y));
            }

            PlotModel1.Series.Add(series1);

            PlotModel2 = new PlotModel { Title = "График функции" };

            var series2 = new LineSeries { Title = "Мой график" };
            for (double x = 0; x <= 100; x += 1)
            {
                double y = x*x;

                series2.Points.Add(new DataPoint(x, y));
            }

            PlotModel2.Series.Add(series2);
        }
    }
}
