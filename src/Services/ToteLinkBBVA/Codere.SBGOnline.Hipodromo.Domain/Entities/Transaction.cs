namespace Codere.SBGOnline.Hipodromo.Domain.Repositories
{
    #region Using

    using Codere.SBGOnline.Hipodromo.Domain.Entities;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    #endregion

    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public double Amount { get; set; }

        [Required]
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
