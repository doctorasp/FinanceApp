using System;

namespace DAL.Entities
{
    public class Gas
    {
        public int Id { get; set; }
        public int CounterNumber { get; set; }
        public int PrevValue { get; set; }
        public int CurrentValue { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
    }
}
