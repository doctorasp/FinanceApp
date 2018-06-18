using System;
using LiveCharts;
using LiveCharts.Wpf;

namespace BLL.Services
{
    public class ChartService
    {

        Func<ChartPoint, string> labelPoint = chartPoint =>
            string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

        public SeriesCollection buildPie(double val1=0, double val2 =0, double val3 =0, double val4 = 0)
        {
            SeriesCollection s = new SeriesCollection()
            {
                new PieSeries
                {
                    Title = "Водопостачання",
                    Values = new ChartValues<double> {val2},
                    DataLabels = true,
                    LabelPoint = labelPoint
                },
                

                   new PieSeries
                {
                    Title = "Опалення",
                    Values = new ChartValues<double> {val3},
                    DataLabels = true,
                    LabelPoint = labelPoint
                },

                    new PieSeries
                {
                    Title = "Електроенергія",
                    Values = new ChartValues<double> {val1},
                    PushOut = 15,
                    DataLabels = true,
                    LabelPoint = labelPoint
                },
                  new PieSeries
                {
                    Title = "Газопостачання",
                    Values = new ChartValues<double> {val4},
                    DataLabels = true,
                    LabelPoint = labelPoint
                }
            };
            return s;
        }
    }
}
