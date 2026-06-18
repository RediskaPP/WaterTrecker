using WaterTrecker.Models;
using WaterTrecker.Services;

namespace WaterTrecker.Pages;

public partial class ProfilePage : ContentPage
{
    private readonly WaterService _waterService;

    public ProfilePage(WaterService waterService)
    {
        InitializeComponent();
        _waterService = waterService;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        Refresh();
    }

    private void Refresh()
    {
        int total = _waterService.GetTotalToday();
        int records = _waterService.GetToday().Count;

        LblToday.Text = $"{total} мл";
        LblRecords.Text = records.ToString();

        BadgeLayout.Children.Clear();

        if (total >= 500)
            BadgeLayout.Children.Add(CreateBadge("🥉 Первые 500 мл НЕ ПЛОХ"));

        if (total >= 1000)
            BadgeLayout.Children.Add(CreateBadge("🥈 1 литр МЕГА ХОРОШЬ"));

        if (total >= 2500)
            BadgeLayout.Children.Add(CreateBadge("🥇 Норма дня УЛЬРА ЗАЧЕТ"));

        if (total >= 4000)
            BadgeLayout.Children.Add(CreateBadge("🏆 Гидратор МЕГА УВАЖУХА"));
    }

    private Frame CreateBadge(string text)
    {
        return new Frame
        {
            Padding = 10,
            Margin = new Thickness(0, 5),
            Content = new Label
            {
                Text = text
            }
        };
    }
}