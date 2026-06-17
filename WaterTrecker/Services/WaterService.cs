using System;
using System.Collections.Generic;
using System.Text;
using WaterTrecker.Models;

namespace WaterTrecker.Services
{
    public class WaterService
    {
        private readonly List<WaterRecord> _records = new();

        public void Add(WaterRecord record) => _records.Add(record);

        public void Delete(Guid id) => _records.RemoveAll(r => r.Id == id);

        public List<WaterRecord> GetToday() =>
            _records.Where(r => r.Timestamp.Date == DateTime.Today).ToList();

        public List<WaterRecord> GetByDate(DateTime date) =>
            _records.Where(r => r.Timestamp.Date == date.Date).ToList();

        public int GetTotalToday() => GetToday().Sum(r => r.Amount);
    }
}
