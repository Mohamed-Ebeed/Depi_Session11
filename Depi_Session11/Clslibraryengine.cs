namespace Depi_Session11
{
    // a) User-defined delegate with the SAME signature as the methods in BookFunctions
    public delegate string BookFunction(Book B);

    public class LibraryEngine
    {
        // a) parameter type = user-defined delegate
        public static void ProcessBooks(List<Book> bList, BookFunction fPtr)
        {
            foreach (Book B in bList)
            {
                Console.WriteLine(fPtr(B));
            }
        }

        // b) parameter type = the proper built-in delegate.
        // The method takes a Book and returns a string => Func<Book, string>
        public static void ProcessBooks(List<Book> bList, Func<Book, string> fPtr)
        {
            foreach (Book B in bList)
            {
                Console.WriteLine(fPtr(B));
            }
        }
    }
}
