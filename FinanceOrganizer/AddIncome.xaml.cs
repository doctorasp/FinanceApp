using DAL.EF;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FinanceOrganazer
{
    /// <summary>
    /// Interaction logic for AddIncome.xaml
    /// </summary>
    public partial class AddIncome : Window
    {
        DatabaseContext _context;
        MainWindow main;
        public AddIncome(MainWindow m)
        {
            main = m;
            _context = main._context;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.incomeTxt.Text != "" && this.priceTxt.Text != "")
            {
                Income income = new Income
                {
                    Name = this.incomeTxt.Text,
                    Price = Decimal.Parse(this.priceTxt.Text),
                    Date = Convert.ToDateTime(this.dateTxt.Text).Date
                };
                _context.Income.Add(income);
                _context.SaveChanges();
                main.drawIncomeChart();
                this.Close();
            }
            else
            {
                MessageBox.Show("Всі поля повинні бути заповнені");
            }
        }
    }
}
