using System;
using System.Collections.Generic;
using System.Text;

namespace WaterTrecker.Models
{
    public class NotificationSettings
    {
        public bool Enabled { get; set; } = true;
        public int IntervalMinutes { get; set; } = 60;
        public TimeSpan QuietStart { get; set; } = new(22, 0, 0);
        public TimeSpan QuietEnd { get; set; } = new(8, 0, 0);
    }
}
