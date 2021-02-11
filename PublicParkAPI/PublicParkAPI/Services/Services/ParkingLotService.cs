using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using PublicParkAPI.Contracts;
using PublicParkAPI.DTO;
using PublicParkAPI.Models;
using PublicParkAPI.Services.IServices;
using PublicParkAPI.Utils;
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

        public async Task<ActionResult<ParkingLotDTO>> GetParkingLot(int id)
        {
            var parkingLot = await _parkingLotRepository.GetParkingLot(id);
            var parkingLotDTO = _mapper.Map<ParkingLot, ParkingLotDTO>(parkingLot);
            return parkingLotDTO;
        }

        public async Task<ActionResult<ParkingLotDTO>> PutParkingLot( ParkingLotDTO parkingLotDTO)
        {
            var parkingLot = _mapper.Map<ParkingLotDTO, ParkingLot>(parkingLotDTO);
            var parkingLotReturn = await _parkingLotRepository.PutParkingLot(parkingLot);
            var parkingLotDTOReturn = _mapper.Map<ParkingLot, ParkingLotDTO>(parkingLotReturn);
            return parkingLotDTOReturn;
        }

        public async Task<ActionResult<ParkingLotDTO>> PostParkingLot(ParkingLotDTO parkingLotDTO)
        {
            var parkingLot = _mapper.Map<ParkingLotDTO, ParkingLot>(parkingLotDTO);
            var parkingLotReturn = await _parkingLotRepository.PostParkingLot(parkingLot);
            var parkingLotDTOReturn = _mapper.Map<ParkingLot, ParkingLotDTO>(parkingLotReturn);
            return parkingLotDTOReturn;
        }

        public ValidationResult Validate(ParkingLotDTO parkingLotDTO)
        {
            ParkingLotValidator validationRules = new ParkingLotValidator();
            ValidationResult Results = validationRules.Validate(parkingLotDTO);
            return Results;
        }
    }
}
