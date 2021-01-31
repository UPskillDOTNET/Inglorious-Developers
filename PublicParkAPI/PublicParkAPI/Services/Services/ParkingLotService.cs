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
    public class ParkingLotService : IParkingLotService
    {
        private readonly IParkingLotRepository _parkingLotRepository;
        private readonly IMapper _mapper;

        public ParkingLotService(IParkingLotRepository parkingLotRepository, IMapper mapper)
        {
            _parkingLotRepository = parkingLotRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ParkingLotDTO>> GetParkingLots()
        {
            var parkingLots = await _parkingLotRepository.GetParkingLots();
            var parkingLotsDTO = _mapper.Map<List<ParkingLot>, List<ParkingLotDTO>>(parkingLots.ToList());
            return parkingLotsDTO;
        }
    }
}
