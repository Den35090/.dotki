namespace BankSystemDapper.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public int Year { get; set; }
        public decimal Price { get; set; }
        public string Genre { get; set; } = string.Empty;

        public override string ToString() => 
            $"{Id} | {Title} - {Author} ({Year}) | {Price} | {Genre}";
    }
}