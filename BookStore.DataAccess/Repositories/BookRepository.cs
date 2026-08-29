using BookStore.Core.Abstractions.Repositories;
using BookStore.Core.Models;
using BookStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataAccess.Repository
{
    public class BooksRepository(BookStoreDbContext context) : IBooksRepository
    {
        private readonly BookStoreDbContext _context = context;

        public async Task<List<Book>> Get()
        {
            var bookEntities = await _context.Books.AsNoTracking().ToListAsync();

            var books = bookEntities
                .Select(b => Book.Create(b.Id, b.Title, b.Description, b.Price).Book)
                .ToList();

            return books;
        }

        public async Task<Guid> Create(Book book)
        {
            var bookEntity = new BookEntity
            {
                Id = book.Id,
                Description = book.Description,
                Price = book.Price,
                Title = book.Title,
            };

            await _context.Books.AddAsync(bookEntity);
            await _context.SaveChangesAsync();

            return bookEntity.Id;
        }

        public async Task<Guid> Update(Guid id, string title, string description, decimal price)
        {
            await _context
                .Books.Where(b => b.Id == id)
                .ExecuteUpdateAsync(s =>
                    s.SetProperty(b => b.Title, b => title)
                        .SetProperty(b => b.Description, b => description)
                        .SetProperty(b => b.Price, b => price)
                );

            return id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _context.Books.Where(b => b.Id == id).ExecuteDeleteAsync();

            return id;
        }
    }
}
