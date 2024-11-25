namespace MiniLibraryManagementSystem.Models
{
    public class Book
    {
        // Benzersiz kimlik
        public int Id { get; set; }

        // Kitabın başlığı
        public string Title { get; set; }

        // Yazarın kimlik bilgisi (Foreign Key)
        public int AuthorId { get; set; }

        // Kitap türü
        public string Genre { get; set; }

        // Yayın tarihi
        public DateTime PublishDate { get; set; }

        // ISBN numarası
        public string ISBN { get; set; }

        // Mevcut kopya sayısı
        public int CopiesAvailable { get; set; }

        // Yazar bilgisi için bir Navigation Property
        public Author Author { get; set; } // ilişkili yazarı - bir kitabın ancak bir yazarı olur

    }
}
