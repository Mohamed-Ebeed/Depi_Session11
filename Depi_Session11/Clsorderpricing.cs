namespace Depi_Session11
{
    // Part 1) User-defined delegate
    public delegate decimal PriceCalculator(Order order);

    // Part 1) Methods that match the PriceCalculator delegate + a method that receives the delegate
    public static class OrderPricing
    {
        // Price x Quantity
        public static decimal CalculateTotal(Order order)
        {
            return order.Price * order.Quantity;
        }

        // Price x Quantity - discount (here the discount is 10% of the total)
        public static decimal CalculateTotalWithDiscount(Order order)
        {
            decimal total = order.Price * order.Quantity;
            decimal discount = total * 0.10m;
            return total - discount;
        }

        // Uses the supplied delegate to calculate the price (no if/else, no switch)
        public static decimal CalculateOrderPrice(Order order, PriceCalculator calculator)
        {
            return calculator(order);
        }
    }
}
