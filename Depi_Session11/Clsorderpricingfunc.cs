namespace Depi_Session11
{
    
    public static class OrderPricingFunc
    {
        public static decimal CalculateOrderPrice(Order order, Func<Order, decimal> calculator)
        {
            return calculator(order);
        }
    }
}
