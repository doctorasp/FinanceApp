using System;
using System.Linq;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using LiveCharts.Wpf.Charts.Base;

namespace BLL.Services
{
    public class ChartService
    {

        Func<ChartPoint, string> labelPoint = chartPoint =>
            string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
        

        public SeriesCollection buildPie(double val1=0, double val2 =0, double val4 = 0)
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

        public SeriesCollection buildIncome(double incomeSum, double outcomeSum )
        {
            SeriesCollection s = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Прибуток за поточний місяць",
                    Values = new ChartValues<double> { incomeSum}
                }
            };

            s.Add(new ColumnSeries
            {
                Title = "Витрати за поточний місяць",
                Values = new ChartValues<double> { outcomeSum }
            });

            Labels = new[] { "Maria" };
            Formatter = value => value.ToString("N");

            return s;
        }

        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

    }
}
