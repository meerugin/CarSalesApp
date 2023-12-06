using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace CarSalesApp
{
    /// <summary>
    /// Логика взаимодействия для Loading.xaml
    /// </summary>
    public partial class Loading : Window
    {
        private DispatcherTimer timer;
        private int timerCounter = 0;
        public Loading()
        {
            InitializeComponent();
            timer = new DispatcherTimer();

            timer.Interval = TimeSpan.FromSeconds(1);

            timer.Tick += Timer_Tick;

            timer.Start();
        }
        int startpoint = 0;
        private void Timer_Tick(object sender, EventArgs e)
        {
            startpoint += 15;
            Myprogress.Value = startpoint;
            Percentage.Content = "" + startpoint;
            if(Myprogress.Value == 100)
            {
                Myprogress.Value = 0;
                timer.Stop();
                Login log = new Login();
                log.Show();
                this.Hide();
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }
    }
}
