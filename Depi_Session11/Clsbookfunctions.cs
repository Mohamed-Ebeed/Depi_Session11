namespace Depi_Session11
{
    // Section 01 - Q1: body of all BookFunctions methods.
    // All of them have the same signature: string Method(Book B)
    public class BookFunctions
    {
        public static string GetTitle(Book B)
        {
            return B.Title;
        }

        public static string GetAuthors(Book B)
        {
            return string.Join(", ", B.Authors);
        }

        public static string GetPrice(Book B)
        {
            return B.Price.ToString("0.00");
        }

        public static string GetISBN(Book B)
        {
            return B.ISBN;
        }

        public static string GetPublicationDate(Book B)
        {
            return B.PublicationDate.ToString("yyyy-MM-dd");
        }
    }
}
