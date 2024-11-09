using OxyPlot.Series;
using OxyPlot;
using System;

namespace trpo
{
    internal class MainViewModel
    {
        public PlotModel plotModel { get; set; }
        public PlotModel plotModelDefalt { get; set; }
        public MainViewModel() 
        {
            plotModelDefalt = new PlotModel { Title = "Сравнение методов сортировки" };
            plotModelDefalt.Axes.Add(new OxyPlot.Axes.LinearAxis
            {
                Position = OxyPlot.Axes.AxisPosition.Bottom,
                Title = "Кол-во элементов"
            });
            plotModelDefalt.Axes.Add(new OxyPlot.Axes.LinearAxis
            {
                Position = OxyPlot.Axes.AxisPosition.Left,
                Title = "Кол-во перестановок"
            });
        }
        public MainViewModel(int count)
        {
            plotModel = new PlotModel { Title = "Сравнение методов сортировки"};
            var seriesBinari = new LineSeries 
            { 
                Title = "Сортировка бинарными вставками O(n*ln(n))",
                Color = OxyColor.FromRgb(0, 128, 0)
            };
            for (double x = 0; x <= count; x += 1)
            {
                double y = x * Math.Log(x);

                seriesBinari.Points.Add(new DataPoint(x, y));
            }

            var seriesSimple = new LineSeries 
            {
                Title = "Сортировка простыми вставками O(n^2)",
                Color = OxyColor.FromRgb(255, 0, 0)
            };
            for (double x = 0; x <= count; x += 1)
            {
                double y = x * x;

                seriesSimple.Points.Add(new DataPoint(x, y));
            }

            plotModel.Series.Add(seriesBinari);
            plotModel.Series.Add(seriesSimple);

            plotModel.Axes.Add(new OxyPlot.Axes.LinearAxis
            {
                Position = OxyPlot.Axes.AxisPosition.Bottom,
                Title = "Кол-во элементов"
            });
            plotModel.Axes.Add(new OxyPlot.Axes.LinearAxis
            {
                Position = OxyPlot.Axes.AxisPosition.Left,
                Title = "Кол-во перестановок"
            });


        }
    }
}
