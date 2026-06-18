using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace WaterTrecker.Models
{
    public class NotificationsViewModel : INotifyPropertyChanged
    {
        public NotificationSettings Settings { get; }

        public ObservableCollection<string> UpcomingNotifications { get; }
            = new();

        public string IntervalText =>
            $"Каждые {Settings.IntervalMinutes} мин";

        public NotificationsViewModel()
        {
            Settings = new NotificationSettings();

            GenerateNotifications();
        }

        private void GenerateNotifications()
        {
            UpcomingNotifications.Clear();

            var time = DateTime.Now;

            for (int i = 0; i < 5; i++)
            {
                time = time.AddMinutes(Settings.IntervalMinutes);

                UpcomingNotifications.Add(
                    $"💧 {time:HH:mm}");
            }
        }

        public void Refresh()
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(nameof(IntervalText)));

            GenerateNotifications();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
