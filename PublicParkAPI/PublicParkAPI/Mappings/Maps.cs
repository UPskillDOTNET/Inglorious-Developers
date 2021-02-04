
using AutoMapper;
using PublicParkAPI.Models;
using PublicParkAPI.Data;
using PublicParkAPI.DTO;

namespace PublicParkAPI.Mappings
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
