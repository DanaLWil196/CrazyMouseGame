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
using System.Threading;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;

namespace CrazyMouseGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Random _random = new Random();
        public static int xMouseLocation = 20;
        public static int yMouseLocation = 20;

        public MainWindow()
        {
            InitializeComponent();
            Thread crazyMouseThread = new Thread(new ThreadStart(CrazyMouseThread));
            crazyMouseThread.Start();
        }

        public void CrazyMouseThread()
        {
            int moveX = 0;
            int moveY = 0;

            while (true)
            {
                moveX = _random.Next(xMouseLocation) - (xMouseLocation / 2);
                moveY = _random.Next(yMouseLocation) - (yMouseLocation / 2);

                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(System.Windows.Forms.Cursor.Position.X + moveX, System.Windows.Forms.Cursor.Position.Y + moveY);
                Thread.Sleep(50);
            }
        }

        private int Score = 0;

        private void mousebtn_Click(object sender, RoutedEventArgs e)
        {
            Score++;
            score.Text = Score.ToString();

            if (mousebtn.Height <= 3)
            {
                MessageBox.Show("Congrats You Win!");

                //Environment.Exit(0);
            }
            else
            {
                xMouseLocation = xMouseLocation + 2;
                yMouseLocation = yMouseLocation + 2;

                mousebtn.Height = mousebtn.Height - 5;
                mousebtn.Width = mousebtn.Width - 7;
            }
        }
    }
}