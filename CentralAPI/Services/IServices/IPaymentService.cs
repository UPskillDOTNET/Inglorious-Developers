using CentralAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.Services.IServices
{
    public interface IPaymentService
    {
        void PayReservation(ReservationDTO reservationDTO, ReservationPaymentDTO reservationPaymentDTO);
        void PayOvertime(string reservationID, DateTime parkingEnd);
    }
}
