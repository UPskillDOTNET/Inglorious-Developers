using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using PrivateParkAPI.DTO;
using PrivateParkAPI.Models;
using PrivateParkAPI.Repositories.IRepository;
using PrivateParkAPI.Services.IServices;
using PrivateParkAPI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace PrivateParkAPI.Services.Services
{
    public class ParkingSpotService : IParkingSpotService
    {
        private readonly IParkingSpotRepository _parkingSpotRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;
        public ParkingSpotService(IParkingSpotRepository parkingSpotRepository, IReservationRepository reservationRepository, IMapper mapper)
        {
            _parkingSpotRepository = parkingSpotRepository;
            _reservationRepository = reservationRepository;
            _mapper = mapper;
        }



        public async Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetAllnotPrivate()
        {
            var parkingSpots = await _parkingSpotRepository.GetnotPrivateParkingSpots();
            var parkingSpotsDTO = _mapper.Map<List<ParkingSpot>, List<ParkingSpotDTO>>(parkingSpots.ToList());
            return parkingSpotsDTO;
        }
        public async Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetAllParkingSpots()
        {
            var parkingSpots = await _parkingSpotRepository.GetAllParkingSpots();
            var parkingSpotsDTO = _mapper.Map<List<ParkingSpot>, List<ParkingSpotDTO>>(parkingSpots.ToList());
            return parkingSpotsDTO;
        }
        public async Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetFreeParkingSpots()
        {
            var reservations = await _reservationRepository.GetReservationDateTimeNow();
            var parkingSpots = await _parkingSpotRepository.GetnotPrivateParkingSpots();
            var res = from p in parkingSpots where !(from r in reservations where r.parkingSpotID == p.parkingSpotID select r.parkingSpotID).Contains(p.parkingSpotID) select p;
            var parkingSpotsDTO = _mapper.Map<List<ParkingSpot>, List<ParkingSpotDTO>>(res.ToList());
            return parkingSpotsDTO;
        }

        public async Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetFreeParkingSpotsbyPrice(decimal priceHour)
        {
            var reservations = await _reservationRepository.GetReservationDateTimeNow();
            var parkingSpots = await _parkingSpotRepository.GetParkingSpotbyPrice(priceHour);
            var res = from p in parkingSpots where !(from r in reservations where r.parkingSpotID == p.parkingSpotID && (r.startTime <= DateTime.Now && r.endTime >= DateTime.Now) select r.parkingSpotID).Contains(p.parkingSpotID) select p;
            var parkingSpotsDTO = _mapper.Map<List<ParkingSpot>, List<ParkingSpotDTO>>(res.ToList());
            return parkingSpotsDTO;
        }
        public async Task<ActionResult<IEnumerable<ParkingSpotDTO>>> GetFreeParkingSpotsByDate(DateTime startDate, DateTime endDate)
        {
            var reservations = await _reservationRepository.GetSpecificReservation(startDate, endDate);
            var parkingSpots = await _parkingSpotRepository.GetnotPrivateParkingSpots();
            var res = from p in parkingSpots where !(from r in reservations where r.parkingSpotID == p.parkingSpotID select r.parkingSpotID).Contains(p.parkingSpotID) select p;
            var parkingSpotsDTO = _mapper.Map<List<ParkingSpot>, List<ParkingSpotDTO>>(res.ToList());
            return parkingSpotsDTO;
        }
        public async Task<ActionResult<ParkingSpotDTO>> GetParkingSpot(string id)
        {
            var parkingSpot = await _parkingSpotRepository.GetParkingSpot(id);
            var parkingSpotsDTO = _mapper.Map<ParkingSpot, ParkingSpotDTO>(parkingSpot);
            return parkingSpotsDTO;
        }

        public async Task<ActionResult<ParkingSpotDTO>> PutParkingSpot(string id, ParkingSpotDTO parkingSpotDTO)
        {
            var parkingSpot = _mapper.Map<ParkingSpotDTO, ParkingSpot>(parkingSpotDTO);


            await _parkingSpotRepository.PutParkingSpot(id, parkingSpot);

            return parkingSpotDTO;

        }

        public async Task<ActionResult<ParkingSpotDTO>> PostParkingSpot(ParkingSpotDTO parkingSpotDTO)
        {
            var parkingSpot = _mapper.Map<ParkingSpotDTO, ParkingSpot>(parkingSpotDTO);
            

            await _parkingSpotRepository.PostParkingSpot(parkingSpot);

            return parkingSpotDTO;

        }

        public async Task<ActionResult<ParkingSpotDTO>> DeleteParkingSpot(string id)
        {

            var parkingspot = await _parkingSpotRepository.GetParkingSpot(id);
            var parkingSpotsDTO = _mapper.Map<ParkingSpot, ParkingSpotDTO>(parkingspot);

            await _parkingSpotRepository.DeleteParkingSpot(id);

            return parkingSpotsDTO;

        }

        public async Task<ActionResult<ParkingSpotDTO>> GetSpecificParkingSpot(ReservationDTO reservationDTO) {
            var parkingspot = await _parkingSpotRepository.GetSpecificParkingSpot(reservationDTO);
            var parkingSpotsDTO = _mapper.Map<ParkingSpot, ParkingSpotDTO>(parkingspot);
            return parkingSpotsDTO;
        }

        public ValidationResult Validate(ParkingSpotDTO parkingSpotDTO)
        {
            ParkingSpotValidator validationRules = new ParkingSpotValidator();

            ValidationResult Results = validationRules.Validate(parkingSpotDTO);

            return Results;
        }
    }
}
