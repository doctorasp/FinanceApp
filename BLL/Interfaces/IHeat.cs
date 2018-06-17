namespace BLL.Interfaces
{
    interface IHeat
    {
        decimal CalculatePrice(int startValue, int EndValue, decimal tariffPer100, decimal tariffAmong100);
    }
}
