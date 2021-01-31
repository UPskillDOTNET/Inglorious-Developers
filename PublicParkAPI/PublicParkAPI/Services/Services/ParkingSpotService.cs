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
        private readonly IMapper _mapper;

        public ParkingSpotService(IParkingSpotRepository parkingSpotRepository, IMapper mapper)
        {
            _parkingSpotRepository = parkingSpotRepository;
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
    }
}
