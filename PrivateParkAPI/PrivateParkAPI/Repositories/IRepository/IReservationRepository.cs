using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrivateParkAPI.DTO;
using PrivateParkAPI.Models;

namespace PrivateParkAPI.Repositories.IRepository
{
    public interface IReservationRepository: IBaseRepository<Reservation>
    {
        Task<IEnumerable<Reservation>> GetReservations();
        Task<IEnumerable<Reservation>> GetSpecificReservation();
        //Task<List<ReservationDTO>> GetAllReservations();
        //Task<List<ReservationDTO>> GetParkingFreeSpots();
        //Task<List<ReservationDTO>> GetParkingSpecificFreeSpots();
        //Task<List<ReservationDTO>> GetParkingPriceFreeSpots();
        //Task<List<ReservationDTO>> GetParkingCoveredFreeSpots();
        //Task <ReservationDTO> GetReservation();
        //Task<ReservationDTO> PutReservation();
        //Task<ReservationDTO> PostReservation();
        //Task<ReservationDTO> DeleteReservation();
        //bool ReservationExists();
    }
}
