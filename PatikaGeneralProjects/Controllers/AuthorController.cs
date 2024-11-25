using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniLibraryManagementSystem.Data;
using MiniLibraryManagementSystem.Models;
using System.Collections.Generic;

namespace MiniLibraryManagementSystem.Controllers
{
    public class AuthorController : Controller
    {
        private readonly LibraryDbContext _context;

        public AuthorController(LibraryDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var authors = _context.Authors
               .Select(a => new AuthorViewModel
               {
                   AuthorID = a.Id,
                   FullName = a.FirstName + " " + a.LastName,
                   DateOfBirth = a.DateOfBirth.ToShortDateString(),
                   BookCount = a.Books == null ? 0 : a.Books.Count
               })
               .ToList();


            return View(authors);
        }

        // Belirli bir yazarın detaylarını göster
        public IActionResult Details(int id)
        {
            var author = _context.Authors
                .Where(a => a.Id == id)
                .Select(a => new AuthorViewModel
                {
                    FullName = a.FirstName + " " + a.LastName,
                    DateOfBirth = a.DateOfBirth.ToShortDateString(),
                    BookCount = a.Books == null ? 0 : a.Books.Count
                })
                .FirstOrDefault();

            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // Yeni bir yazar eklemek için form sayfası
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Author author)
        {
            if (ModelState.IsValid)
            {
                author.DateOfBirth = DateTimeUTCHelper(author.DateOfBirth);
                _context.Authors.Add(author);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(author);
        }

        public DateTime DateTimeUTCHelper(DateTime gelenDateTime)
        {
           // UTC'ye dönüştür
            DateTime utcDateTime = TimeZoneInfo.ConvertTimeToUtc(gelenDateTime);

            return utcDateTime;
        }

        // GET: Author/Edit/{id}
        public async Task<IActionResult> Edit(int id)
        {
            var author = await _context.Authors.FindAsync(id); // Yazar bilgilerini getir
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // POST: Author/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Author author)
        {
            if (id != author.Id)
            {
                return BadRequest(); // Gelen ID uyumsuzsa hata dön
            }

            if (ModelState.IsValid)
            {
                try
                {
                    author.DateOfBirth = DateTimeUTCHelper(author.DateOfBirth);
                    _context.Update(author); // Yazar bilgisini güncelle
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Authors.Any(a => a.Id == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(author);
        }


        // GET: Author/Delete/{id}
        public async Task<IActionResult> Delete(int id)
        {
            var author = await _context.Authors
                                       .Include(a => a.Books) // Kitaplarla birlikte getir
                                       .FirstOrDefaultAsync(a => a.Id == id);

            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // POST: Author/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var author = await _context.Authors
                                       .Include(a => a.Books) // Kitaplarla birlikte getir
                                       .FirstOrDefaultAsync(a => a.Id == id);

            if (author != null)
            {
                _context.Authors.Remove(author); // Yazarı ve bağlı kitapları sil
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }


    }
}
