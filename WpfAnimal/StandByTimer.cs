using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace WpfAnimal
{
    class StandByTimer
    {
        public class TimerEventArgs : EventArgs
        {
            public bool IsCompleted { get; set; }
        }
        public static event EventHandler<TimerEventArgs> HasCompleted;
        private static void HasComplete(bool completed)
        {
            if (HasCompleted != null)
            {
                var eventarg = new TimerEventArgs();
                eventarg.IsCompleted = true;
                HasCompleted.Invoke(timer, eventarg);
            }
        }
        static DispatcherTimer timer { get; set; }

        public static double MyProperty { get; set; }

        public static void cek(double sayi)
        {
            sayi = MyProperty;
        }
        static StandByTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += Timer_Tick;
        }
        static int StandByTime { get; set; }
        static int Counter { get; set; }
        private static void Timer_Tick(object sender, EventArgs e)
        {
            StandByTime = Convert.ToInt32(Settings.timerCountdown);
            Console.WriteLine($"CounterValue: {Counter}");
            if (++Counter < StandByTime)
            {
                return;
            }
            else
            {
                HasComplete(true);
                timer.Stop();
            }
        }
        public static void Start()
        {
            Counter = 0;
            timer.Start();
        }
        public static void StopForce()
        {
            timer.Stop();
        }
        public static bool Available() => timer.IsEnabled;
    }
}