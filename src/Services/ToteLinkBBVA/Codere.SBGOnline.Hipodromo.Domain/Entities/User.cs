namespace Codere.SBGOnline.Hipodromo.Domain.Entities
{
    #region Using

    using Codere.SBGOnline.Hipodromo.Domain.Repositories;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    #endregion

    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public string AccountNumber { get; set; }
        public string Username { get; set; }
        [Required]
        public string Pin { get; set; }
        public bool BlockTransaction { get; set; }

        public ICollection<Transaction> Transactions { get; set; } = new HashSet<Transaction>();
    }
}
