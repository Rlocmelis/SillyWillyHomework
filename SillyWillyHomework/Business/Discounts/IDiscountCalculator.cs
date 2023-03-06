namespace SillyWillyHomework.Business.Discounts
{
    public interface IDiscountCalculator
    {
        decimal CalculateDiscount(decimal basePrice, int amount);
    }
}
