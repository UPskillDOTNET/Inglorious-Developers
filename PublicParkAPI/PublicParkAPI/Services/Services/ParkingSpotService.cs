using PublicParkAPI.Contracts;
using PublicParkAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PublicParkAPI.Models;

namespace PublicParkAPI.Services.Services
{
    public class ParkingSpotService : IParkingSpotService
    {

        private readonly IParkingSpotRepository _parkingSpotRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;

        public ParkingSpotService(IParkingSpotRepository parkingSpotRepository, IMapper mapper, IReservationRepository reservationSpotRepository)
        {
            _parkingSpotRepository = parkingSpotRepository;
            _reservationRepository = reservationSpotRepository;
            _mapper = mapper;
        }


        public async Task<IEnumerable<ParkingSpotDTO>> GetParkingSpots()
        {
            var parkingSpots = await _parkingSpotRepository.GetParkingSpots();
            var parkingSpotsDTO = _mapper.Map<List<ParkingSpot>, List<ParkingSpotDTO>>(parkingSpots.ToList());
            return parkingSpotsDTO;

        }

        public async Task<ParkingSpotDTO> GetParkingSpot(string id)
        {
            var parkingSpot = await _parkingSpotRepository.GetParkingSpot(id);
            var parkingSpotDTO =  _mapper.Map<ParkingSpot, ParkingSpotDTO>(parkingSpot);
            return parkingSpotDTO;
        }

        public async Task<IEnumerable<ParkingSpotDTO>> GetFreeParkingSpots()
        {
            var reservations = await _reservationRepository.GetSpecificReservation();
            Console.WriteLine(reservations);
            var parkingSpots = await _parkingSpotRepository.GetParkingSpots();
            Console.WriteLine(parkingSpots);
            Console.WriteLine("batata");

            var res = from p in parkingSpots where !(from r in reservations where r.parkingSpotID == p.parkingSpotID && (r.startTime <= DateTime.Now && r.endTime >= DateTime.Now) select r.parkingSpotID).Contains(p.parkingSpotID) select p;
            var parkingSpotsDTO = _mapper.Map<List<ParkingSpot>, List<ParkingSpotDTO>>(res.ToList());
            return parkingSpotsDTO;
        }
    }
}
