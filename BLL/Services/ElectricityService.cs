using BLL.Interfaces;
using System;

namespace BLL.Services
{
    public class ElectricityService : IElectricity
    {
        public decimal FirstBlock{ get; set; }
        public decimal SecondBlock { get; set; }
        public int Consumed { get; set; }
        public decimal GeneralPrice { get; set; }

        public decimal CalculatePrice(int startValue, int endValue, decimal tariffPer100, decimal tariffAmong100)
        {
            int consumed = endValue - startValue;
            decimal per100Price = (100 * tariffPer100);
            decimal among100Price = ((consumed - 100) * tariffAmong100) / 100;

            FirstBlock = per100Price;
            SecondBlock = among100Price;
            Consumed = consumed;
            GeneralPrice = per100Price + among100Price;

            return per100Price + among100Price;
        }

        public decimal PriceFristBlock()
        {
            return FirstBlock;
        }
        public decimal PriceSecondBlock()
        {
            return SecondBlock;
        }

        public decimal Price()
        {
            return GeneralPrice;
        }
    }
}
