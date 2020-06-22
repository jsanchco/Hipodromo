namespace Codere.SBGOnline.Infrastructure.Helpers
{
    #region Using

    using Codere.SBGOnline.Hipodromo.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using System;

    #endregion

    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = Guid.NewGuid(),
                    Username = "Hipódromo Test",
                    AccountNumber = "32486865",
                    Pin = "123456"
                }
            );
        }
    }
}
