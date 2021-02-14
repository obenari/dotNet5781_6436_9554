using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BLAPI;

namespace UI
{
    /// <summary>
    /// Interaction logic for SimulationWindow.xaml
    /// </summary>
    public partial class SimulationWindow : Window
    {
        IBL bl;
        PO.Station CurrentStation;
        BackgroundWorker Worker;
        TimeSpan Watch = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
        bool IsTimeRun = true;
        public SimulationWindow(IBL myBl, PO.Station station)
        {
            InitializeComponent();
            bl = myBl;
            CurrentStation = station;
            txtName.Text = CurrentStation.Name;
            txtCode.Text = CurrentStation.Code.ToString();
            tbWatch.Text = Watch.ToString();
            Worker = new BackgroundWorker();
            Worker.WorkerReportsProgress = true;
            Worker.WorkerSupportsCancellation = true;
            Worker.DoWork += simulation_DoWork;
            Worker.ProgressChanged += simulation_ProgressChanged;
            Worker.RunWorkerAsync();
        }
        /// <summary>
        /// this method sleep and call to progresssChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simulation_DoWork(object sender, DoWorkEventArgs e)
        {
            while (IsTimeRun == true)
            {
                Worker.ReportProgress(1);
                Thread.Sleep(1000);
            }
        }
        /// <summary>
        /// this method update  the watch and the lines that coming soon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simulation_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
          
            Watch = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            //take only 8 first letters from the watch 
            tbWatch.Text = Watch.ToString().Substring(0,8);
            lvLineTiming.ItemsSource = bl.GetLinesTiming(CurrentStation.Code, Watch);
        }

        /// <summary>
        /// this method closed the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            IsTimeRun = false;//in order to stop the thread
            this.Close();
        }
    }
}
