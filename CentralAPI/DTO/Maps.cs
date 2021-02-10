using AutoMapper;
using CentralAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.DTO
{
    public class Maps : Profile
    {
        public Maps()
        {            
            CreateMap<ParkingLot, ParkingLotDTO>().ReverseMap();
            CreateMap<PrivateParkAPI.Models.Reservation, PrivateParkAPI.DTO.ReservationDTO>().ReverseMap();
            CreateMap<PublicParkAPI.Models.Reservation, PublicParkAPI.DTO.ReservationDTO>().ReverseMap();
        }
    }
}
