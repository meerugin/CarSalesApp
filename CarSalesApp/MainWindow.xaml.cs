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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CarSalesApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CarsButton_Click(object sender, RoutedEventArgs e)
        {
            Cars carsWindow = new Cars();
            carsWindow.ShowDialog();
        }

        private void CustomersButton_Click(object sender, RoutedEventArgs e)
        {
            Customers carsWindow = new Customers();
            carsWindow.ShowDialog();
        }

        private void SoldButton_Click(object sender, RoutedEventArgs e)
        {
            Prodani carsWindow = new Prodani();
            carsWindow.ShowDialog();
        }

        private void FreeCarsButton_Click(object sender, RoutedEventArgs e)
        {
            Free carsWindow = new Free();
            carsWindow.ShowDialog();
        }

        private void UsersButton_Click(object sender, RoutedEventArgs e)
        {
            Sellers carsWindow = new Sellers();
            carsWindow.ShowDialog();
        }

        private void TableButton_Click(object sender, RoutedEventArgs e)
        {
            Tablica carsWindow = new Tablica();
            carsWindow.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Login carsWindow = new Login();
            carsWindow.ShowDialog();
        }
    }
    }
