using Microsoft.EntityFrameworkCore;
using MiniLibraryManagementSystem.Models;

namespace MiniLibraryManagementSystem.Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }

        // Kitaplar için DbSet
        public DbSet<Book> Books { get; set; }

        // Yazarlar için DbSet
        public DbSet<Author> Authors { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

             modelBuilder.Entity<Book>()
            .Property(b => b.PublishDate)
            .HasConversion(
                v => v.ToUniversalTime(), // DateTime'ı UTC'ye dönüştürerek kaydet
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc) // Veritabanından okurken UTC olarak belirle
            );

            // Book ve Author arasında bire çok ilişki tanımlama
            // Bir yazarın birden çok kitabı olabilir FAKAT bir kıtabın birden çok yazarı olamaz sadece bir kitabı olabilir  one- multiple 
            modelBuilder.Entity<Book>()
                        .HasOne(b => b.Author)
                        .WithMany(a => a.Books)
                        .HasForeignKey(b => b.AuthorId)
                        .OnDelete(DeleteBehavior.Cascade); //  DeleteBehavior.Cascade kullanılarak, bir yazarın silinmesi durumunda ona bağlı kitapların da otomatik olarak silinmesi sağlanır.

        }




    }
}
