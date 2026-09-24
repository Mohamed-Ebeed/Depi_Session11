namespace Depi_Session11
{
    // Part 5 + Bonus) The service notifies others through an EVENT,
    // and it does NOT know how the price is calculated: the pricing behavior is given from outside.
    public class OrderService
    {
        private readonly Func<Order, decimal> _pricingStrategy;

        // Part 5) event
        public event Action<Order> OrderProcessed;

        // Bonus) the caller decides the pricing strategy
        public OrderService(Func<Order, decimal> pricingStrategy)
        {
            _pricingStrategy = pricingStrategy ?? throw new ArgumentNullException(nameof(pricingStrategy));
        }

        public void ProcessOrder(Order order)
        {
            decimal total = _pricingStrategy(order);
            Console.WriteLine($"Order {order.Id} total price: {total:0.00}");

            // Raise the event: every subscribed handler runs. "?." avoids an error when there are no subscribers.
            OrderProcessed?.Invoke(order);
        }
    }
}
