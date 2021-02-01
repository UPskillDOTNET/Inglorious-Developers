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
<<<<<<< HEAD
=======
        Task<ParkingSpot> GetSpecificParkingSpot(ReservationDTO reservationDTO);
>>>>>>> c24a4c75303b5624b4103e57ecc650f5b38aab24
        Task<ActionResult<ParkingSpot>> DeleteParkingSpot(string id);

    }
}
