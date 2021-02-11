using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CentralAPI.Repositories;
using CentralAPI.Controllers;
using CentralAPI.Models;
using CentralAPI.DTO;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using CentralAPI.Services.IServices;
using CentralAPI.Repositories.IRepository;
using AutoMapper;
using CentralAPI.Utils;

namespace CentralAPI.Services.Services {
    public class CentralReservationService : ICentralReservationService {

        private readonly ICentralReservationRepository _centralReservationRepository;
        private readonly IParkingLotRepository _parkingLotRepository;
        //private readonly ParkingSpotController _parkingSpotsController;
        //private readonly IUserRepository _userRepository;
        private readonly QRgenerator _qRgenerator;
        private readonly EmailService _emailService;

        private readonly IMapper _mapper;

        public CentralReservationService(ICentralReservationRepository centralReservationRepository, IParkingLotRepository parkingLotRepository,QRgenerator qRgenerator, EmailService emailService ,IMapper mapper) {
            _centralReservationRepository = centralReservationRepository;
            _parkingLotRepository = parkingLotRepository;
            _qRgenerator = qRgenerator;
            _emailService = emailService; 
            //_parkingSpotsController = parkingSpotController;
            //_userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ActionResult<IEnumerable<CentralReservationDTO>>> GetAllCentralReservations() {
            var centralReservations = await _centralReservationRepository.GetAllCentralReservations();
            var centralReservationsDTO = _mapper.Map<List<CentralReservation>, List<CentralReservationDTO>>(centralReservations.ToList());
            return centralReservationsDTO;
        }

        public async Task<ActionResult<IEnumerable<CentralReservationDTO>>> GetCentralReservationsNotCancelled() {
            var centralReservations = await _centralReservationRepository.GetCentralReservationsNotCancelled();
            var centralReservationsDTO = _mapper.Map<List<CentralReservation>, List<CentralReservationDTO>>(centralReservations.ToList());
            return centralReservationsDTO;
        }

        public async Task<ActionResult<CentralReservationDTO>> GetCentralReservationById(string id) {
            var centralReservation = await _centralReservationRepository.GetCentralReservationById(id);
            var centralReservationDTO = _mapper.Map<CentralReservation, CentralReservationDTO>(centralReservation);
            return centralReservationDTO;
        }

        public async Task<ActionResult<CentralReservationDTO>> PostCentralReservation(CentralReservationDTO centralReservationDTO) {
            await GetEndTimeandFinalPrice(centralReservationDTO);
            var qr = _qRgenerator.MakeQR(centralReservationDTO);
            await _emailService.SendQRToEmailAsync(qr.Result.Value, centralReservationDTO.userID, centralReservationDTO.centralReservationID);
            var centralReservation = _mapper.Map<CentralReservationDTO, CentralReservation>(centralReservationDTO);
            await _centralReservationRepository.PostCentralReservation(centralReservation);
            return centralReservationDTO;
        }

        public async Task<ActionResult<CentralReservationDTO>> GetEndTimeandFinalPrice(CentralReservationDTO centralReservationDTO) {
            var parkingSpot = await _parkingLotRepository.GetParkingLot(centralReservationDTO.parkingLotID);
            //centralReservationDTO.endTime = centralReservationDTO.startTime.AddHours(centralReservationDTO.hours);
            //centralReservationDTO.finalPrice = centralReservationDTO.hours * parkingSpot.priceHour;
            return centralReservationDTO;
        }

        public async Task<ActionResult<CentralReservation>> PatchCentralReservation(string id) {
            return await _centralReservationRepository.PatchCentralReservation(id);
        }
        public async Task<bool> FindCentralReservationAny(string id) {
            return await _centralReservationRepository.FindCentralReservationAny(id);
        }

        //public ValidationResult Validate(CentralReservationDTO centralReservationDTO) {
        //    CentralReservationValidator validationRules = new CentralReservationValidator();
        //    ValidationResult Results = validationRules.Validate(centralReservationDTO);
        //    return Results;
        //}
    }
}


