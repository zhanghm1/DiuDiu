using AutoMapper;
using DiuDiu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiuDiu.Dtos
{
    public class AutoMapperConfigs : Profile
    {
        public AutoMapperConfigs()
        {
            CreateMap<Service, ServiceItemDto>();
            CreateMap<ServiceCheck, ServiceCheckItemDto>();

            CreateMap<ServiceEditDto, Service>();
            CreateMap<ServiceCheckEditDto, ServiceCheck>();

            
        }
    }
}
