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
using System.IO;
using Microsoft.VisualBasic;
using System.Xml.Serialization;
using System.Xml;

namespace CrazyMouseGame
{
    public partial class MainWindow : Window
    {
        Employee emp = new Employee();
        public static Random _random = new Random();
        private int Score = 0;
        private string PlayerName = "";
        private int PreviousScore = 0;
        public static int xMouseLocation = 20;
        public static int yMouseLocation = 20;
        DispatcherTimer _timer;
        TimeSpan _time;

        public MainWindow()
        {
            InitializeComponent();
            PastScore.Text = ($"The previous score is {PreviousScore} by {PlayerName}.");
            _time = TimeSpan.FromSeconds(90);
            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                Timer.Text = _time.ToString("c");
                if (_time == TimeSpan.Zero) _timer.Stop();
                _time = _time.Add(TimeSpan.FromSeconds(-1));
            }, System.Windows.Application.Current.Dispatcher);
            _timer.Start();
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

        private void mousebtn_Click(object sender, RoutedEventArgs e)
        {
            Score++;
            score.Text = Score.ToString();
            if (mousebtn.Height <= 4)
            {
                xMouseLocation = xMouseLocation + 0;
                yMouseLocation = yMouseLocation + 0;
                mousebtn.Height = mousebtn.Height - 0;
                mousebtn.Width = mousebtn.Width - 0;
                _timer.Stop();
                MessageBox.Show("Congrats You Win!");
                if (Score > PreviousScore)
                {
                    string FilePath = "C:/Users/Wilson_dana/source/repos/CrazyMouseGame/";
                    string FileName = "Scores.xml";
                    TextWriter writer = new StreamWriter(FilePath + FileName);
                    //needs to record score that has been made, along with player's name and both added to a document file//
                    PlayerName = Interaction.InputBox("What is your name?", "Beaten The High Score, Great Job!", "");
                    Score = PreviousScore;
                    XmlSerializer ser = new XmlSerializer(typeof(Employee));
                    ser.Serialize(writer, emp);
                    writer.Close();
                }
                else
                {
                    MessageBox.Show("Better Luck Next Time on getting the High Score.");
                    Environment.Exit(0);
                }
            }
            else
            {
                xMouseLocation = xMouseLocation + 2;
                yMouseLocation = yMouseLocation + 2;
                mousebtn.Height = mousebtn.Height - 4;
                mousebtn.Width = mousebtn.Width - 7;
            }
        }
    }
}