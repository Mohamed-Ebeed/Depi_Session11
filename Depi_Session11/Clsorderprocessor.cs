namespace Depi_Session11
{
    // Part 4) The behavior that runs AFTER processing is passed as an Action<Order>.
    // We can pass any behavior without modifying this method.
    public static class OrderProcessor
    {
        public static void ProcessOrder(Order order, Action<Order> action)
        {
            Console.WriteLine($"Processing order {order.Id}...");
            action(order);
        }
    }
}
