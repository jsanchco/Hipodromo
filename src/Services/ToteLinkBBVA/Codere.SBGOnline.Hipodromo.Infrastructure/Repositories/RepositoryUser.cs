namespace Codere.SBGOnline.Hipodromo.Infrastructure.Repositories
{
    #region Using

    using Codere.SBGOnline.Domain.Models;
    using Codere.SBGOnline.Hipodromo.Domain.Entities;
    using Codere.SBGOnline.Hipodromo.Domain.Repositories;
    using Codere.SBGOnline.Infrastructure;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    #endregion

    public class RepositoryUser : IRepositoryUser
    {
        private readonly EFContextSQL _context;

        public RepositoryUser(EFContextSQL context)
        {
            _context = context;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }

        public async Task<ResultTransaction<User>> AddAsync(User newUser)
        {
            _context.Users.Add(newUser);
            return new ResultTransaction<User> { Item = newUser, Result = await _context.SaveChangesAsync() > 0 };
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var toRemove = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (toRemove == null)
                return false;

            _context.Users.Remove(toRemove);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public IQueryable<User> GetAll()
        {
            return _context.Users;
        }

        public User GetById(Guid id)
        {
            return _context.Users
                .FirstOrDefault(x => x.Id == id);
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public bool? IsBlockTransaction(Guid id)
        {
            return _context.Users
                .FirstOrDefault(x => x.Id == id)?.BlockTransaction;
        }

        public async Task<bool> UpdateAsync(User user)
        {
            _context.Users.Update(user);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public bool UserExists(Guid id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id) != null;
        }

        public async Task<User> GetByAccountNumberAndPinAsync(string accountNumber, string pin)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.AccountNumber == accountNumber && x.Pin == pin);
        }
    }
}
