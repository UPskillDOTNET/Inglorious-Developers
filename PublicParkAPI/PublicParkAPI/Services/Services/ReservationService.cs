using AutoMapper;
using PublicParkAPI.Contracts;
using PublicParkAPI.DTO;
using PublicParkAPI.Models;
using PublicParkAPI.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicParkAPI.Services.Services
{
        public class ReservationService : IReservationService
        {
            private readonly IReservationRepository _reservationRepository;
            private readonly IMapper _mapper;
            public ReservationService(IReservationRepository reservationRepository, IMapper mapper)
            {
                _reservationRepository = reservationRepository;
                _mapper = mapper;
            }


            public async Task<IEnumerable<ReservationDTO>> GetReservations()
            {
                var reservations = await _reservationRepository.GetReservations();
                var reservationsDTO = _mapper.Map<List<Reservation>, List<ReservationDTO>>(reservations.ToList());
                return reservationsDTO;
            }
    }
    }

