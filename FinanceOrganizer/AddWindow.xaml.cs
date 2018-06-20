using DAL.EF;
using DAL.Entities;
using System;
using System.Data.Entity;
using System.Windows;

namespace FinanceOrganazer
{
    
    public partial class AddWindow : Window
    {
        DatabaseContext _context;
        MainWindow main = null;
        public AddWindow(MainWindow m)
        {
            main = m;
            InitializeComponent();

            _context = m._context;
            _context.Category.Load();
            this.categories.ItemsSource = _context.Category.Local.ToBindingList();

            DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.categories.Text != "" && this.Price.Text != "" && this.Date.Text != "")
            {
                switch (this.categories.Text)
                {
                    case "Їжа":
                        Eat eat = new Eat {
                            Price = Convert.ToDecimal(this.Price.Text),
                            Date = Convert.ToDateTime(this.Date.Text),
                            CategoryId = Convert.ToInt32(this.categories.SelectedValue)
                        };

                        _context.Eat.Add(eat);
                        _context.SaveChanges();
                        MessageBox.Show("Витрата успішно додана");


                        break;
                    case "Транспорт":
                        Transport tr = new Transport
                        {
                            Price = Convert.ToDecimal(this.Price.Text),
                            Date = Convert.ToDateTime(this.Date.Text),
                            CategoryId = Convert.ToInt32(this.categories.SelectedValue)
                        };

                        _context.Transport.Add(tr);
                        _context.SaveChanges();
                        MessageBox.Show("Витрата успішно додана");
                        break;
                    case "Машина":
                        Car car = new Car
                        {
                            Price = Convert.ToDecimal(this.Price.Text),
                            Date = Convert.ToDateTime(this.Date.Text),
                            CategoryId = Convert.ToInt32(this.categories.SelectedValue)
                        };

                        _context.Car.Add(car);
                        _context.SaveChanges();
                        MessageBox.Show("Витрата успішно додана");
                        break;
                    case "Зарплата":
                        Salary salary = new Salary
                        {
                            Price = Convert.ToDecimal(this.Price.Text),
                            Date = Convert.ToDateTime(this.Date.Text),
                            CategoryId = Convert.ToInt32(this.categories.SelectedValue)
                        };

                        _context.Salary.Add(salary);
                        _context.SaveChanges();
                        MessageBox.Show("Витрата успішно додана");
                        break;
                    default:
                        break;
                }
                main.drawDoughnut();

            }
            else
            {
                MessageBox.Show("Всі поля повинні бути заповнені");
            }
        }
    }
}
