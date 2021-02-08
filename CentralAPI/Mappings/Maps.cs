using AutoMapper;
using CentralAPI.Models;
using CentralAPI.DTO;

namespace CentralAPI.Mappings {
    public class Maps : Profile {
        public Maps() {
            CreateMap<CentralReservation, CentralReservationDTO>().ReverseMap();
        }
    }
}