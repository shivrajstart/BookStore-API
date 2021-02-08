using BookStore_API.Contracts;
using BookStore_API.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore_API.Services
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext dbContext;
        public BookRepository(ApplicationDbContext applicationDbContext)
        {
            dbContext = applicationDbContext;
        }
        public async Task<bool> Create(Book entity)
        {
            await dbContext.Books.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Book entity)
        {
            dbContext.Books.Remove(entity);
            return await Save();
        }

        public async Task<IList<Book>> FindAll()
        {
            var books = await dbContext.Books.ToListAsync();
            return books;
        }

        public async Task<Book> FindById(int id)
        {
            var books = await dbContext.Books.FindAsync(id);
            return books;
        }

        public async Task<bool> isExists(int id)
        {
            var isExists = await dbContext.Books.AnyAsync(b => b.Id == id);
            return isExists;
        }

        public async Task<bool> Save()
        {
            var changes = await dbContext.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Book entity)
        {
            dbContext.Books.Update(entity);
            return await Save();
        }
    }
}
