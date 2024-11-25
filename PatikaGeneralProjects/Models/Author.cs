namespace MiniLibraryManagementSystem.Models
{
    public class Author
    {
        // Benzersiz kimlik
        public int Id { get; set; }

        // Yazarın adı
        public string FirstName { get; set; }

        // Yazarın soyadı
        public string LastName { get; set; }

        // Doğum tarihi
        public DateTime DateOfBirth { get; set; }

        //// Bir yazarın yazdığı kitaplar
        //public List<Book> Books { get; set; }

        // Bir yazarın yazdığı kitaplar
        public ICollection<Book>? Books { get; set; } // Nullable olabilir
    }
}
