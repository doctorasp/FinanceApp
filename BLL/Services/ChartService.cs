using System;
using LiveCharts;
using LiveCharts.Wpf;

namespace BLL.Services
{
    public class ChartService
    {

        Func<ChartPoint, string> labelPoint = chartPoint =>
            string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

        public SeriesCollection buildPie()
        {
            SeriesCollection s = new SeriesCollection()
            {
                 new PieSeries
                {
                    Title = "Електроенергія",
                    Values = new ChartValues<double> {3},
                    PushOut = 15,
                    DataLabels = true,
                    LabelPoint = labelPoint
                },
                new PieSeries
                {
                    Title = "Водопостачання",
                    Values = new ChartValues<double> {4},
                    DataLabels = true,
                    LabelPoint = labelPoint
                },
                 new PieSeries
                {
                    Title = "Опалення",
                    Values = new ChartValues<double> {5},
                    DataLabels = true,
                    LabelPoint = labelPoint
                },
                 new PieSeries
                {
                    Title = "Газопостачання",
                    Values = new ChartValues<double> {5},
                    DataLabels = true,
                    LabelPoint = labelPoint
                }
            };
            return s;
        }
    }
}
