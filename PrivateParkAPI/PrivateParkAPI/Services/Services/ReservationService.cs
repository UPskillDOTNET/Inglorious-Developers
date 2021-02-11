using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using PrivateParkAPI.DTO;
using PrivateParkAPI.Models;
using PrivateParkAPI.Repositories.IRepository;
using PrivateParkAPI.Services.IServices;
using PrivateParkAPI.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetReservationsNotCancelled()
        {
            var reservations = await _reservationRepository.GetReservationsNotCancelled();
            var reservationsDTO = _mapper.Map<List<Reservation>, List<ReservationDTO>>(reservations.ToList());
            return reservationsDTO;
        }

        public async Task<ActionResult<ReservationDTO>> GetReservation(string id)
        {
            var reservation = await _reservationRepository.GetReservation(id);
            var reservationsDTO = _mapper.Map<Reservation, ReservationDTO>(reservation);
            return reservationsDTO;
        }

        public async Task<ActionResult<ReservationDTO>> PostReservation(ReservationDTO reservationDTO)
        {
            var reservation = _mapper.Map<ReservationDTO, Reservation>(reservationDTO);
            var reservationReturn = await _reservationRepository.PostReservation(reservation);
            var reservationDTOReturn = _mapper.Map<Reservation, ReservationDTO>(reservationReturn);
            return reservationDTOReturn;
        }
        public async Task<ActionResult<ReservationDTO>> PatchReservation(string id)
        {
            var reservation = await _reservationRepository.Find(id);
            reservation.isCancelled = true;
            var reservations = await _reservationRepository.PatchReservation(reservation);
            var reservationDTO = _mapper.Map<Reservation, ReservationDTO>(reservations);
            return reservationDTO;
        }
        public async Task<bool> FindReservationAny(string id)
        {
            return await _reservationRepository.FindReservationAny(id);
        }
        public ValidationResult Validate(ReservationDTO reservationDTO)
        {
            ReservationValidator validationRules = new ReservationValidator();
            ValidationResult Results = validationRules.Validate(reservationDTO);
            return Results;
        }
    }
}


