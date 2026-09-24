namespace Depi_Session11 
{
    class Program
    {
        static void Main(string[] args)
        {
            Section01_Books();
            Section02_Orders();
        }

        // =====================================================================
        // Section 01
        // =====================================================================
        static void Section01_Books()
        {
            Console.WriteLine("################ Section 01 ################");

            List<Book> books = new List<Book>
            {
                new Book("111-A", "C# Fundamentals", new[] { "Ali Hassan", "Sara Adel" }, new DateTime(2020, 5, 17), 250.50m),
                new Book("222-B", "Learning ASP.NET Core", new[] { "Mona Samir" }, new DateTime(2022, 11, 3), 320m),
                new Book("333-C", "SQL Server Basics", new[] { "Omar Khaled", "Nour Ahmed" }, new DateTime(2019, 1, 25), 180.75m)
            };

            // Q1) the Book class works (constructor + ToString)
            Console.WriteLine("--- Books (ToString) ---");
            foreach (Book b in books)
                Console.WriteLine(b);

            // Note: LibraryEngine.ProcessBooks has two overloads (BookFunction and Func<Book,string>).
            // A method group / lambda converts to BOTH delegate types, so passing it directly would be
            // ambiguous. That is why we create the delegate explicitly (new ...) or use a typed variable.

            // a) User-defined delegate
            Console.WriteLine("\n--- a) User-defined delegate (GetTitle, GetAuthors) ---");
            LibraryEngine.ProcessBooks(books, new BookFunction(BookFunctions.GetTitle));
            LibraryEngine.ProcessBooks(books, new BookFunction(BookFunctions.GetAuthors));

            // b) Built-in delegate Func<Book, string>
            Console.WriteLine("\n--- b) Built-in delegate Func<Book, string> (GetPrice) ---");
            LibraryEngine.ProcessBooks(books, new Func<Book, string>(BookFunctions.GetPrice));

            // c) Anonymous method (instead of the GetISBN method)
            Console.WriteLine("\n--- c) Anonymous method (GetISBN) ---");
            Func<Book, string> getIsbn = delegate (Book B)
            {
                return B.ISBN;
            };
            LibraryEngine.ProcessBooks(books, getIsbn);

            // d) Lambda expression (instead of the GetPublicationDate method)
            Console.WriteLine("\n--- d) Lambda expression (GetPublicationDate) ---");
            Func<Book, string> getDate = B => B.PublicationDate.ToString("yyyy-MM-dd");
            LibraryEngine.ProcessBooks(books, getDate);

            Console.WriteLine();
        }

        // =====================================================================
        // Section 02
        // =====================================================================
        static void Section02_Orders()
        {
            Console.WriteLine("################ Section 02 ################");

            Part1_UserDefinedDelegate();
            Part2_Func();
            Part3_Predicate();
            Part4_Action();
            Part5_And_6_Events();
            Part7_FinalApplication();
            Bonus_PricingStrategies();
        }

        static Order CreateSampleOrder()
        {
            return new Order { Id = 1, CustomerName = "Ahmed Ali", Price = 100m, Quantity = 3 };
        }

        // ---------------- Part 1 ----------------
        static void Part1_UserDefinedDelegate()
        {
            Console.WriteLine("===== Part 1: User-defined delegate =====");
            Order order = CreateSampleOrder();

            Console.WriteLine($"CalculateTotal             : {OrderPricing.CalculateOrderPrice(order, OrderPricing.CalculateTotal):0.00}");
            Console.WriteLine($"CalculateTotalWithDiscount : {OrderPricing.CalculateOrderPrice(order, OrderPricing.CalculateTotalWithDiscount):0.00}");
            Console.WriteLine();
        }

        // ---------------- Part 2 ----------------
        static void Part2_Func()
        {
            Console.WriteLine("===== Part 2: Func<Order, decimal> =====");
            Order order = CreateSampleOrder();

            Console.WriteLine($"Normal   : {OrderPricingFunc.CalculateOrderPrice(order, x => x.Price * x.Quantity):0.00}");
            Console.WriteLine($"Discount : {OrderPricingFunc.CalculateOrderPrice(order, x => x.Price * x.Quantity * 0.85m):0.00}   (15% off)");
            Console.WriteLine();
        }

        // ---------------- Part 3 ----------------
        static void Part3_Predicate()
        {
            Console.WriteLine("===== Part 3: Predicate<Order> =====");
            Order order = CreateSampleOrder();
            Order badOrder = new Order { Id = 2, CustomerName = "", Price = 0m, Quantity = 0 };

            // 3 validation rules as lambdas
            Predicate<Order> hasQuantity = o => o.Quantity > 0;
            Predicate<Order> hasPrice = o => o.Price > 0;
            Predicate<Order> hasCustomerName = o => !string.IsNullOrWhiteSpace(o.CustomerName);

            bool valid = OrderValidator.ValidateOrder(order, o => o.Quantity > 0);
            Console.WriteLine($"Order {order.Id} -> Quantity > 0: {valid}");

            Console.WriteLine($"Order {order.Id} -> Price > 0: {OrderValidator.ValidateOrder(order, hasPrice)}");
            Console.WriteLine($"Order {order.Id} -> Has customer name: {OrderValidator.ValidateOrder(order, hasCustomerName)}");

            Console.WriteLine($"Order {badOrder.Id} -> Quantity > 0: {OrderValidator.ValidateOrder(badOrder, hasQuantity)}");
            Console.WriteLine($"Order {badOrder.Id} -> Price > 0: {OrderValidator.ValidateOrder(badOrder, hasPrice)}");
            Console.WriteLine($"Order {badOrder.Id} -> Has customer name: {OrderValidator.ValidateOrder(badOrder, hasCustomerName)}");
            Console.WriteLine();
        }

        // ---------------- Part 4 ----------------
        static void Part4_Action()
        {
            Console.WriteLine("===== Part 4: Action<Order> =====");
            Order order = CreateSampleOrder();

            Action<Order> printOrder = o =>
            {
                Console.WriteLine($"Order {o.Id} processed.");
            };

            Action<Order> sendConfirmation = o =>
            {
                Console.WriteLine($"Confirmation message sent to {o.CustomerName} for order {o.Id}.");
            };

            Action<Order> writeAudit = o =>
            {
                Console.WriteLine($"[AUDIT] Order {o.Id} for {o.CustomerName} was processed at {DateTime.Now:HH:mm:ss}.");
            };

            // Same method (ProcessOrder), different behaviors, without modifying ProcessOrder
            OrderProcessor.ProcessOrder(order, printOrder);
            OrderProcessor.ProcessOrder(order, sendConfirmation);
            OrderProcessor.ProcessOrder(order, writeAudit);

            // Multicast delegate: one Action that holds three methods
            Console.WriteLine("\n-- Multicast delegate (all three actions together) --");
            Action<Order> all = printOrder;
            all += sendConfirmation;
            all += writeAudit;
            OrderProcessor.ProcessOrder(order, all);
            Console.WriteLine();
        }

        // ---------------- Handlers used with the event ----------------
        static void PrintOrderMessage(Order order)
        {
            Console.WriteLine($"  [Handler1] Order {order.Id} processed.");
        }

        static void SendNotification(Order order)
        {
            Console.WriteLine($"  [Handler2] Notification sent to {order.CustomerName}.");
        }

        static void WriteAuditMessage(Order order)
        {
            Console.WriteLine($"  [Handler3] [AUDIT] Order {order.Id} was completed.");
        }

        // ---------------- Part 5 + Part 6 ----------------
        static void Part5_And_6_Events()
        {
            Console.WriteLine("===== Part 5: Events =====");
            OrderService orderService = new OrderService(PricingStrategies.Normal);

            // Subscribe multiple handlers
            orderService.OrderProcessed += PrintOrderMessage;
            orderService.OrderProcessed += SendNotification;
            orderService.OrderProcessed += WriteAuditMessage;

            Order order = CreateSampleOrder();
            orderService.ProcessOrder(order);   // all 3 handlers run

            Console.WriteLine("\n===== Part 6: Unsubscribe =====");
            orderService.OrderProcessed -= PrintOrderMessage;   // Handler1 is removed

            Order order2 = new Order { Id = 2, CustomerName = "Sara Adel", Price = 50m, Quantity = 2 };
            orderService.ProcessOrder(order2);   // only Handler2 and Handler3 run
            Console.WriteLine();
        }

        // ---------------- Part 7 ----------------
        static void Part7_FinalApplication()
        {
            Console.WriteLine("===== Part 7: Final application =====");

            Order order = new Order { Id = 100, CustomerName = "Mona Samir", Price = 200m, Quantity = 2 };

            // 1) Validate Order (all rules must pass)
            Predicate<Order>[] rules =
            {
                o => o.Quantity > 0,
                o => o.Price > 0,
                o => !string.IsNullOrWhiteSpace(o.CustomerName)
            };
            bool isValid = rules.All(rule => OrderValidator.ValidateOrder(order, rule));
            Console.WriteLine($"1) Validate order  -> {(isValid ? "valid" : "invalid")}");

            if (!isValid)
            {
                Console.WriteLine("Order is not valid, stopping.");
                return;
            }

            // 2) Calculate Price + 3) Process Order + 4) OrderProcessed event -> handlers
            Console.WriteLine("2) Calculate price + 3) Process order (10% discount strategy)");
            OrderService service = new OrderService(PricingStrategies.TenPercentDiscount);

            service.OrderProcessed += PrintOrderMessage;
            service.OrderProcessed += SendNotification;
            service.OrderProcessed += WriteAuditMessage;

            Console.WriteLine("4) OrderProcessed event -> handlers:");
            service.ProcessOrder(order);
            Console.WriteLine();
        }

        // ---------------- Bonus ----------------
        static void Bonus_PricingStrategies()
        {
            Console.WriteLine("===== Bonus: Pricing strategies at runtime =====");
            Order order = CreateSampleOrder();   // 100 x 3 = 300

            // The caller decides the strategy; OrderService is never modified.
            foreach (KeyValuePair<string, Func<Order, decimal>> strategy in PricingStrategies.All)
            {
                Console.WriteLine($"-- {strategy.Key} --");
                OrderService service = new OrderService(strategy.Value);
                service.ProcessOrder(order);
            }
        }
    }
} 
