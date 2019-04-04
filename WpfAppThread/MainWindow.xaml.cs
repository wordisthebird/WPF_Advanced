using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace WpfAppThread
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

        private void BackWorkButton_Click(object sender, RoutedEventArgs e)
        {
            //instanciate background worker
            BackgroundWorker MyWorker = new BackgroundWorker();

            //show progress
            MyWorker.WorkerReportsProgress = true;

            //start
            MyWorker.DoWork += worker_DoWork;
            MyWorker.ProgressChanged += worker_ProgressChanged;
            MyWorker.RunWorkerCompleted += worker_RunWorkerCompleted;
            MyWorker.RunWorkerAsync();
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //update value on screen
            PB.Value = e.ProgressPercentage;
            //update text block based on e.Userstate
            ProgressTextBlock.Text = (string)e.UserState;
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;

            //loading 1 to 100 
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(500);
                //multiply by 10 to haveprogress bar the same as i.e.g I<10
                worker.ReportProgress((i), String.Format("Loading {0}", i + "%."));
            }
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //display message
            MessageBox.Show("Loading Complete");
            //reset to 0
            PB.Value = 0;
            //set to null
            ProgressTextBlock.Text = "";
        }

        private void Colorbutton_Click(object sender, EventArgs e)
        {
            //generate random numeber
            Random r = new Random();

            //generate random color
            Color randomColor = Color.FromRgb((byte)r.Next(1, 255),
            (byte)r.Next(1, 255), (byte)r.Next(1, 233));
            var converter = new System.Windows.Media.BrushConverter();
            //change the colour
            var brush = (Brush)converter.ConvertFromString(randomColor.ToString());
            Background = brush;
        }
    }
}

