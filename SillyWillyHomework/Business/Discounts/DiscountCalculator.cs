namespace SillyWillyHomework.Business.Discounts
{

    public class DefaultDiscountCalculator : IDiscountCalculator
    {
        public decimal CalculateDiscount(decimal basePrice, int amount)
        {
            decimal discount = 0.0m;
            if (amount >= 10 && amount < 50)
            {
                discount = 0.05m;
            }
            else if (amount >= 50)
            {
                discount = 0.15m;
            }
            return discount;
        }
    }
}
