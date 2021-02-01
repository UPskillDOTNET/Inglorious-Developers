using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PrivateParkAPI.DTO;
using PrivateParkAPI.Models;
using PrivateParkAPI.Repositories.IRepository;
using PrivateParkAPI.Services.IServices;


namespace PrivateParkAPI.Services.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IParkingSpotRepository _parkingSpotRepository;
        private readonly IMapper _mapper;
        public ReservationService(IReservationRepository reservationRepository, IParkingSpotRepository parkingSpotRepository, IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _parkingSpotRepository = parkingSpotRepository;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<ReservationDTO>> GetReservations()
        {
            var reservations = await _reservationRepository.GetReservations();
            var reservationsDTO = _mapper.Map<List<Reservation>, List<ReservationDTO>>(reservations.ToList());
            return reservationsDTO;
        }

        public async Task<ReservationDTO> GetReservation(string id) {
            var reservation = await _reservationRepository.GetReservation(id);
            var reservationsDTO = _mapper.Map<Reservation, ReservationDTO>(reservation);
            return reservationsDTO;
        }
        public async Task<ReservationDTO> PostReservation(ReservationDTO reservationDTO) {
            var parkingSpot = await _parkingSpotRepository.GetSpecificParkingSpot(reservationDTO);
            reservationDTO.endTime = reservationDTO.startTime.AddHours(reservationDTO.hours);
            reservationDTO.finalPrice = reservationDTO.hours * parkingSpot.priceHour;
            var reservation = _mapper.Map<ReservationDTO, Reservation>(reservationDTO);
            await _reservationRepository.PostReservation(reservation);


            return reservationDTO;
        }
    }
}


