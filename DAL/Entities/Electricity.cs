using System;

namespace DAL.Entities
{
    public class Electricity
    {
        public int Id { get; set; }
        public int StartValue { get; set; }
        public int EndValue { get; set; }
        public DateTime Date { get; set; }
        public int Consumed { get; set; }
        public decimal PricePer100 { get; set; }
        public decimal PriceAmong100 { get; set; }
        public decimal Price { get; set; }
    }
}
