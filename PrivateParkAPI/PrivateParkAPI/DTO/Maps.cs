using AutoMapper;
using PrivateParkAPI.Models;

namespace PrivateParkAPI.DTO
{
    public class Maps : Profile
    {
        public Maps()
        {
            CreateMap<ParkingSpot, ParkingSpotDTO>().ReverseMap();
            CreateMap<ParkingLot, ParkingLotDTO>().ReverseMap();
            CreateMap<Reservation, ReservationDTO>().ReverseMap();
        }
    }
}
