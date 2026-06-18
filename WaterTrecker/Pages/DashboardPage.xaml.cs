using System.Collections.ObjectModel;
using WaterTrecker.Models;
using WaterTrecker.Services;

namespace WaterTrecker.Pages;

public partial class DashboardPage : ContentPage
{
    private readonly WaterService _waterService;
    private readonly ObservableCollection<WaterRecordView> _items = new();

    private int DailyGoal => Preferences.Get("daily_goal", 2000);

    public DashboardPage(WaterService waterService)
    {
        InitializeComponent();
        _waterService = waterService;
        WaterLogList.ItemsSource = _items;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LblDate.Text = DateTime.Now.ToString("dddd, d MMMM",
            new System.Globalization.CultureInfo("ru-RU"));
        Refresh();
    }

    private void OnAdd150(object sender, EventArgs e) => AddRecord(150, "Вода", "💧");
    private void OnAdd250(object sender, EventArgs e) => AddRecord(250, "Вода", "💧");
    private void OnAdd500(object sender, EventArgs e) => AddRecord(500, "Вода", "💧");

    private void OnDelete(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.CommandParameter is Guid id)
        {
            _waterService.Delete(id);
            Refresh();
        }
    }

    private void AddRecord(int ml, string type, string icon)
    {
        _waterService.Add(new WaterRecord
        {
            Id = Guid.NewGuid(),
            Amount = ml,
            DrinkType = type,
            Icon = icon,
            Timestamp = DateTime.Now
        });
        Refresh();
    }

    private void Refresh()
    {
        var records = _waterService.GetToday();
        int total = records.Sum(r => r.Amount);
        int remaining = Math.Max(DailyGoal - total, 0);
        double progress = Math.Min((double)total / DailyGoal, 1.0);

        // Прогресс
        LblCurrent.Text = $"{total} мл";
        LblGoal.Text = $"из {DailyGoal} мл";
        ProgressBar.Progress = progress;

        // Мотивация
        LblMotivation.Text = progress switch
        {
            >= 1.0 => "🎉 ТВОЯ МАМА КОРОЛЕВА!",
            >= 0.75 => $"🔥 УЛЬТРА ХОРОШ! Осталось {remaining} мл",
            >= 0.5 => $"💪 НЕ ПЛОХ! Осталось {remaining} мл",
            >= 0.25 => $"👍 ГРУСТНЕНЬКО! Осталось {remaining} мл",
            _ => $"💧 Пора пить воду! Осталось {remaining} мл"
        };

        // Список
        _items.Clear();
        foreach (var r in records.OrderByDescending(r => r.Timestamp))
        {
            _items.Add(new WaterRecordView
            {
                Id = r.Id,
                Amount = r.Amount,
                DrinkType = r.DrinkType,
                Icon = r.Icon,
                TimeDisplay = r.Timestamp.ToString("HH:mm")
            });
        }

        LblEmpty.IsVisible = _items.Count == 0;
    }
}

public class WaterRecordView
{
    public Guid Id { get; set; }
    public int Amount { get; set; }
    public string DrinkType { get; set; } = "";
    public string Icon { get; set; } = "";
    public string TimeDisplay { get; set; } = "";
}
