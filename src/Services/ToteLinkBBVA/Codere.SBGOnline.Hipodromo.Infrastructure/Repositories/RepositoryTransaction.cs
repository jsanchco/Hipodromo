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

    public class RepositoryTransaction : IRepositoryTransaction
    {
        private readonly EFContextSQL _context;

        public RepositoryTransaction(EFContextSQL context)
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
        public async Task<ResultTransaction<Transaction>> AddAsync(Transaction newTransaction)
        {
            _context.Transactions.Add(newTransaction);
            return new ResultTransaction<Transaction> { Item = newTransaction, Result = await _context.SaveChangesAsync() > 0 };
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var toRemove = await _context.Transactions.FirstOrDefaultAsync(x => x.Id == id);
            if (toRemove == null)
                return false;

            _context.Transactions.Remove(toRemove);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public IQueryable<Transaction> GetAll()
        {
            return _context.Transactions
                .Include(x => x.User);
        }

        public IQueryable<Transaction> GetAllByUserId(Guid userId)
        {
            return _context.Transactions
                .Include(x => x.User)
                .Where(x => x.UserId == userId);
        }

        public Transaction GetById(Guid id)
        {
            return _context.Transactions
                .Include(x => x.User)
                .FirstOrDefault(x => x.Id == id);
        }

        public async Task<Transaction> GetByIdAsync(Guid id)
        {
            return await _context.Transactions
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public bool TransactionExists(Guid id)
        {
            return _context.Transactions.FirstOrDefault(x => x.Id == id) != null;
        }

        public async Task<bool> UpdateAsync(Transaction transaction)
        {
            _context.Transactions.Update(transaction);
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
