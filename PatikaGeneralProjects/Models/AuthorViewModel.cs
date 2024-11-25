namespace MiniLibraryManagementSystem.Models
{
    public class AuthorViewModel
    {

        // İlgili yazarın benzersiz kimlik numarası
        public int AuthorID { get; set; }

        // Yazarın tam adı (Ad + Soyad birleştirilmiş)
        public string FullName { get; set; }

        // Yazarın doğum tarihi
        public string DateOfBirth { get; set; }

        // Yazarın kitaplarının sayısı
        public int BookCount { get; set; }
    }
}
