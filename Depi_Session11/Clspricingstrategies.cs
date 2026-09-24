namespace Depi_Session11
{
    // Bonus) Different pricing strategies, all of type Func<Order, decimal>.
    // New strategies can be added here without touching OrderService.
    public static class PricingStrategies
    {
        public static readonly Func<Order, decimal> Normal =
            order => order.Price * order.Quantity;

        public static readonly Func<Order, decimal> TenPercentDiscount =
            order => order.Price * order.Quantity * 0.90m;

        public static readonly Func<Order, decimal> TwentyPercentDiscount =
            order => order.Price * order.Quantity * 0.80m;

        // VIP customers get 30% discount
        public static readonly Func<Order, decimal> Vip =
            order => order.Price * order.Quantity * 0.70m;

        // Name -> strategy (so the strategy can be chosen at runtime)
        public static readonly Dictionary<string, Func<Order, decimal>> All =
            new Dictionary<string, Func<Order, decimal>>
            {
                { "Normal price", Normal },
                { "10% discount", TenPercentDiscount },
                { "20% discount", TwentyPercentDiscount },
                { "VIP discount", Vip }
            };
    }
}
