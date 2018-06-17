using System;

namespace DAL.Entities
{
    public class Water
    {
        public int Id { get; set; }
        public int CounterValue { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
    }
}
