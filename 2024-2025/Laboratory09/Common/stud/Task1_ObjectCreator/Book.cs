namespace Task1_ObjectCreator
{
    public class Book
    {
        public string Title { get; set; }
        public Author Author { get; set; }

        public override string? ToString()
        {
            return $"Book '{Title} - written by {Author}";
        }
    }
}
