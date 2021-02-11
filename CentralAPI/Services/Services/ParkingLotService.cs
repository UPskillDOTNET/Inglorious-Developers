using AutoMapper;
using CentralAPI.DTO;
using CentralAPI.Models;
using CentralAPI.Repositories.IRepository;
using CentralAPI.Services.IServices;
using CentralAPI.Utils;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.Services.Services
{
    public class ParkingLotService :IParkingLotService
    {
        private readonly IParkingLotRepository _parkingLotRepository;
        private readonly IMapper _mapper;

        public ParkingLotService(IParkingLotRepository parkingLotRepository, IMapper mapper)
        {
            _parkingLotRepository = parkingLotRepository;
            _mapper = mapper;
        }


        public async Task<ActionResult<IEnumerable<ParkingLotDTO>>> GetParkingLots()
        {
            var parkingLots = await _parkingLotRepository.GetParkingLots();
            var parkingLotDTO = _mapper.Map<List<ParkingLot>, List<ParkingLotDTO>>(parkingLots.ToList());
            return parkingLotDTO;
        }

        public async Task<ActionResult<ParkingLotDTO>> GetParkingLot(int id)
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

        public ValidationResult Validate(ParkingLotDTO parkingLotDTO) {
            ParkingLotValidator validationRules = new ParkingLotValidator();
            ValidationResult Results = validationRules.Validate(parkingLotDTO);
            return Results;
        }
    }
}
