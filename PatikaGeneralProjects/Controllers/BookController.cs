using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiniLibraryManagementSystem.Data;
using MiniLibraryManagementSystem.Models;
using System.Collections.Generic;
using static System.Reflection.Metadata.BlobBuilder;

namespace MiniLibraryManagementSystem.Controllers
{
    public class BookController : Controller
    {
        private readonly LibraryDbContext _context;

        public BookController(LibraryDbContext libraryContext)
        {
            _context = libraryContext;
        }

        // Tüm kitaplar listele
        public IActionResult Index() // List
        {
            var books = _context.Books
                .Select(b => new BookViewModel
                {
                    BookID = b.Id,
                    Title = b.Title,
                    Genre = b.Genre,
                    ISBN = b.ISBN,
                    PublishDate = b.PublishDate,
                    CopiesAvailable = b.CopiesAvailable,
                    AuthorFullName = b.Author.FirstName + " " + b.Author.LastName
                }
                ).ToList();

            return View(books);
        }

        // Belirli bir kitabın detaylarını göster
        public IActionResult Details(int id)
        {
            var book = _context.Books
                .Where(b => b.Id == id)
                .Select(b => new BookViewModel
                {
                    Title = b.Title,
                    Genre = b.Genre,
                    ISBN = b.ISBN,
                    PublishDate = b.PublishDate,
                    CopiesAvailable = b.CopiesAvailable,
                    AuthorFullName = b.Author.FirstName + " " + b.Author.LastName
                })
                .FirstOrDefault();

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }


        // Yeni bir kitap eklemek için form sayfası
        public IActionResult Create()
        {
            // açılır menü için yazarların listesini
            var viewModel = new BookViewModel
            {
                Authors = GetAuthorsForDropdown()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BookViewModel model)
        {
            // Yeni kitap nesnesi oluştur
            var book = new Book
            {
                Title = model.Title,
                AuthorId = model.AuthorId,
                Genre = model.Genre,
                PublishDate = model.PublishDate,
                ISBN = model.ISBN,
                CopiesAvailable = model.CopiesAvailable
            };

            // Veritabanına ekle ve kaydet
            _context.Books.Add(book);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // Kitap Güncelleme (GET)
        public IActionResult Edit(int id)
        {
            //var bookViewModel = _context.Books
            //     .Where(b => b.Id == id)
            //     .Select(b => new BookViewModel
            //     {
            //         Title = b.Title,
            //         Genre = b.Genre,
            //         ISBN = b.ISBN,
            //         Authors = GetAuthorsForDropdown(),
            //         PublishDate = b.PublishDate,
            //         CopiesAvailable = b.CopiesAvailable,
            //         AuthorFullName = b.Author.FirstName + " " + b.Author.LastName
            //     })
            //     .FirstOrDefault();

            // Kitabı önce veritabanından alıyoruz
            var book = _context.Books
                .Include(b => b.Author) // Yazar bilgilerini de yüklüyoruz
                .FirstOrDefault(b => b.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            // Modeli manuel olarak oluşturuyoruz yeni edit için kullanılacak yeni model
            var bookViewModel = new BookViewModel
            {
                BookID = book.Id,
                Title = book.Title,
                Genre = book.Genre,
                ISBN = book.ISBN,
                PublishDate = book.PublishDate,
                CopiesAvailable = book.CopiesAvailable,
                AuthorFullName = book.Author.FirstName + " " + book.Author.LastName,
                Authors = GetAuthorsForDropdown() 
            };
           

            if (bookViewModel == null)
            {
                return NotFound();
            }

            return View(bookViewModel);
        }

        // Kitap Güncelleme (POST)
        [HttpPost]
        public IActionResult Edit(BookViewModel bookViewModel)
        {
           
                // BookViewModel'den Book nesnesi oluşturma
                var book = _context.Books.FirstOrDefault(b => b.Id == bookViewModel.BookID);
                if (book == null)
                {
                    return NotFound();
                }

                // ViewModel'den Book modeline veri aktarımı
                book.Title = bookViewModel.Title;
                book.Genre = bookViewModel.Genre;
                book.ISBN = bookViewModel.ISBN;
                book.PublishDate = bookViewModel.PublishDate;
                book.CopiesAvailable = bookViewModel.CopiesAvailable;
                book.AuthorId = bookViewModel.AuthorId;

                // Güncelleme işlemi
                _context.Books.Update(book);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            
        }

        // Kitap Silme (GET - Onay Sayfası)
        public IActionResult Delete(int Id)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == Id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // Kitap Silme (POST)
        [HttpPost, ActionName("DeleteConfirmed")]
        public IActionResult DeleteConfirmed(int Id)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == Id);

            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        private IEnumerable<SelectListItem> GetAuthorsForDropdown()
        {
            return _context.Authors.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(), // Yazar ID'si
                Text = $"{a.FirstName} {a.LastName}" // Yazarın Adı ve Soyadı
            }).ToList();
        }
    }
}
