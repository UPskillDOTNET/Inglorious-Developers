using AutoMapper;
using CentralAPI.Models;

namespace CentralAPI.DTO
{
    public class Maps : Profile
    {
        public Maps()
        {            
            CreateMap<ParkingLot, ParkingLotDTO>().ReverseMap();
            CreateMap<PrivateParkAPI.Models.Reservation, PrivateParkAPI.DTO.ReservationDTO>().ReverseMap();            
        }
    }
}
