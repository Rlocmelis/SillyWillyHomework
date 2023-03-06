namespace SillyWillyHomework.Business.Discounts
{
    public interface IProductDiscountService
    {
        decimal CalculateTotalPrice(decimal basePrice, int amount);
    }
}
