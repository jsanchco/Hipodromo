namespace Codere.SBGOnline.Domain.Models
{
    #region Using

    using System;

    #endregion

    public class UserViewModel
    {
        public Guid id { get; set; }
        public string accountNumber { get; set; }
        public string username { get; set; }
        public string pin { get; set; }
        public bool blockTransaction { get; set; }
    }
}
