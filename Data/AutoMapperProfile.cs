using AutoMapper;
using vyroute.Dto;
using vyroute.Models;

namespace vyroute.Data
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Vehicle, VehicleResponseDTO>();
            CreateMap<Vehicle, VehicleDTO2>();
            CreateMap<Transit, TransitResponseDTO>();
            
        }
    }
}
