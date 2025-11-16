namespace BadShopRefatorado
{
    public interface IDiscountStrategy
    {
        double ApplyDiscount(double total, double discountValue);
    }

    public class PercentageDiscount : IDiscountStrategy
    {
        public double ApplyDiscount(double total, double value)
        {
            return total - (total * value);
        }
    }

    public class FixedDiscount : IDiscountStrategy
    {
        public double ApplyDiscount(double total, double value)
        {
            return total - value;
        }
    }

    public class NoDiscount : IDiscountStrategy
    {
        public double ApplyDiscount(double total, double value)
        {
            return total;
        }
    }

    public static class DiscountFactory
    {
        public static IDiscountStrategy GetStrategy(double discount)
        {
            if (discount <= 0)
                return new NoDiscount();
            if (discount <= 1)
                return new PercentageDiscount();
            return new FixedDiscount();
        }
    }
}
