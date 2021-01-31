using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrivateParkAPI.Models;

namespace PrivateParkAPI.Services.IServices
{
    public interface IParkingSpotService
    {
        Task<IEnumerable<ParkingSpot>> GetAllnotPrivate();

        
    }
}
