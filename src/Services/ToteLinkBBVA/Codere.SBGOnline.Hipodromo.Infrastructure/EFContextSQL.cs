namespace Codere.SBGOnline.Infrastructure
{
    #region Using

    using Codere.SBGOnline.Hipodromo.Domain.Entities;
    using Codere.SBGOnline.Hipodromo.Domain.Repositories;
    using Codere.SBGOnline.Infrastructure.Helpers;
    using Microsoft.EntityFrameworkCore;
    using System.Threading;

    #endregion

    public class EFContextSQL : DbContext
    {
        #region Members

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }

        public static long InstanceCount;

        #endregion

        public EFContextSQL(DbContextOptions options) : base(options) => Interlocked.Increment(ref InstanceCount);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
        }
    }
}
