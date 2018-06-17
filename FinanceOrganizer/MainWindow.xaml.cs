using BLL.Services;
using DAL.EF;
using DAL.Entities;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Data.Entity;
using System.Windows;

namespace FinanceOrganazer
{

    public partial class MainWindow : Window
    {
        DatabaseContext _context;
        ChartService cs;
        public MainWindow()
        {
            InitializeComponent();

            cs = new ChartService();

            this.pieChart1.Series = cs.buildPie();

            this.pieChart1.LegendLocation = LegendLocation.Bottom;

            _context = new DatabaseContext();
            _context.Electricity.Load();
            _context.Water.Load();
            _context.Gas.Load();

            electricityGrid.ItemsSource = _context.Electricity.Local.ToBindingList();
            waterGrid.ItemsSource = _context.Water.Local.ToBindingList();
            gasGrid.ItemsSource = _context.Gas.Local.ToBindingList();
            this.Closing += MainWindow_Closing;
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

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (electricityGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < electricityGrid.SelectedItems.Count; i++)
                {
                    Electricity el = electricityGrid.SelectedItems[i] as Electricity;
                    if (el != null)
                    {
                        _context.Electricity.Remove(el);
                    }
                }
            }
            _context.SaveChanges();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(this.startPrice.Text!="" && this.endPrice.Text!= "" && this.date.Text!= "" && this.tariffPer100.Text!= "" && this.tariffAmong100.Text != "")
            {
                ElectricityService es = new ElectricityService();
                int startValue = Convert.ToInt32(this.startPrice.Text);
                int endValue = Convert.ToInt32(this.endPrice.Text);
                decimal tariffPer100 = Decimal.Parse(this.tariffPer100.Text) / 10;
                decimal tariffAmong100 = Convert.ToDecimal(this.tariffAmong100.Text);
                decimal calculatedPrice = es.CalculatePrice(startValue, endValue, tariffPer100, tariffAmong100);
                DateTime dateTime = DateTime.Parse(this.date.Text);

                MessageBox.Show(
                    $"\nСпожито: {es.Consumed} кВт∙год" +
                    $"\nЗа 100 кВт∙год: {es.FirstBlock} грн." +
                    $"\nРешта кВт∙год: {es.SecondBlock} грн." +
                    $"\nВсього ціна: {calculatedPrice} грн."
               );
                Electricity electricity = new Electricity()
                {
                    StartValue = startValue,
                    EndValue = endValue,
                    Consumed = es.Consumed,
                    PricePer100 = es.FirstBlock,
                    PriceAmong100 = es.SecondBlock,
                    Date = dateTime,
                    Price = es.Price()
                };
                _context.Electricity.Add(electricity);
                _context.SaveChanges();
            }
            else
            {
                MessageBox.Show("Всі поля повинні бути заповнені");
            }
            
        }

        private void deleteWaterButton_Click(object sender, RoutedEventArgs e)
        {
            if (waterGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < waterGrid.SelectedItems.Count; i++)
                {
                    Water el = waterGrid.SelectedItems[i] as Water;
                    if (el != null)
                    {
                        _context.Water.Remove(el);
                    }
                }
            }
            _context.SaveChanges();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            if (this.waterCounterStart.Text != "" && this.waterCounterValue.Text != "" && this.waterTariff.Text != "" && this.waterDate.Text != "")
            {
                WaterService ws = new WaterService();
                int startValue = Convert.ToInt32(this.waterCounterStart.Text);
                int counterValue = Convert.ToInt32(this.waterCounterValue.Text);
                decimal tariff = Decimal.Parse(this.waterTariff.Text) /100;

                decimal calculatedPrice = ws.CalculatePrice(startValue, counterValue, tariff);
                DateTime dateTime = DateTime.Parse(this.waterDate.Text);

                MessageBox.Show(
                    $"\nСпожито: {ws.Consumed} од. куб." +
                    $"\nВсього ціна: {calculatedPrice} грн."
               );
                Water water = new Water()
                {
                    CounterValue = counterValue,
                    Date = dateTime,
                    Price = calculatedPrice
                    
                };
                _context.Water.Add(water);
                _context.SaveChanges();
            }
            else
            {
                MessageBox.Show("Всі поля повинні бути заповнені");
            }
        }

        private void TabControl_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

            if (this.gasCounter.Text != "" && this.gasPrev.Text != "" && this.gasCurr.Text != "" && this.gasTariff.Text != "" && this.gasDate.Text!="")
            {
                GasService gs = new GasService();
                int startValue = Convert.ToInt32(this.gasPrev.Text);
                int counterValue = Convert.ToInt32(this.gasCurr.Text);
                decimal tariff = Decimal.Parse(this.gasTariff.Text) / 100;

                decimal calculatedPrice = gs.CalculatePrice(startValue, counterValue, tariff);
                DateTime dateTime = DateTime.Parse(this.gasDate.Text);

                MessageBox.Show(
                    $"\nСпожито: {gs.Consumed}" +
                    $"\nВсього ціна: {calculatedPrice} грн."
               );
                Gas gas = new Gas()
                {
                    CounterNumber = Convert.ToInt32(gasCounter.Text),
                    Date = dateTime,
                    Price = calculatedPrice,
                    CurrentValue = counterValue,
                    PrevValue = startValue

                };
                _context.Gas.Add(gas);
                _context.SaveChanges();
            }
            else
            {
                MessageBox.Show("Всі поля повинні бути заповнені");
            }
        }

        private void deleteGasbtn_Click(object sender, RoutedEventArgs e)
        {
            if (gasGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < gasGrid.SelectedItems.Count; i++)
                {
                    Gas el = gasGrid.SelectedItems[i] as Gas;
                    if (el != null)
                    {
                        _context.Gas.Remove(el);
                    }
                }
            }
            _context.SaveChanges();
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
           

        }
    }

}
