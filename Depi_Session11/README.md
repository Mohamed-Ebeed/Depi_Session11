# Assignment 11 - Delegates & Events

Console app. Run it with `dotnet run`.

## Answers

**Func<> vs custom delegate (Part 2)**
With a custom delegate I have to declare a new type for each signature. `Func<>` is already built in, so I don't need to declare anything.

**Predicate<Order> vs Func<Order, bool> (Part 3)**
They work the same, but `Predicate` tells you the method is a rule that checks the order (true or false), so the code is easier to read.

**Q1: PriceCalculator vs Func<Order, decimal>**
Same signature. `PriceCalculator` is my own delegate that I must declare. `Func<Order, decimal>` is built in.

**Q2: Action<Order> vs Func<Order, decimal>**
`Action` returns nothing (it just does something like printing). `Func` returns a value.

**Q3: Why does Predicate<T> return bool?**
Because it checks a condition on an object, like "is the quantity greater than 0?". It is used for validation and searching.

**Q4: Delegate vs event**
A delegate holds methods and anyone can call it. An event is built on a delegate but outside code can only subscribe (`+=`) or unsubscribe (`-=`), and only the class that owns it can raise it.

**Q5: Why can't outside code invoke an event?**
Because the compiler only lets outside code add or remove handlers. The real delegate is private, so calling the event from another class gives an error.

**Q6: Many handlers on one event**
All of them run one after another, in the order they were added.

**Q7: `orderService.OrderProcessed += HandleOrderProcessed;`**
- `OrderProcessed` is the event of `orderService`.
- `+=` adds a method to the event (it does not replace the old ones). `+` combines and `=` saves the result.
- `HandleOrderProcessed` is the method that will run when the event is raised.

**Q8: Action<Order> vs event Action<Order>**
If I expose a plain `Action<Order>`, anyone can call it, replace it with `=`, or set it to null. With `event` outside code can only add or remove handlers, and only `OrderService` can raise it.

## Bonus
`OrderService` gets a `Func<Order, decimal>` in its constructor, so it does not know how the price is calculated. The strategies (normal, 10%, 20%, VIP) are in `PricingStrategies`.
