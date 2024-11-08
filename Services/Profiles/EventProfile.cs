using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Repository.Data.Entity;
using Services.Models.Request;
using Services.Models.Response;
using Services.Services;

namespace Services.Profiles
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            
            CreateMap<Event, EventResponse>().ReverseMap();
            CreateMap<Event, EventRequest>().ReverseMap();

        }
    }

    public class AdoptionProfile : Profile
    {
        public AdoptionProfile()
        {
            CreateMap<Adoption, AdoptionUserResponse>().ReverseMap();
        }
    }

    public class PetProfile : Profile
    {
        public PetProfile()
        {
            CreateMap<Pet, PetAdoptionResponse>().ReverseMap();
        }
    }
}
