using WaterTrecker.Services;

namespace WaterTrecker.Pages;

public partial class SettingsPage : ContentPage
{
    private readonly WaterService _waterService;

    public SettingsPage(WaterService waterService)
    {
        InitializeComponent();
        _waterService = waterService;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        EntryGoal.Text =
            Preferences.Get("daily_goal", 2000).ToString();

        SwitchDarkMode.IsToggled =
            Preferences.Get("dark_mode", false);
    }

    private void OnThemeChanged(object sender, ToggledEventArgs e)
    {
        Preferences.Set("dark_mode", e.Value);

        var theme = e.Value ? AppTheme.Dark : AppTheme.Light;

        Application.Current!.UserAppTheme = theme;
    }

    private async void OnSave(object sender, EventArgs e)
    {
        if (int.TryParse(EntryGoal.Text, out int goal))
        {
            Preferences.Set("daily_goal", goal);
        }

        Preferences.Set("dark_mode", SwitchDarkMode.IsToggled);

        await DisplayAlert(
            "Готово",
            "Настройки сохранены",
            "OK");
    }

    private async void OnClearData(object sender, EventArgs e)
    {
        bool result = await DisplayAlert(
            "Подтверждение",
            "Удалить все записи?",
            "Да",
            "Нет");

        if (!result)
            return;

        var records = Enumerable.Range(0, 365)
            .SelectMany(i =>
                _waterService.GetByDate(
                    DateTime.Today.AddDays(-i)))
            .ToList();

        foreach (var record in records)
        {
            _waterService.Delete(record.Id);
        }

        await DisplayAlert(
            "Готово",
            "Все записи удалены",
            "OK");
    }
}