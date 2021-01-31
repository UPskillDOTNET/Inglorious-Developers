using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicParkAPI.Contracts;
using PublicParkAPI.Data;
using PublicParkAPI.DTO;
using PublicParkAPI.Models;
using PublicParkAPI.Services;

namespace PublicParkAPI.Controllers
{
    [Route("api/testes")]
    public class ParkingSpots2Controller : Controller
    {
        private readonly IParkingSpotService _parkingSpotService;

        public ParkingSpots2Controller(IParkingSpotService parkingSpotService)
        {
            _parkingSpotService = parkingSpotService;
        }


        // GET: api/ParkingSpots
        [HttpGet]
        public Task<IEnumerable<ParkingSpotDTO>> GetParkingSpots()
        {
            return _parkingSpotService.GetParkingSpots();
        }

        [HttpGet("{id}")]
        public Task<ParkingSpotDTO> GetParkingSpot(string id)
        {
            return _parkingSpotService.GetParkingSpot(id);
        }

        //[Route("/freeSpots")]
        //public async Task<ActionResult<IEnumerable<ParkingSpot>>> GetParkingFreeSpots()
        //{
        //    return Ok(_spotRepository.GetParkingFreeSpots());
        //}




    }
}
