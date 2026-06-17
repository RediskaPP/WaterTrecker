using System;
using System.Collections.Generic;
using System.Text;

namespace WaterTrecker.Models
{
    public class WaterRecord
    {
        public Guid Id { get; set; }
        public int Amount { get; set; }
        public string DrinkType { get; set; } = "Вода";
        public string Icon { get; set; } = "💧";
        public DateTime Timestamp { get; set; }
    }
}
