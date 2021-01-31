using AutoMapper;
using PrivateParkAPI.Models;
using PrivateParkAPI.DTO;
using PrivateParkAPI.Data;

namespace PrivateParkAPI.Mappings {
    public class Maps : Profile {
        //Used to track Model info to DTO info
        public Maps() {

            CreateMap<ParkingSpot, ParkingSpotDTO>().ReverseMap();
            CreateMap<ParkingLot, ParkingLotDTO>().ReverseMap();
            CreateMap<Reservation, ReservationDTO>().ReverseMap();
        }

    }

}