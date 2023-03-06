namespace SillyWillyHomework.Business.Discounts
{
    public class ProductDiscount
    {
        private readonly IDiscountCalculator discountCalculator;

        public ProductDiscount(IDiscountCalculator discountCalculator)
        {
            this.discountCalculator = discountCalculator;
        }

        public decimal CalculateTotalPrice(decimal basePrice, int amount)
        {
            decimal price = basePrice * amount;
            decimal discount = discountCalculator.CalculateDiscount(basePrice, amount);
            decimal discountAmount = Math.Round(price * discount, 2);
            decimal totalPrice = Math.Round(price - discountAmount, 2);
            return totalPrice;
        }
    }
}
