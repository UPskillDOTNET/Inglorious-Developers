﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.DTO;
using WebApp.Services.IServices;

namespace WebApp.Controllers
{
    [Authorize]
    public class MakeReservation : Controller
    {
        private readonly IParkingLotService _webParkingLotService;
        private readonly IParkingSpotService _parkingSpotService;
        private readonly IReservationService _reservationService;
        private readonly IUserService _userService;
        private readonly IPaymentService _paymentService;

        public MakeReservation(IParkingLotService parkingLotService, IParkingSpotService parkingSpotService, IReservationService reservationService, IUserService userService, IPaymentService paymentService)
        {
            _webParkingLotService = parkingLotService;
            _parkingSpotService = parkingSpotService;
            _reservationService = reservationService;
            _userService = userService;
            _paymentService = paymentService;
        }
          public async Task<IActionResult> Index(string sortOrder, string searchString)
        {

            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["LocationSortParm"] = String.IsNullOrEmpty(sortOrder) ? "location_desc" : "";
       
            var parkingLots = from p in (await _webParkingLotService.GetAllParkingLots()).Value
                              select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                parkingLots = parkingLots.Where(p => p.name.Contains(char.ToUpper(searchString[0]) + searchString[1..])
                                || p.location.Contains(char.ToUpper(searchString[0]) + searchString[1..]));
            }

            parkingLots = sortOrder switch
            {
                "name_desc" => parkingLots.OrderByDescending(p => p.name),
                "location_desc" => parkingLots.OrderByDescending(p => p.location),
                _ => parkingLots.OrderBy(p => p.name),
            };
            return View(parkingLots.ToList());
        }



        //method for view endDate pick
        public IActionResult Later(int id)
        {
            ReservationDTO reservationDTO = new()
            {
                parkingLotID = id
            };
            return View(reservationDTO);
        }

        //Method for view parkingSpot pick
        [HttpPost]
        public async Task<ActionResult<IEnumerable<ParkingSpotDTO>>> ReserveLater(ReservationDTO reservationDTO)
        {
         
            try
            {
                ViewData["startTime"] = reservationDTO.startTime;
                ViewData["endTime"] = reservationDTO.endTime;
                ViewData["plotID"] = reservationDTO.parkingLotID;
                var vm = await _parkingSpotService.GetFreeParkingSpotsByDate(reservationDTO);
                return View(vm.Value);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        //Method for view parkingSpot pick
        public async Task<ActionResult<IEnumerable<ParkingSpotDTO>>> Now(int id)
        {
            try
            {
                ViewData["parkingLotId"] = id;
                ViewBag.parkLotName = _webParkingLotService.GetParkingLotById(id).Result.Value.name;
                return View((await _parkingSpotService.GetAllFreeParkingSpots(id)).Value);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        //method for view endDate pick
        public IActionResult ReserveNow(int id, string pSpotId)
        {

            ReservationDTO reservationDTO = new()
            {
                parkingLotID = id,
                parkingSpotID = pSpotId,
                startTime = DateTime.Now,
                userID = HttpContext.User.FindFirst("sub")?.Value,
                

            };
            var userName = _userService.GetUserById(reservationDTO.userID).Result.Value;
            ViewData["UserName"] = userName.name;
            return View(reservationDTO);
        }
  
        //view for ReserveNow
        [HttpPost]
        public async Task<IActionResult> Confirm(ReservationDTO reservationDTO)
        {
            if (reservationDTO.endTime< reservationDTO.startTime)
            {
                return BadRequest("Date can't be earlier than " + reservationDTO.startTime);
            }
            reservationDTO = new ReservationDTO() {
                parkingLotID = reservationDTO.parkingLotID,
                parkingSpotID = reservationDTO.parkingSpotID,
                startTime = reservationDTO.startTime,
                endTime = reservationDTO.endTime,
                userID = HttpContext.User.FindFirst("sub")?.Value
             };

            var userName = _userService.GetUserById(reservationDTO.userID).Result.Value;
            ViewData["UserName"] = userName.name;
            var parkLot = _webParkingLotService.GetParkingLotById(reservationDTO.parkingLotID).Result.Value;
            ViewData["ParkName"] = parkLot.name;
            var reservation = await _reservationService.GetDurationAndFinalPrice(reservationDTO);
            reservationDTO = reservation.Value;

            return View(reservationDTO);
        }

        //View for ReserveLater - view is the same, model for rendering is diferent
        public async Task<IActionResult> Confirm(int id,  DateTime startDate, DateTime endDate, string pSpotId)
        {
            if (endDate < startDate)
            {
                return BadRequest("Date can't be earlier than " + startDate);
            }
            ReservationDTO reservationDTO = new() {  
                parkingLotID = id,
                parkingSpotID = pSpotId,
                startTime = startDate,
                endTime = endDate,
                userID = HttpContext.User.FindFirst("sub")?.Value
            };

            var reservation = await _reservationService.GetDurationAndFinalPrice(reservationDTO);
            reservationDTO = reservation.Value;
            return View(reservationDTO);
        }


        //Method to post and create reservation - now and later end here!
        [HttpPost]
        public async Task<IActionResult> Create(ReservationDTO reservationDTO)
        {
           

            try
            {
               var paymentOperation =  await Pay(reservationDTO);
                if (paymentOperation.Value.isSuccess == false)
                {
                    return BadRequest(paymentOperation.Value.message);
                }
                await _reservationService.PostCentralReservation(reservationDTO);
                return RedirectToAction("Index", "Reservations", new { id = reservationDTO.userID });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }



        public async Task<ActionResult<PaymentDTOOperation>> Pay(ReservationDTO reservationDTO)
        {
            PaymentDTO paymentDTO = new()
            {
                paymentID = reservationDTO.reservationID,
                reservationID = reservationDTO.reservationID,
                finalPrice = reservationDTO.finalPrice,
                userID = reservationDTO.userID,

            };
            var preferedMethod = reservationDTO.preferedMethod;
            try
            {
               var paymentOperation =  await _paymentService.PayReservation(paymentDTO, preferedMethod);
              
                return paymentOperation;
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
