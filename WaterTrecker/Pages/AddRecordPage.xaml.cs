using WaterTrecker.Models;
using WaterTrecker.Services;
using System;

namespace WaterTrecker.Pages;

public partial class AddRecordPage : ContentPage
{
    private readonly WaterService _waterService;

    private DrinkOption _selectedDrink;

    private bool _isSyncing = false;

    public AddRecordPage(WaterService waterService)
    {
        InitializeComponent();
        _waterService = waterService;

        LoadDrinkTypes();
    }

    private void LoadDrinkTypes()
    {
        var drinks = new List<DrinkOption>
        {
            new DrinkOption { Name = "Вода",   Icon = "💧" },
            new DrinkOption { Name = "Чай",    Icon = "🍵" },
            new DrinkOption { Name = "Кофе",   Icon = "☕" },
            new DrinkOption { Name = "Сок",    Icon = "🧃" },
            new DrinkOption { Name = "Молоко", Icon = "🥛" },
            new DrinkOption { Name = "Другое", Icon = "🫙" },
        };

        DrinkList.ItemsSource = drinks;

        _selectedDrink = drinks[0];
        DrinkList.SelectedItem = drinks[0];
    }

    private void OnSliderChanged(object sender, ValueChangedEventArgs e)
    {
        if (_isSyncing) return;
        _isSyncing = true;

        int val = (int)Math.Round(e.NewValue / 10) * 10; // шаг 10 мл
        LblAmount.Text = $"{val} мл";
        EntryAmount.Text = val.ToString();

        _isSyncing = false;
    }

    private void OnEntryChanged(object sender, TextChangedEventArgs e)
    {
        if (_isSyncing) return;
        if (!int.TryParse(e.NewTextValue, out int val)) return;

        _isSyncing = true;

        val = Math.Clamp(val, 50, 1000);
        SliderAmount.Value = val;
        LblAmount.Text = $"{val} мл";

        _isSyncing = false;
    }

    private void OnDrinkSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is DrinkOption drink)
            _selectedDrink = drink;
    }

    private async void OnSave(object sender, EventArgs e)
    {
        int amount;
        if (!int.TryParse(EntryAmount.Text, out amount) || amount <= 0)
            amount = (int)Math.Round(SliderAmount.Value / 10) * 10;

        if (amount <= 0)
        {
            await DisplayAlert("Ошибка", "Введите корректный объём", "ОК");
            return;
        }

        var record = new WaterRecord
        {
            Id = Guid.NewGuid(),
            Amount = amount,
            DrinkType = _selectedDrink?.Name ?? "Вода",
            Icon = _selectedDrink?.Icon ?? "💧",
            Timestamp = DateTime.Now  // просто берём текущее время
        };

        _waterService.Add(record);
        await Shell.Current.GoToAsync("//dashboard");
    }
}

public class DrinkOption
{
    public string Name { get; set; } = "";
    public string Icon { get; set; } = "";
}