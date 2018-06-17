namespace BLL.Interfaces
{
    interface IGas
    {
        decimal CalculatePrice(int startValue, int EndValue, decimal tariff);
    }
}
