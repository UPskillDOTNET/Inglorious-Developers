using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PrivateParkAPI.DTO;
using PrivateParkAPI.Models;
using PrivateParkAPI.Repositories.IRepository;
using PrivateParkAPI.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateParkAPI.Services.Services
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
            var parkingLotDTO = _mapper.Map<List<ParkingLot>, List<ParkingLotDTO>>(parkingLots.ToList());
            return parkingLotDTO;
        }

        public async Task<ParkingLotDTO> GetParkingLot(int id)
        {
            var parkingLot = await _parkingLotRepository.GetParkingLot(id);
            var parkingLotDTO = _mapper.Map<ParkingLot, ParkingLotDTO>(parkingLot);
            return parkingLotDTO;
        }

        public async Task<ActionResult<ParkingLotDTO>> PutParkingLot(int id, ParkingLotDTO parkingLotDTO)
        {
            var parkingLot = _mapper.Map<ParkingLotDTO, ParkingLot>(parkingLotDTO);
            await _parkingLotRepository.PutParkingLot(id, parkingLot);
            return parkingLotDTO;
        }

        public async Task<ActionResult<ParkingLotDTO>> PostParkingLot(ParkingLotDTO parkingLotDTO)
        {
            var parkingLot = _mapper.Map<ParkingLotDTO, ParkingLot>(parkingLotDTO);
            await _parkingLotRepository.PostParkingLot(parkingLot);
            return parkingLotDTO;
        }

        //public async Task<ActionResult<ParkingLotDTO>> DeleteParkingLot(int id)
        //{
        //    var parkingLot = await _parkingLotRepository.GetParkingLot(id);
        //    var parkingLotDTO = _mapper.Map<ParkingLotDTO, ParkingLot>(parkingLot);
        //    await _parkingLotRepository.DeleteParkingLot(id);
        //    return parkingLotDTO;
        //}
    }
}
