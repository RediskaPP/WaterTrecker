using System.Collections.ObjectModel;
using System.ComponentModel;
using WaterTrecker.Models;

namespace WaterTrecker.Pages;

public partial class NotificationsPage : ContentPage
{
    public NotificationsPage()
    {
        InitializeComponent();

        BindingContext =
            new NotificationsViewModel();
    }

    private NotificationsViewModel ViewModel =>
    (NotificationsViewModel)BindingContext;

    private void MinusClicked(object sender, EventArgs e)
    {
        if (ViewModel.Settings.IntervalMinutes > 15)
            ViewModel.Settings.IntervalMinutes -= 15;

        ViewModel.Refresh();
    }

    private void PlusClicked(object sender, EventArgs e)
    {
        if (ViewModel.Settings.IntervalMinutes < 240)
            ViewModel.Settings.IntervalMinutes += 15;

        ViewModel.Refresh();
    }
}