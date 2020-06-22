namespace Codere.SBGOnline.Hipodromo.Domain.Repositories
{
    #region Using

    using Codere.SBGOnline.Domain.Models;
    using Codere.SBGOnline.Hipodromo.Domain.Entities;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    #endregion

    public interface IRepositoryUser : IDisposable
    {
        bool UserExists(Guid id);
        bool? IsBlockTransaction(Guid id);
        User GetById(Guid id);
        Task<User> GetByAccountNumberAndPinAsync(string accountNumber, string pin);
        IQueryable<User> GetAll();
        Task<User> GetByIdAsync(Guid id);
        Task<ResultTransaction<User>> AddAsync(User newUser);
        Task<bool> UpdateAsync(User user);
        Task<bool> DeleteAsync(Guid id);
    }
}
