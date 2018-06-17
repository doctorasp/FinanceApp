namespace BLL.Interfaces
{
    interface IElectricity
    {
        decimal CalculatePrice(int startValue, int EndValue, decimal tariffPer100, decimal tariffAmong100);
    }
}
