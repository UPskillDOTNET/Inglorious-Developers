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
        private readonly IReservationService _reservationService;
        //private readonly ParkingSpotController _parkingSpotsController;
        //private readonly IUserRepository _userRepository;
        private readonly IParkingSpotService _parkingSpotService;
        private readonly ISubletService _subletService;
        private readonly QRgenerator _qRgenerator;
        private readonly EmailService _emailService;
        private readonly IMapper _mapper;

        public CentralReservationService(ICentralReservationRepository centralReservationRepository, IReservationService reservationService, IParkingLotRepository parkingLotRepository,QRgenerator qRgenerator, EmailService emailService, IParkingSpotService parkingSpotService,ISubletService subletService , IMapper mapper) {
            _centralReservationRepository = centralReservationRepository;
            _parkingLotRepository = parkingLotRepository;
            _qRgenerator = qRgenerator;
            _emailService = emailService;
            _subletService = subletService;
            _reservationService = reservationService;
           
            //_parkingSpotsController = parkingSpotController;
            //_userRepository = userRepository;
            _parkingSpotService = parkingSpotService;
            _mapper = mapper;
        }

        public async Task<ActionResult<IEnumerable<CentralReservationDTO>>> GetAllCentralReservations() {
            var centralReservations = await _centralReservationRepository.GetAllCentralReservations();
            var centralReservationsDTO = _mapper.Map<List<CentralReservation>, List<CentralReservationDTO>>(centralReservations.ToList());
            return centralReservationsDTO;
        }
        public async Task<ActionResult<IEnumerable<CentralReservationDTO>>> GetAllCentralReservationsById(string id)
        {
            var centralReservations = await _centralReservationRepository.GetAllCentralReservationsById(id);
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

        public async Task<ActionResult<CentralReservationDTO>> GetCentralReservationByUserId(string userID) {
            var centralReservation = await _centralReservationRepository.GetCentralReservationByUserId(userID);
            var centralReservationDTO = _mapper.Map<CentralReservation, CentralReservationDTO>(centralReservation);
            return centralReservationDTO;
        }

        public async Task<ActionResult<CentralReservationDTO>> PostCentralReservation(CentralReservationDTO centralReservationDTO) {  
            var result= await GetEndTimeandFinalPrice(centralReservationDTO);
            centralReservationDTO = result.Value;
            //var qr = _qRgenerator.MakeQR(centralReservationDTO);
            //await _emailService.SendQRToEmailAsync(qr.Result.Value, centralReservationDTO.userID, centralReservationDTO.centralReservationID);
            var centralReservation = _mapper.Map<CentralReservationDTO, CentralReservation>(centralReservationDTO);
            //If returns true when should return false?
            if (await subletReservationExists(centralReservationDTO))
            {
                var subletReservation = await _centralReservationRepository.GetsubletReservation(centralReservation);
                try
                {
                    await _subletService.CreateSublet(centralReservationDTO, subletReservation);
                    return centralReservationDTO;
                }
                catch (Exception)
                {
                    throw new Exception("cant compute");
                }
            }
           
            centralReservation = await _centralReservationRepository.PostCentralReservation(centralReservation);
            centralReservation.reservationID = centralReservation.centralReservationID;
            await _reservationService.PostReservation(centralReservation, centralReservation.parkingLotID);
            
            return centralReservationDTO;
        }
        public async Task<ActionResult<CentralReservationDTO>> PostCentralReservationNotCompleted(CentralReservationDTO centralReservationDTO)
        {
            var qr = _qRgenerator.MakeQRnotCompleted(centralReservationDTO);
            await _emailService.SendQRToEmailAsync(qr.Result.Value, centralReservationDTO.userID, centralReservationDTO.centralReservationID);
            var centralReservation = _mapper.Map<CentralReservationDTO, CentralReservation>(centralReservationDTO);
            await _centralReservationRepository.PostCentralReservation(centralReservation);
            return centralReservationDTO;
        }

        public async Task<ActionResult<CentralReservationDTO>> PatchCentralReservation(string id) {
            var reservation = await _centralReservationRepository.Find(id);
            reservation.isCancelled = true;            
            var reservations = await _centralReservationRepository.PatchCentralReservation(reservation);
            var reservationDTO = _mapper.Map<CentralReservation, CentralReservationDTO>(reservations);
            return reservationDTO;

        }

        public async Task<ActionResult<CentralReservationDTO>> CompleteCentralReservation(string id)
        {
            var reservation = await _centralReservationRepository.Find(id);
            var result = await GetEndTimeandFinalPriceForComplete(reservation);
            var reservations = await _centralReservationRepository.PatchCentralReservation(result.Value);
            var reservationDTO = _mapper.Map<CentralReservation, CentralReservationDTO>(reservations);
            return reservationDTO;

        }
        public async Task<ActionResult<CentralReservationDTO>> SubletCentralReservation(string id)
        {
            var reservation = await _centralReservationRepository.Find(id);
            reservation.forSublet = true;
            var reservations = await _centralReservationRepository.PatchCentralReservation(reservation);
            var reservationDTO = _mapper.Map<CentralReservation, CentralReservationDTO>(reservations);
            return reservationDTO;

        }

        public async Task<ActionResult<CentralReservationDTO>> GetEndTimeandFinalPrice(CentralReservationDTO centralReservationDTO)
        {
            var parkingSpot = await _parkingSpotService.GetParkingSpotById(centralReservationDTO.parkingLotID, centralReservationDTO.parkingSpotID);
            centralReservationDTO.endTime = centralReservationDTO.startTime.AddHours(centralReservationDTO.hours);
            centralReservationDTO.finalPrice = centralReservationDTO.hours * parkingSpot.Value.priceHour;
            return centralReservationDTO;
        }

        public async Task<ActionResult<CentralReservation>> GetEndTimeandFinalPriceForComplete(CentralReservation centralReservation)
        {
            var parkingSpot = await _parkingSpotService.GetParkingSpotById(centralReservation.parkingLotID, centralReservation.parkingSpotID);
            var hours = DateTime.Now - centralReservation.startTime;
            centralReservation.endTime = centralReservation.startTime.AddHours(hours.TotalHours);
            centralReservation.hours = Convert.ToInt32(hours.TotalHours);
            centralReservation.finalPrice = centralReservation.hours * parkingSpot.Value.priceHour;
            return centralReservation;
        }
        public async Task<bool> FindCentralReservationAny(string id) {
            return await _centralReservationRepository.FindCentralReservationAny(id);
        }

        public async Task<bool> FindCentralReservationAnyByUser(string userID) {
            return await _centralReservationRepository.FindCentralReservationAnyByUser(userID);
        }

        //Erro do Parking Lot 2
        public async Task<bool> subletReservationExists(CentralReservationDTO centralReservationDTO)
        {
            var reservation = _mapper.Map<CentralReservationDTO, CentralReservation>(centralReservationDTO);
            var result = await _centralReservationRepository.subletReservationAny(reservation);
            return result;
        }

        //public ValidationResult Validate(CentralReservationDTO centralReservationDTO) {
        //    CentralReservationValidator validationRules = new CentralReservationValidator();
        //    ValidationResult Results = validationRules.Validate(centralReservationDTO);
        //    return Results;
        //}
    }
}


