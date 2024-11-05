using Repository.Data.Entity;
using Services.Models.Request;
using Services.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;


namespace Services.Profiles
{
    public class UserEventProfile : Profile
    {
        public UserEventProfile()
        {


            CreateMap<UserEvent, UserEventResponse>().ReverseMap();
        }
    }
}
