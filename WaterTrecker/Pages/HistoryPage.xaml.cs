using WaterTrecker.Services;

namespace WaterTrecker.Pages;

public partial class HistoryPage : ContentPage
{
    private readonly WaterService _waterService;

    public HistoryPage(WaterService waterService)
    {
        InitializeComponent();
        _waterService = waterService;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadData(DateTime.Today);
    }

    private void OnDateSelected(object sender, DateChangedEventArgs e)
    {
        if (e.NewDate.HasValue)
            LoadData(e.NewDate.Value);
    }

    private void LoadData(DateTime date)
    {
        var records = _waterService.GetByDate(date);

        HistoryList.ItemsSource = records;

        int total = records.Sum(x => x.Amount);

        LblTotal.Text = $"Всего: {total} мл";
    }
}