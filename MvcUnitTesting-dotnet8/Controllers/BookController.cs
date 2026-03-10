using Microsoft.AspNetCore.Mvc;
using MvcUnitTesting_dotnet8.Models;

namespace MvcUnitTesting_dotnet8.Controllers
{
    public class BookController : Controller
    {
        private readonly IRepository<Book> repository;

        public BookController(IRepository<Book> bookRepo)
        {
            repository = bookRepo;
        }

        public IActionResult Index()
        {
            var books = repository.GetAll();
            return View(books);
        }

        public IActionResult Details(int id)
        {
            var book = repository.Get(id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                repository.Add(book);
                return RedirectToAction(nameof(Index));
            }

            return View(book);
        }

        public IActionResult Edit(int id)
        {
            var book = repository.Get(id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Book book)
        {
            if (id != book.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                repository.Update(book);
                return RedirectToAction(nameof(Index));
            }

            return View(book);
        }

        public IActionResult Delete(int id)
        {
            var book = repository.Get(id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var book = repository.Get(id);
            if (book != null)
            {
                repository.Remove(book);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}