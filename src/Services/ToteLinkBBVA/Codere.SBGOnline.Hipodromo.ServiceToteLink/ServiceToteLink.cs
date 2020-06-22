namespace Codere.SBGOnline.Hipodromo.ServiceToteLink
{
    #region Using

    using Codere.SBGOnline.Domain.Models;
    using Codere.SBGOnline.Hipodromo.Domain.Entities;
    using Codere.SBGOnline.Hipodromo.Domain.Repositories;
    using Codere.SBGOnline.Hipodromo.Domain.Services;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    #endregion

    public class ServiceToteLink : IServiceToteLink
    {
        private readonly IRepositoryUser _repositoryUser;

        public ServiceToteLink(IRepositoryUser repositoryUser)
        {
            _repositoryUser = repositoryUser ??
                throw new ArgumentNullException(nameof(repositoryUser));
        }

        public Task<ResultRequest<User>> AccountLogon(string accountNumber, string pin, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}
