using BO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace PL
{
    /// <summary>
    /// Interaction logic for SimulatorWindow.xaml
    /// </summary>
    public partial class SimulatorWindow : Window
    {
        private BackgroundWorker BgW = new BackgroundWorker();
        Stopwatch SW = new Stopwatch();
        private bool IsTimeRun = true;

        #region properties
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


        public DateTime? before
        {
            get { return (DateTime?)GetValue(beforeProperty); }
            set { SetValue(beforeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for before.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty beforeProperty =
            DependencyProperty.Register("before", typeof(DateTime?), typeof(SimulatorWindow), new PropertyMetadata(null));


        public DateTime? after
        {
            get { return (DateTime?)GetValue(afterProperty); }
            set { SetValue(afterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for after.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty afterProperty =
            DependencyProperty.Register("after", typeof(DateTime?), typeof(SimulatorWindow), new PropertyMetadata(null));


        public string timeText
        {
            get { return (string)GetValue(timeTextProperty); }
            set { SetValue(timeTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for timeText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty timeTextProperty =
            DependencyProperty.Register("timeText", typeof(string), typeof(SimulatorWindow), new PropertyMetadata(null));
        #endregion

        public SimulatorWindow()
        {
            InitializeComponent();
            SW.Start();
            BgW.DoWork += BgW_DoWork;
            BgW.ProgressChanged += BgW_ProgressChanged;
            BgW.RunWorkerCompleted += BgW_RunWorkerCompleted;
            BgW.WorkerReportsProgress = true;
            BgW.WorkerSupportsCancellation = true;
            BgW.RunWorkerAsync();
            processed = false;
            timeText = "00:00:00";
            after = DateTime.Now;
            before = DateTime.Now;
            status = BO.OrderStatus.Ordered;
        }

        private void BgW_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            Simulator.Simulator.UnRegisterReport1(DoReport1);
            Simulator.Simulator.UnRegisterReport2(DoReport2);
            Simulator.Simulator.UnRegisterReport3(DoReport3);
            Close();
        }

        public bool processed
        {
            get { return (bool)GetValue(processedProperty); }
            set { SetValue(processedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for processed.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty processedProperty =
            DependencyProperty.Register("processed", typeof(bool), typeof(SimulatorWindow), new PropertyMetadata(false));



        public Color statusColor
        {
            get { return (Color)GetValue(statusColorProperty); }
            set { SetValue(statusColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for statusColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty statusColorProperty =
            DependencyProperty.Register("statusColor", typeof(Color), typeof(Window), new PropertyMetadata(null));



        private void BgW_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            switch(e.ProgressPercentage)
            {
                case 0:
                    string TimeText = SW.Elapsed.ToString();
                    timeText = TimeText.Substring(0, 8);
                    break;
                case 1:
                    orderId = currId;
                    before = currBefoer;
                    after = currAfter;
                    status = currStatus;
                    UpdateStatus(status);
                    //statusColor = Colors.Pink;
                    break;
                case 2:
                    processed = currProcesed;
                    statusColor = Colors.DeepPink;
                    break;
                case 3:
                    MessageBox.Show(e.UserState.ToString());
                    break;

            }

        }



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



        private void UpdateStatus(OrderStatus status)
        {
            if(status == BO.OrderStatus.Ordered)
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

        private void BgW_DoWork(object? sender, DoWorkEventArgs e)
        {
            Simulator.Simulator.RegisterReport1(DoReport1);
            Simulator.Simulator.RegisterReport2(DoReport2);
            Simulator.Simulator.RegisterReport3(DoReport3);
            Simulator.Simulator.Activate();
            while (BgW.CancellationPending == false)
            {
                e.Cancel = true;
                break;
            }
            while (IsTimeRun)
            {
                BgW.ReportProgress(0);
                Thread.Sleep(1000);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void StopTimerBtn_Click(object sender, RoutedEventArgs e)
        {
            SW.Stop();
            IsTimeRun = false;
            Close();
        }

        int currId;
        DateTime? currBefoer;
        DateTime? currAfter;
        BO.OrderStatus currStatus;
        
        private void DoReport1(int Id, DateTime? last, DateTime? now, BO.OrderStatus Status)
        {
            currId = Id;
            currBefoer = last;
            currAfter = now;
            currStatus = Status;
            BgW.ReportProgress(1);
        }

        bool currProcesed;
        private void DoReport2()
        {
            currProcesed = true;
            BgW.ReportProgress(2);
        }

        private void DoReport3(string st)
        {
            BgW.ReportProgress(3, st);
        }
    }
}
