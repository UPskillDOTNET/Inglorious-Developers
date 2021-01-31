﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PrivateParkAPI.DTO;
using PrivateParkAPI.Models;
using PrivateParkAPI.Repositories.IRepository;
using PrivateParkAPI.Services.IServices;


namespace PrivateParkAPI.Services.Services
{
    public class ParkingSpotService : IParkingSpotService
    {
        private readonly IParkingSpotRepository _parkingSpotRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;
        public ParkingSpotService(IParkingSpotRepository parkingSpotRepository,IReservationRepository reservationRepository,IMapper mapper)
        {
            _parkingSpotRepository = parkingSpotRepository;
            _reservationRepository = reservationRepository;
            _mapper = mapper;
        }

       
       
        public async Task<IEnumerable<ParkingSpotDTO>> GetAllnotPrivate()
        {
            var parkingSpots = await _parkingSpotRepository.GetnotPrivateParkingSpots();
            var parkingSpotsDTO = _mapper.Map<List<ParkingSpot>, List<ParkingSpotDTO>>(parkingSpots.ToList());
            return parkingSpotsDTO;
        }

        public async Task<IEnumerable<ParkingSpotDTO>> GetFreeParkingSpots()
        {
            var reservations = await _reservationRepository.GetSpecificReservation(); 
            var parkingSpots = await _parkingSpotRepository.GetnotPrivateParkingSpots();
            var res = from p in parkingSpots where !(from r in reservations where r.parkingSpotID == p.parkingSpotID && (r.startTime <= DateTime.Now && r.endTime >= DateTime.Now) select r.parkingSpotID).Contains(p.parkingSpotID) select p;
            var parkingSpotsDTO = _mapper.Map<List<ParkingSpot>, List<ParkingSpotDTO>>(res.ToList());
            return parkingSpotsDTO;
        }
    }
}
