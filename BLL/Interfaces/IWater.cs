namespace BLL.Interfaces
{
    interface IWater
    {
        decimal CalculatePrice(int startValue, int CounterValue, decimal tariff);
    }
}
