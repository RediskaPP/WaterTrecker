using System.Collections.ObjectModel;
using WaterTrecker.Services;

namespace WaterTrecker.Models
{
    internal class ProfileViewModel
    {
        private readonly WaterService _waterService;

        public string UserName => "Пользователь";

        public int TotalToday =>
            _waterService.GetTotalToday();

        public int RecordsCount =>
            _waterService.GetToday().Count;

        public ObservableCollection<string> Badges { get; } = new();

        public ProfileViewModel(WaterService waterService)
        {
            _waterService = waterService;

            LoadBadges();
        }

        private void LoadBadges()
        {
            var total = TotalToday;

            if (total >= 500)
                Badges.Add("🥉 Первые 500 мл НЕ ПЛОХ");

            if (total >= 1000)
                Badges.Add("🥈 1 литр МЕГА ХОРОШЬ");

            if (total >= 2000)
                Badges.Add("🥇 Норма дня УЛЬРА ЗАЧЕТ");

            if (total >= 3000)
                Badges.Add("🏆 Гидратор МЕГА УВАЖУХА");
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
}
