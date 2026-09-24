namespace Depi_Session11
{
    // Part 3) Validation: the RULE is passed as a Predicate<Order>
    public static class OrderValidator
    {
        public static bool ValidateOrder(Order order, Predicate<Order> validationRule)
        {
            return validationRule(order);
        }
    }
}
