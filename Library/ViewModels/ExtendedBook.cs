namespace Library.ViewModels
{
    public class ExtendedBook
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public int? UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Available { get; set; }
    }
}
