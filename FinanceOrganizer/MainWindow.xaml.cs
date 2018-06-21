using BLL.Services;
using DAL.EF;
using DAL.Entities;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace FinanceOrganazer
{

    public partial class MainWindow : Window
    {
        public DatabaseContext _context;
        ChartService cs;

        
        public MainWindow()
        {
            InitializeComponent();

            cs = new ChartService();

            _context = new DatabaseContext();

            drawChart();
            drawDoughnut();


            DataContext = this;

            this.Closing += MainWindow_Closing;
        }

        public void drawDoughnut()
        {
            double eat = 0, tr = 0, sal = 0, car = 0;

            if (!_context.Eat.Any())
            {
                eat = 0;
            }
            else
            {
                var eatQ = (from x in _context.Eat where x.Date.Month == DateTime.Now.Month orderby x.Id descending select x.Price).Sum();
                eat = Double.Parse(eatQ.ToString());
            };

            if (!_context.Transport.Any())
            {
                tr = 0;
            }
            else
            {
                var trQ = (from x in _context.Transport where x.Date.Month == DateTime.Now.Month orderby x.Id descending select x.Price).Sum();
                tr = Double.Parse(trQ.ToString());
            };

            if (!_context.Salary.Any())
            {
                sal = 0;
            }
            else
            {
                var salQ = (from x in _context.Salary where x.Date.Month == DateTime.Now.Month orderby x.Id descending select x.Price).Sum();
                sal = Double.Parse(salQ.ToString());

            };

            if (!_context.Car.Any())
            {
                car = 0;
            }
            else
            {
                var carQ = (from x in _context.Car where x.Date.Month == DateTime.Now.Month orderby x.Id descending select x.Price).Sum();
                car = Double.Parse(carQ.ToString());
            };

            //Комуналка
            var query1 = (from x in _context.Electricity where x.Date.Month == DateTime.Now.Month orderby x.Id descending select x.Price).FirstOrDefault();
            var query2 = (from x in _context.Water where x.Date.Month == DateTime.Now.Month orderby x.Id descending select x.Price).FirstOrDefault();
            var query3 = (from x in _context.Gas where x.Date.Month == DateTime.Now.Month orderby x.Id descending select x.Price).FirstOrDefault();

            double val1 = Convert.ToDouble(query1);
            double val2 = Convert.ToDouble(query2);
            double val3 = Convert.ToDouble(query3);

            double sum = Math.Round((val1 + val2 + val3), 2);

            SeriesCollection = new SeriesCollection()
            {
                new PieSeries
                {
                    Title = "Комуналка",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(sum) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Їжа",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(eat) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Транспорт",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(tr) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Зарплата",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(sal) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Машина",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(car) },
                    DataLabels = true
                }
            };
            this.Chart.Series = SeriesCollection;
            this.spendingSum.Text = "-"+(sum + eat + tr + sal + car).ToString();
    }
        public SeriesCollection SeriesCollection { get; set; }


        private void UpdateAllOnClick(object sender, RoutedEventArgs e)
        {
            var r = new Random();

            foreach (var series in SeriesCollection)
            {
                foreach (var observable in series.Values.Cast<ObservableValue>())
                {
                    observable.Value = r.Next(0, 10);
                }
            }
        }

        private void AddSeriesOnClick(object sender, RoutedEventArgs e)
        {
            var r = new Random();
            var c = SeriesCollection.Count > 0 ? SeriesCollection[0].Values.Count : 5;

            var vals = new ChartValues<ObservableValue>();

            for (var i = 0; i < c; i++)
            {
                vals.Add(new ObservableValue(r.Next(0, 10)));
            }

            SeriesCollection.Add(new PieSeries
            {
                Values = vals
            });
        }

        private void RemoveSeriesOnClick(object sender, RoutedEventArgs e)
        {
            if (SeriesCollection.Count > 0)
                SeriesCollection.RemoveAt(0);
        }

        private void AddValueOnClick(object sender, RoutedEventArgs e)
        {
            var r = new Random();
            foreach (var series in SeriesCollection)
            {
                series.Values.Add(new ObservableValue(r.Next(0, 10)));
            }
        }

        private void RemoveValueOnClick(object sender, RoutedEventArgs e)
        {
            foreach (var series in SeriesCollection)
            {
                if (series.Values.Count > 0)
                    series.Values.RemoveAt(0);
            }
        }

        private void RestartOnClick(object sender, RoutedEventArgs e)
        {
            Chart.Update(true, true);
        }

        public void drawChart()
        {
            //PieChart
            var query1 = (from x in _context.Electricity where x.Date.Month == DateTime.Now.Month orderby x.Id descending select x.Price).FirstOrDefault();
            var query2 = (from x in _context.Water where x.Date.Month == DateTime.Now.Month orderby x.Id descending select x.Price).FirstOrDefault();
            var query3 = (from x in _context.Gas where x.Date.Month == DateTime.Now.Month orderby x.Id descending select x.Price).FirstOrDefault();

            double val1 = Convert.ToDouble(query1);
            double val2 = Convert.ToDouble(query2);
            double val3 = Convert.ToDouble(query3);

            this.pieChart1.Series = cs.buildPie(val1, val2, val3);

            decimal sum = Math.Round((decimal)(val1 + val2 + val3), 2);
            this.pieChart1.LegendLocation = LegendLocation.Bottom;

            this.spendingComSum.Text = "-"+sum.ToString();
        }



        private void Chart_OnDataClick(object sender, ChartPoint chartpoint)
        {
            var chart = (LiveCharts.Wpf.PieChart)chartpoint.ChartView;

            //clear selected slice.
            foreach (PieSeries series in chart.Series)
                series.PushOut = 0;

            var selectedSeries = (PieSeries)chartpoint.SeriesView;
            selectedSeries.PushOut = 8;
        }


        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _context.Dispose();
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            _context.SaveChanges();
        }

        private void TabControl_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            string tabItem = ((sender as TabControl).SelectedItem as TabItem).Header as string;

            switch (tabItem)
            {
                case "Витрати":
                    //drawChart();
                    break;
                case "Фінанси":
                    //drawDoughnut();
                    break;

                default:
                    return;
            }
        }

       

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //TextBlock x1 = this.electricityGrid.Columns[6].GetCellContent(this.electricityGrid.Items[this.electricityGrid.Items.Count-2]) as TextBlock;
            //TextBlock x2 = this.waterGrid.Columns[2].GetCellContent(this.waterGrid.Items[this.waterGrid.Items.Count - 2]) as TextBlock;
            //TextBlock x3 = this.gasGrid.Columns[4].GetCellContent(this.gasGrid.Items[this.gasGrid.Items.Count - 2]) as TextBlock;

        }

        private void TabItem_Loaded(object sender, RoutedEventArgs e)
        {
            drawChart();
        }

        private void tabItem2_Clicked(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MessageBox.Show("+");
        }

        //private void Button_Click_4(object sender, RoutedEventArgs e)
        //{
        //    string elSearch = this.electSearch.Text;

        //    if (!String.IsNullOrEmpty(elSearch))
        //    {
        //        var query = (from q in _context.Electricity where q.EndValue.ToString() == elSearch select q).ToList();
        //        electricityGrid.ItemsSource = query;
        //    }
        //    else
        //    {
        //        electricityGrid.ItemsSource = _context.Electricity.Local.ToBindingList();
        //    }
           
        //}

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            new AddWindow(this).Show();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            new DeleteWindow(this).Show();
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            new AddCom(this).Show();
        }
    }

}
