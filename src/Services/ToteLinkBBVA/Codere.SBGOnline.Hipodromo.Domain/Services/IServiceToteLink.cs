namespace Codere.SBGOnline.Hipodromo.Domain.Services
{
    #region Using

    using Codere.SBGOnline.Domain.Models;
    using Codere.SBGOnline.Hipodromo.Domain.Entities;
    using System.Threading;
    using System.Threading.Tasks;

    #endregion

    public interface IServiceToteLink
    {
        Task<ResultRequest<User>> AccountLogon(string accountNumber, string pin, CancellationToken ct = default);
    }
}
