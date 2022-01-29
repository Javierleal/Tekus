using Domain.Base;
using Domain.Interfaces;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Data.RepoNew
{
    public class RepositoryUser  : BaseEntity
    {
        private readonly DbSet<User> _dbSet;

        public RepositoryUser(EFContext dbContext)
        {
            _dbSet = dbContext.Set<User>();
        }

        public async Task<User> AddAsync(User entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public Task<bool> DeleteAsync(User entity)
        {
            _dbSet.Remove(entity);
            return Task.FromResult(true);
        }

        public Task<User> GetAsync(Expression<Func<User, bool>> expression)
        {
            return _dbSet.FirstOrDefaultAsync(expression);
        }

        public Task<List<User>> ListAsync(Expression<Func<User, bool>> expression)
        {
            return _dbSet.Where(expression).ToListAsync();
        }

        public Task<User> UpdateAsync(User entity)
        {
            _dbSet.Update(entity);
            return Task.FromResult(entity);
        }
    }
}