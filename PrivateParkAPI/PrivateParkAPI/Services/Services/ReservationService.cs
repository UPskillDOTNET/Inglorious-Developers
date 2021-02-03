using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using PrivateParkAPI.DTO;
using PrivateParkAPI.Models;
using PrivateParkAPI.Repositories.IRepository;
using PrivateParkAPI.Services.IServices;
using PrivateParkAPI.Utils;

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
        
        public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetReservations()
        {
            var reservations = await _reservationRepository.GetReservations();
            var reservationsDTO = _mapper.Map<List<Reservation>, List<ReservationDTO>>(reservations.ToList());
            return reservationsDTO;
        }
        public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetReservationsNotCancelled() {
            var reservations = await _reservationRepository.GetReservationsNotCancelled();
            var reservationsDTO = _mapper.Map<List<Reservation>, List<ReservationDTO>>(reservations.ToList());
            return reservationsDTO;
        }

        public async Task<ActionResult<ReservationDTO>> GetReservation(string id) {
            var reservation = await _reservationRepository.GetReservation(id);
            var reservationsDTO = _mapper.Map<Reservation, ReservationDTO>(reservation);
            return reservationsDTO;
        }

        public async Task<ActionResult<ReservationDTO>> PostReservation(ReservationDTO reservationDTO) {

            await GetEndTimeandFinalPrice(reservationDTO);
            var reservation = _mapper.Map<ReservationDTO, Reservation>(reservationDTO);
            await _reservationRepository.PostReservation(reservation);
            return reservationDTO;
        }

        public async Task<ActionResult<ReservationDTO>> GetEndTimeandFinalPrice(ReservationDTO reservationDTO)
        {
            var parkingSpot = await _parkingSpotRepository.GetSpecificParkingSpot(reservationDTO);
            reservationDTO.endTime = reservationDTO.startTime.AddHours(reservationDTO.hours);
            reservationDTO.finalPrice = reservationDTO.hours * parkingSpot.priceHour;
            return reservationDTO;
        }

        public async Task<ActionResult<Reservation>> PatchReservation(string id)
        {
            
            return await _reservationRepository.PatchReservation(id);
        }
        public ValidationResult Validate(ReservationDTO reservationDTO)
        {
            ReservationValidator validationRules = new ReservationValidator();

            ValidationResult Results = validationRules.Validate(reservationDTO);

            return Results;
        }
    }
}


