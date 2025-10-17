﻿using DataAccess.Data;
using DataAccess.Data.Entities;
using DataAccess.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, BaseEntity
    {
        internal ShopDbContext context;
        internal DbSet<T> set;

        public Repository(ShopDbContext context)
        {
            this.context = context;
            this.set = context.Set<T>();
        }

        // IEnumerable vs IQueryble
        public async Task<IReadOnlyList<T>> GetAllAsync(
            int? pageNumber = null,
            int pageSize = 10,
            Expression<Func<T, bool>>? filtering = null,
            params string[]? includes)
        {
            var query = set.AsQueryable();

            if (pageNumber != null)
                query = await query.PaginateAsync(pageNumber.Value, pageSize);

            if (filtering != null)
                query = query.Where(filtering);

            if (includes != null && includes.Length > 0)
                foreach (var prop in includes)
                    query = query.Include(prop);

            return await query.ToListAsync(); // execute
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await set.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await set.AddAsync(entity);
            await context.SaveChangesAsync();
        }
        public async Task UpdateAsync(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            await DeleteAsync(entity);
        }
        public async Task DeleteAsync(T? entity)
        {
            if (entity != null)
            {
                set.Remove(entity);
                await context.SaveChangesAsync(true);
            }
        }
    }
}
