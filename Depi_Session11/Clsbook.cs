namespace Depi_Session11
{
    // Section 01 - Q1: body of the Book class (properties, constructor, ToString)
    public class Book
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string[] Authors { get; set; }
        public DateTime PublicationDate { get; set; }
        public decimal Price { get; set; }

        public Book(string _ISBN, string _Title,
            string[] _Authors, DateTime _PublicationDate,
            decimal _Price)
        {
            ISBN = _ISBN;
            Title = _Title;
            Authors = _Authors;
            PublicationDate = _PublicationDate;
            Price = _Price;
        }

        public override string ToString()
        {
            string authors = Authors == null ? "" : string.Join(", ", Authors);
            return $"ISBN: {ISBN} | Title: {Title} | Authors: {authors} | Published: {PublicationDate:yyyy-MM-dd} | Price: {Price:0.00}";
        }
    }
}
