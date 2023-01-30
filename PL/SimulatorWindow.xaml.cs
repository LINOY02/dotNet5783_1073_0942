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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BO;
using Simulator;

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
            DependencyProperty.Register("timeText", typeof(string), typeof(SimulatorWindow), new PropertyMetadata(0));
        #endregion

        public SimulatorWindow()
        {
            InitializeComponent();
            SW.Start();
            BgW.DoWork += BgW_DoWork;
            BgW.ProgressChanged += BgW_ProgressChanged;
            BgW.RunWorkerCompleted += BgW_RunWorkerCompleted;
            BgW.WorkerReportsProgress = true;
            BgW.RunWorkerAsync();
            processed = false;
            timeText = "00:00:00";
            orderId = 0;
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


        private void BgW_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            switch(e.ProgressPercentage)
            {
                case 0:
                string timeText = SW.Elapsed.ToString();
                timeText = timeText.Substring(0, 8);
                break;

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
                Thread.Sleep(1000);
                BgW.ReportProgress(10);
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

        private void DoReport1(int Id, DateTime? last, DateTime? now, BO.OrderStatus Status)
        {
            orderId = Id;
            before = last;
            after = now;
            status = Status;
        }

        private void DoReport2()
        {
            processed = true;
        }

        private void DoReport3(string st)
        {
            MessageBox.Show(st);
        }
    }
}
