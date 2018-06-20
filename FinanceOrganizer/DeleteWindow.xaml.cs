using DAL.EF;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Interaction logic for DeleteWindow.xaml
    /// </summary>
    public partial class DeleteWindow : Window
    {
        MainWindow main = null;
        DatabaseContext _context;
        public DeleteWindow(MainWindow m)
        {
            _context = m._context;
            main = m;
            InitializeComponent();

            _context.Salary.Load();
            _context.Eat.Load();
            _context.Transport.Load();
            _context.Car.Load();

            this.salary.ItemsSource = _context.Salary.Local.ToBindingList();
            this.eat.ItemsSource = _context.Eat.Local.ToBindingList();
            this.transport.ItemsSource = _context.Transport.Local.ToBindingList();
            this.car.ItemsSource = _context.Car.Local.ToBindingList();
        }

        public void DeleteSalary()
        {
            if (salary.SelectedItems.Count > 0)
            {
                for (int i = 0; i < salary.SelectedItems.Count; i++)
                {
                    Salary el = salary.SelectedItems[i] as Salary;
                    if (el != null)
                    {
                        _context.Salary.Remove(el);
                    }
                }
            }
            _context.SaveChanges();
        }

        public void DeleteEat()
        {
            if (eat.SelectedItems.Count > 0)
            {
                for (int i = 0; i < eat.SelectedItems.Count; i++)
                {
                    Eat el = eat.SelectedItems[i] as Eat;
                    if (el != null)
                    {
                        _context.Eat.Remove(el);
                    }
                }
            }
            _context.SaveChanges();
        }

        public void DeleteTransport()
        {
            if (transport.SelectedItems.Count > 0)
            {
                for (int i = 0; i < transport.SelectedItems.Count; i++)
                {
                    Transport el = transport.SelectedItems[i] as Transport;
                    if (el != null)
                    {
                        _context.Transport.Remove(el);
                    }
                }
            }
            _context.SaveChanges();
        }

        public void DeleteCar()
        {
            if (car.SelectedItems.Count > 0)
            {
                for (int i = 0; i < car.SelectedItems.Count; i++)
                {
                    Car el = car.SelectedItems[i] as Car;
                    if (el != null)
                    {
                        _context.Car.Remove(el);
                    }
                }
            }
            _context.SaveChanges();
        }




        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //string tabItem = ((sender as TabControl).SelectedItem as TabItem).Header as string;

            //switch (tabItem)
            //{
            //    case "Зарплата":
            //        break;
            //    case "Їжа":
            //        break;
            //    case "Транспорт":
            //        break;
            //    case "Машина":
            //        break;

            //    default:
            //        return;
            //}
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DeleteSalary();
            main.drawDoughnut();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DeleteEat();
            main.drawDoughnut();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            DeleteTransport();
            main.drawDoughnut();

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            DeleteCar();
            main.drawDoughnut();
        }
    }
}
