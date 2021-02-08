using BookStore_API.Contracts;
using BookStore_API.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore_API.Services
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ApplicationDbContext dbContext;

        public AuthorRepository(ApplicationDbContext applicationDbContext)
        {
            dbContext = applicationDbContext;
        }
        public async Task<bool> Create(Author entity)
        {
            await dbContext.Authors.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Author entity)
        {
            dbContext.Authors.Remove(entity);
            return await Save();
        }

        public async Task<IList<Author>> FindAll()
        {
            var authors = await dbContext.Authors.ToListAsync();
            return authors;
        }

        public async Task<Author> FindById(int id)
        {
            var author = await dbContext.Authors.FindAsync(id);
            return author;
        }

        public async Task<bool> isExists(int id)
        {
            return await dbContext.Authors.AnyAsync(q => q.Id == id);
        }

        public async Task<bool> Save()
        {
            var changes = await dbContext.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Author entity)
        {
            dbContext.Authors.Update(entity);
            return await Save();
        }
    }
}
