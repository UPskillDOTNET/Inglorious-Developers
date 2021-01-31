using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrivateParkAPI.Models;
using PrivateParkAPI.Repositories.IRepository;
using PrivateParkAPI.Services.IServices;

namespace PrivateParkAPI.Services.Services
{
    public class ParkingSpotService : IParkingSpotService
    {
        private readonly IParkingSpotRepository _parkingSpotRepository;

        public ParkingSpotService(IParkingSpotRepository parkingSpotRepository)
        {
            _parkingSpotRepository = parkingSpotRepository;
        }

       

        public async Task<IEnumerable<ParkingSpot>> GetAllnotPrivate()
        {
            return await _parkingSpotRepository.GetnotPrivateParkingSpots();
        }
    }
}
