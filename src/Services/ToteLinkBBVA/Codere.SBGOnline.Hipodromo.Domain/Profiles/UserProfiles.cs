namespace Codere.SBGOnline.Domain.Profiles
{
    #region Using

    using AutoMapper;
    using Codere.SBGOnline.Domain.Models;
    using Codere.SBGOnline.Hipodromo.Domain.Entities;

    #endregion

    public class UserProfiles : Profile
    {
        public UserProfiles()
        {
            CreateMap<User, UserViewModel>();
            CreateMap<UserViewModel, User>();
        }
    }
}
