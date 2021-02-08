//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using CentralAPI.Repositories;
//using CentralAPI.Models;
//using CentralAPI.DTO;

//namespace CentralAPI.Services.Services {
//    public class CentralReservationService : ICentralReservationService  {

//        private readonly ICentralReservationRepository _centralReservationRepository;
//        private readonly IParkingSpotRepository _parkingSpotRepository;
//        private readonly IMapper _mapper;

//        public CentralReservationService(ICentralReservationRepository centralReservationRepository, IMapper mapper, IParkingSpotRepository parkingSpotRepository) {
//            _centralReservationRepository = centralReservationRepository;
//            _parkingSpotRepository = parkingSpotRepository;
//            _mapper = mapper;
//        }


//        public async Task<ActionResult<IEnumerable<CentralReservationDTO>>> GetCentralReservations() {
//            var centralReservations = await _centralReservationRepository.GetCentralReservations();
//            var centralReservationsDTO = _mapper.Map<List<CentralReservation>, List<CentralReservationDTO>>(centralReservations.ToList());
//            return centralReservationsDTO;
//        }

//        public async Task<ActionResult<IEnumerable<CentralReservationDTO>>> GetCentralReservationsNotCancelled() {
//            var centralReservations = await _centralReservationRepository.GetCentralReservationsNotCancelled();
//            var centralReservationsDTO = _mapper.Map<List<CentralReservation>, List<CentralReservationDTO>>(centralReservations.ToList());
//            return centralReservationsDTO;
//        }

//        public async Task<ActionResult<CentralReservationDTO>> GetCentralReservation(string id) {
//            var centralReservation = await _centralReservationRepository.GetCentralReservation(id);
//            var centralReservationDTO = _mapper.Map<CentralReservation, CentralReservationDTO>(centralReservation);
//            return centralReservationDTO;
//        }

//        public async Task<ActionResult<CentralReservationDTO>> PostCentralReservation(CentralReservationDTO centralReservationDTO) {
//            await GetEndTimeandFinalPrice(centralReservationDTO);
//            var centralReservation = _mapper.Map<CentralReservationDTO, CentralReservation>(centralReservationDTO);
//            await _centralReservationRepository.PostCentralReservation(centralReservation);
//            return centralReservationDTO;
//        }

//        public async Task<ActionResult<CentralReservationDTO>> GetEndTimeandFinalPrice(CentralReservationDTO centralReservationDTO) {
//            var parkingSpot = await _parkingSpotRepository.FindParkingSpot(centralReservationDTO.parkingSpotID);
//            centralReservationDTO.endTime = centralReservationDTO.startTime.AddHours(centralReservationDTO.hours);
//            centralReservationDTO.finalPrice = centralReservationDTO.hours * parkingSpot.priceHour;
//            return centralReservationDTO;
//        }

//        public async Task<ActionResult<CentralReservation>> PatchCentralReservation(string id) {
//            return await _centralReservationRepository.PatchCentralReservation(id);
//        }
//        public async Task<bool> FindCentralReservationAny(string id) {
//            return await _centralReservationRepository.FindCentralReservationAny(id);
//        }

//        public ValidationResult Validate(CentralReservationDTO centralReservationDTO) {
//            CentralReservationValidator validationRules = new CentralReservationValidator();
//            ValidationResult Results = validationRules.Validate(centralReservationDTO);
//            return Results;
//        }


//    }
//}


