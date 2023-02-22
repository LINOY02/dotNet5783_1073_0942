using BO;

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using static Simulator.Simulator;

namespace PL
{
    /// <summary>
    /// Interaction logic for SimulatorWindow.xaml
    /// </summary>
    public partial class SimulatorWindow : Window
    {
        private BackgroundWorker BgW = new BackgroundWorker();
        Stopwatch SW = new Stopwatch();
        Stopwatch progresBarSW = new Stopwatch();
        private bool IsTimeRun = true;
   
        // property depenties
        #region properties


        public int progresBar
        {
            get { return (int)GetValue(progresBarProperty); }
            set { SetValue(progresBarProperty, value); }
        }

        // Using a DependencyProperty as the backing store for progresBar.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty progresBarProperty =
            DependencyProperty.Register("progresBar", typeof(int), typeof(Window), new PropertyMetadata(0));



        public string progres
        {
            get { return (string)GetValue(progresProperty); }
            set { SetValue(progresProperty, value); }
        }

        // Using a DependencyProperty as the backing store for progres.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty progresProperty =
            DependencyProperty.Register("progres", typeof(string), typeof(Window), new PropertyMetadata(null));



        public string statusBefore
        {
            get { return (string)GetValue(statusBeforeProperty); }
            set { SetValue(statusBeforeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for statusText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty statusBeforeProperty =
            DependencyProperty.Register("statusBefore", typeof(string), typeof(Window), new PropertyMetadata(null));

        public string statusAfter
        {
            get { return (string)GetValue(statusAfterProperty); }
            set { SetValue(statusAfterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for statusText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty statusAfterProperty =
            DependencyProperty.Register("statusAfter", typeof(string), typeof(Window), new PropertyMetadata(null));



        public int orderId
        {
            get { return (int)GetValue(orderIdProperty); }
            set { SetValue(orderIdProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ordrId.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty orderIdProperty =
            DependencyProperty.Register("orderId", typeof(int), typeof(SimulatorWindow), new PropertyMetadata(null));

        

        public BO.OrderStatus status
        {
            get { return (BO.OrderStatus)GetValue(statusProperty); }
            set { SetValue(statusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for status.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty statusProperty =
            DependencyProperty.Register("status", typeof(BO.OrderStatus), typeof(SimulatorWindow), new PropertyMetadata(null));




        public string before
        {
            get { return (string)GetValue(beforeProperty); }
            set { SetValue(beforeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for before.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty beforeProperty =
            DependencyProperty.Register("before", typeof(string), typeof(Window), new PropertyMetadata(null));




        public string after
        {
            get { return (string)GetValue(afterProperty); }
            set { SetValue(afterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for after.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty afterProperty =
            DependencyProperty.Register("after", typeof(string), typeof(SimulatorWindow), new PropertyMetadata(null));


        public string timeText
        {
            get { return (string)GetValue(timeTextProperty); }
            set { SetValue(timeTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for timeText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty timeTextProperty =
            DependencyProperty.Register("timeText", typeof(string), typeof(SimulatorWindow), new PropertyMetadata(null));
        #endregion


        /// <summary>
        /// the constractor add the function to the background worker and all the property to defult value
        /// </summary>
        public SimulatorWindow()
        {
            InitializeComponent();
            SW.Start();
            BgW.DoWork += BgW_DoWork;
            BgW.ProgressChanged += BgW_ProgressChanged;
            BgW.RunWorkerCompleted += BgW_RunWorkerCompleted;
            BgW.WorkerReportsProgress = true;
            BgW.WorkerSupportsCancellation = true;
            BgW.RunWorkerAsync(Delay);
            timeText = "00:00:00";
            after = "";
            before = "";
            status = OrderStatus.Ordered;
        }

        /// <summary>
        /// The DO WORK function of the background worker
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BgW_DoWork(object? sender, DoWorkEventArgs e)
        {
            //add all the reports to the simulator
            RegisterReport1(DoReport1);
            RegisterReport2(DoReport2);
            RegisterReport3(DoReport3);
            // activate the thread
            Simulator.Simulator.Activate();

            while (IsTimeRun)
            {
                // run the watch
                BgW.ReportProgress(0, -1);

                // run the progrresbar
                int length = Delay;
                for (int i = 1; i <= length; i++)
                {
                    // Perform a time consuming operation and report progress.
                    Thread.Sleep(500);
                    BgW.ReportProgress(0, i * 100 / length);
                    Thread.Sleep(500);
                }
                Thread.Sleep(500);

            }
            e.Result = progresBarSW.ElapsedMilliseconds;

            // turn the cancel flag in case the button stop is pressed
            if (BgW.CancellationPending == true)
            {
                e.Cancel = true;
            }

        }

        /// <summary>
        /// the progrres changed function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BgW_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            switch (e.ProgressPercentage)
            {
                case 0:// the watch and the progrresbar
                    string TimeText = SW.Elapsed.ToString();
                    timeText = TimeText.Substring(0, 8);
                    if ((int?)e.UserState != -1)
                    {
                        int progress = int.Parse(e.UserState?.ToString()!);
                        progres = (progress + "%");
                        progresBar = progress;
                    }
                    break;
                case 1:// update the window
                    orderId = currId;
                    before = currBefore.ToString()!;
                    after = currAfter?.ToString()!;
                    status = currStatus;
                    UpdateStatus(status);
                    break;
                case 2:// reset the progrresbar
                    Thread.Sleep(1000);
                    progresBar = 0;
                    IsTimeRun = true;
                    break;
            }

        }

        /// <summary>
        /// run worker complete function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BgW_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            //remove all the reports from the simulator
            UnRegisterReport1(DoReport1);
            UnRegisterReport2(DoReport2);
            UnRegisterReport3(DoReport3);
            if (!e.Cancelled)// if all the orders were update
                MessageBox.Show("The order update process has been successfully completed");
            Close();
        }


        
        /// <summary>
        /// update the label of the status
        /// </summary>
        /// <param name="status"></param>
        private void UpdateStatus(OrderStatus status)
        {
            if(status == OrderStatus.Ordered)
            {
                statusBefore = "Ordered";
                statusAfter = "Shipped";
            }
            else
            {
                statusBefore = "Shipped";
                statusAfter = "Delivered";
            }
        }

        /// <summary>
        /// button that stop the thread
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StopTimerBtn_Click(object sender, RoutedEventArgs e)
        {
            if (BgW.IsBusy)// if the thread is run
            {
                SW.Stop();// stop the watch
                IsTimeRun = false;
                BgW.CancelAsync();//cancel the background worker
                StopActivate();//stop the thread
                Close();
            }
        }

        int currId;
        DateTime? currBefore;
        DateTime? currAfter;
        OrderStatus currStatus;
        static int Delay;
        /// <summary>
        /// func off report 1
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="last"></param>
        /// <param name="now"></param>
        /// <param name="Status"></param>
        /// <param name="delay"></param>
        private void DoReport1(int Id, DateTime? last, DateTime? now, OrderStatus Status, int delay)
        {
            currId = Id;
            currBefore = last;
            currAfter = now;
            currStatus = Status;
            Delay = delay;
            BgW.ReportProgress(1);
        }

        /// <summary>
        /// func off report 2
        /// </summary>
        private void DoReport2()
        {
            BgW.ReportProgress(2);
        }

        /// <summary>
        /// func off report 3
        /// </summary>
        /// <param name="st"></param>
        private void DoReport3(string st)
        {
            SW.Stop();
            IsTimeRun = false;
            StopActivate();
        }
    }
}
