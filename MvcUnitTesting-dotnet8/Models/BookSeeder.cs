using System.Linq;

namespace MvcUnitTesting_dotnet8.Models
{
    public class BookSeeder
    {
        private readonly BookDbContext _context;

        public BookSeeder(BookDbContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (!_context.Books.Any())
            {
                _context.Books.AddRange(
                    new Book { Genre = "Fiction", Name = "Moby Dick", Price = 12.50m },
                    new Book { Genre = "Fiction", Name = "War and Peace", Price = 17m },
                    new Book { Genre = "Science Fiction", Name = "Escape from the vortex", Price = 12.50m },
                    new Book { Genre = "History", Name = "The Battle of the Somme", Price = 22m }
                );

                _context.SaveChanges();
            }
        }
    }
}