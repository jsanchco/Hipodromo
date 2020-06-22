namespace Codere.SBGOnline.API.Models
{
    #region Using

    using Codere.SBGOnline.Domain.Models;

    #endregion

    public class Session
    {
        public UserViewModel user { get; set; }
        public string token { get; set; }
    }
}
