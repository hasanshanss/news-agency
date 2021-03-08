using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewsAgency.DAL.Entities;
using NewsAgency.API.Contracts.V1.Requests;

namespace NewsAgency.API.MappingProfiles
{
    public class DomainToResponseProfile : Profile
    {
        public DomainToResponseProfile()
        {
            CreateMap<CreateNewsRequest, News>();
            //CreateMap<News, CreateNewsRequest>();
        }
    }
}
