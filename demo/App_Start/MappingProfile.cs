using AutoMapper;
using demo.Models;
using demo.Models.Data_Transfer_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace demo.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, UserDto>();
            CreateMap<UserDto, ApplicationUser>();
        }
    }
}