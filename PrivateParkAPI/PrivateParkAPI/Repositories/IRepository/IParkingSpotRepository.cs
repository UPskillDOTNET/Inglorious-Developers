using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PrivateParkAPI.DTO;
using PrivateParkAPI.Models;

namespace PrivateParkAPI.Repositories.IRepository
{
    public interface IParkingSpotRepository: IBaseRepository<ParkingSpot>
    {
        Task<IEnumerable<ParkingSpot>> GetnotPrivateParkingSpots();
        Task<IEnumerable<ParkingSpot>> GetAllParkingSpots();
        Task<IEnumerable<ParkingSpot>> GetParkingSpotbyPrice(decimal priceHour);
        Task<ParkingSpot> GetParkingSpot(string ID);
        Task<ActionResult<ParkingSpot>> PutParkingSpot(string id, ParkingSpot parkingSpot);
        Task<ActionResult<ParkingSpot>> PostParkingSpot(ParkingSpot parkingSpot);
        Task<ParkingSpot> GetSpecificParkingSpot(ReservationDTO reservationDTO);
        Task<ActionResult<ParkingSpot>> DeleteParkingSpot(string id);

    }
}
