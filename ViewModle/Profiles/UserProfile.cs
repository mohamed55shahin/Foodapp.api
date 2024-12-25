using AutoMapper;
using FoodApp.Api.ViewModle.authVIewModel;
using FoodApp.Core.Entities;

namespace FoodApp.Api.ViewModle.Profiles
{
    public class UserProfile : Profile
    {

        public UserProfile() {

            CreateMap<RegistrationViewModel, ApplicationUser>();

            CreateMap<ApplicationUser, UserViewModel>().
            ForMember((dst => dst.fullname), opt => opt.
            MapFrom(user =>$"{user.Fname} {user.Lname}"));
         }    

    }
}
