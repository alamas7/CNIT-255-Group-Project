using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Collections;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Windows.Media.Effects;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using Microsoft.CSharp;

namespace TIMER
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {       
        System.Windows.Threading.DispatcherTimer m_dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        System.Windows.Threading.DispatcherTimer m_dispatcherDate = new System.Windows.Threading.DispatcherTimer();
        Time timer = new Time();
        
        BindingList<Item> m_items = new BindingList<Item>();

        public MainWindow()
        {
            InitializeComponent();
            m_dispatcherDate.Tick += dispatcherDate_Tick;
            m_dispatcherDate.Interval = new TimeSpan(0, 0, 1);
            m_dispatcherDate.Start();
            m_dispatcherTimer.Tick += dispatcherTimer_Tick;
            m_dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            tbDate.Text = System.DateTime.Now.ToString();
            lblHours.Text = "hours";
            lblMins.Text = "mins";
            lblSecs.Text = "secs";
            lblMilliSecs.Text = "1/10";
        }

        private void dispatcherDate_Tick(object sender, EventArgs e)
        {
            tbDate.Text = string.Empty;
            tbDate.Text = System.DateTime.Now.ToString();
            tbDate.UpdateLayout();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {                     
            tbHours.Text = string.Empty;
            tbHours.Text = timer.GetStopwatchHours();
            tbHours.UpdateLayout();
            tbMins.Text = string.Empty;
            tbMins.Text = timer.GetStopwatchMinutes();
            tbMins.UpdateLayout();
            tbSecs.Text = timer.GetStopWatchseconds();
            tbSecs.UpdateLayout();
            tbMilliSecs.Text = string.Empty;
            tbMilliSecs.Text = timer.GetStopWatchMilliSeconds();
            tbMilliSecs.UpdateLayout();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            timer.SetLongestTime(m_items);                      
            System.TimeSpan duration = new TimeSpan(0, 0, Convert.ToInt16(timer.GetLongestTime()));
            string endtime = duration.ToString();
            m_dispatcherTimer.Start();
            timer.StartStopWatch();
            txtEndTime.Text = System.DateTime.Now.ToShortTimeString();
            txtTotalTime.Text = endtime; ;
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            m_dispatcherTimer.Stop();
            timer.PauseStopWatch();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            TimerItem timerForm = new TimerItem(m_items);
            timerForm.Show();
            Imported.ItemsSource = m_items;
            this.UpdateLayout();          
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {           
            if (m_items.Count != 0 && Imported.SelectedIndex != -1)
            {
                m_items.Remove(m_items[Imported.SelectedIndex]);
            }
            Imported.UpdateLayout();
        }
    }

    public class Duration
    {
        double seconds = 0;
        double minutes = 0;
        double hours = 0;
        System.TimeSpan timespan = new TimeSpan(0,0,0);

        public Duration()
        {
            seconds = 0;
            minutes = 0;
            hours = 0;
            timespan = new TimeSpan(0, 0, 0);
        }
        public void SetSeconds(double a_seconds)
        {
            timespan = TimeSpan.FromSeconds(a_seconds);
        }
        public void SetMinutes(double a_minutes)
        {
            timespan = TimeSpan.FromMinutes(a_minutes);
        }
        public void SetHours(double a_hours)
        {
            timespan = TimeSpan.FromHours(a_hours);
        }
        public int GetSeconds()
        {
            return timespan.Seconds;
        }
        public int GetMinutes()
        {
            return timespan.Minutes;
        }
        public int GetHours()
        {
            return timespan.Hours;
        }

        public TimeSpan GetDuration()
        {
            return timespan;
        }
    }

    public class Time: Duration
    {
        Duration duration = new Duration();
        Stopwatch stopwatch = new Stopwatch();
        System.Windows.Threading.DispatcherTimer timeTimer = new System.Windows.Threading.DispatcherTimer();
        System.Windows.Threading.DispatcherTimer timeDate = new System.Windows.Threading.DispatcherTimer();
        System.TimeSpan lastElapsedtime = new TimeSpan();
        System.TimeSpan tsTime = new TimeSpan();
        int start = 0;
        double totalTime = 0;
        double longestTime = 0;
        int countLaps = 1;

        public void SetLongestTime(BindingList<Item> a_items)
        {            
            foreach (Item item in a_items)
            {
                if (Convert.ToDouble(item.Time) >= longestTime)
                {
                    longestTime = Convert.ToDouble(item.Time);
                }
            }
        }
      
        public void SetTotalTime(BindingList<Item> a_items)
        {
            foreach (Item item in a_items)
            {
                totalTime += Convert.ToDouble(item.Time);
            }
        }

        public double GetTotalTime()
        {
            return totalTime;
        }

        public double GetLongestTime()
        {
            return longestTime;
        } 

        public string GetStopWatchTime()
        {
            return stopwatch.Elapsed.Milliseconds.ToString();
        }

        public string GetStopWatchMilliSeconds()
        {
            return stopwatch.Elapsed.Milliseconds.ToString();
        }

        public string GetStopWatchseconds()
        {
            return stopwatch.Elapsed.Seconds.ToString();
        }

        public string GetStopwatchMinutes()
        {
            return stopwatch.Elapsed.Minutes.ToString();
        }

        public string GetStopwatchHours()
        {
            return stopwatch.Elapsed.Hours.ToString();
        }

        public void StartStopWatch()
        {
            stopwatch.Start();
        }

        public void PauseStopWatch()
        {
            stopwatch.Stop();
        }

        public string GetLapCount()
        {
            return countLaps.ToString();
        }

        public string GetLapTime()
        {
            countLaps++;
            string result = string.Empty;

            if (tsTime.ToString() == "00:00:00")
            {
                tsTime = stopwatch.Elapsed;
                lastElapsedtime = stopwatch.Elapsed;
                result = (lastElapsedtime.Hours + ":" + lastElapsedtime.Minutes + ":" + lastElapsedtime.Seconds + "." + lastElapsedtime.Milliseconds).ToString();
                return result;
            }
            if (tsTime.ToString() != "00:00:00")
            {
                lastElapsedtime = stopwatch.Elapsed - tsTime;
                tsTime = stopwatch.Elapsed;
                result = (lastElapsedtime.Hours + ":" + lastElapsedtime.Minutes + ":" + lastElapsedtime.Seconds + "." + lastElapsedtime.Milliseconds).ToString();
            }
            return result;
        }
    } 
}
