using System;
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
        private readonly IMapper _mapper;
        public ParkingSpotService(IParkingSpotRepository parkingSpotRepository, IMapper mapper)
        {
            _parkingSpotRepository = parkingSpotRepository;
            _mapper = mapper;
        }

       

       
        
        public async Task<IEnumerable<ParkingSpotDTO>> GetAllnotPrivate()
        {
            var parkingSpots = await _parkingSpotRepository.GetnotPrivateParkingSpots();
            var parkingSpotsDTO = _mapper.Map<List<ParkingSpot>, List<ParkingSpotDTO>>(parkingSpots.ToList());
            return parkingSpotsDTO;
        }
    }
}
