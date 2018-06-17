using BLL.Interfaces;
using System;

namespace BLL.Services
{
    public class GasService : IGas
    {
        public int Consumed { get; set; }

        public decimal CalculatePrice(int startValue, int counterValue, decimal tariff)
        {
            int consumed = counterValue - startValue;
            decimal Price = (tariff*consumed);

            Consumed = consumed;
            return Price;
        }
    }
}
