using Microsoft.AspNetCore.Mvc.Rendering;

namespace MiniLibraryManagementSystem.Models
{
    public class BookViewModel
    {
        // Kitabın benzersiz kimlik numarası
        public int BookID { get; set; }

        // Kitabın başlığı
        public string Title { get; set; }

        public int AuthorId { get; set; } // Yazarın ID'si (Dropdown için)

        // Kitap türü
        public string Genre { get; set; }

        // ISBN numarası
        public string ISBN { get; set; }

        // Yayın tarihi
        public DateTime PublishDate { get; set; }

        // Mevcut kopya sayısı
        public int CopiesAvailable { get; set; }

        // Yazarın adı ve soyadı
        public string AuthorFullName { get; set; }

        // Yazarları açılır menüde listelemek için bir SelectList
        public IEnumerable<SelectListItem> Authors { get; set; }
    }
}
