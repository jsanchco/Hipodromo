namespace Codere.SBGOnline.Hipodromo.Domain.Repositories
{
    #region Using

    using Codere.SBGOnline.Domain.Models;
    using Codere.SBGOnline.Hipodromo.Domain.Entities;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    #endregion

    public interface IRepositoryTransaction : IDisposable
    {
        bool TransactionExists(Guid id);
        Transaction GetById(Guid id);
        IQueryable<Transaction> GetAll();
        IQueryable<Transaction> GetAllByUserId(Guid userId);
        Task<Transaction> GetByIdAsync(Guid id);
        Task<ResultTransaction<Transaction>> AddAsync(Transaction newTransaction);
        Task<bool> UpdateAsync(Transaction transaction);
        Task<bool> DeleteAsync(Guid id);
    }
}
