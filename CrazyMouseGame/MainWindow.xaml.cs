using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CrazyMouseGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        DispatcherTimer _timer;
        TimeSpan _time;

        private void startbtn_Click(object sender, RoutedEventArgs e)
        {
            _time = TimeSpan.FromSeconds(300);

            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                tbTime.Text = _time.ToString("c");
                if (_time == TimeSpan.Zero) _timer.Stop();
                _time = _time.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);

            _timer.Start();
        }

        private int Score = 0;

        private void mousebtn_Click(object sender, RoutedEventArgs e)
        {
            Score++;
            score.Text = Score.ToString();
            
        }

        private void resetbtn_Click(object sender, RoutedEventArgs e)
        {
            Score = 0;
            score.Text = "0";
            
        }
    }
}
