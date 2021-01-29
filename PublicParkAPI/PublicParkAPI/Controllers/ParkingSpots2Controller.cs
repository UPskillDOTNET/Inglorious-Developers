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
using PublicParkAPI.Models;

namespace PublicParkAPI.Controllers
{
    [Route("api/testes")]
    public class ParkingSpots2Controller : Controller
    {
        private readonly IParkingSpotRepository _spotRepository;
        private readonly IMapper _mapper;

        public ParkingSpots2Controller(IParkingSpotRepository spotRepository, IMapper mapper)
        {
            _spotRepository = spotRepository;
            _mapper = mapper;
        }

        // GET: api/ParkingSpots
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParkingSpot>>> GetParkingSpots()
        {
            return Ok(_spotRepository.GetParkingSpots());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ParkingSpot>> GetParkingSpot(string id)
        {
            return Ok(_spotRepository.GetParkingSpot(id));
        }

        [Route("/freeSpots")]
        public async Task<ActionResult<IEnumerable<ParkingSpot>>> GetParkingFreeSpots()
        {
            return Ok(_spotRepository.GetParkingFreeSpots());
        }




    }
}
