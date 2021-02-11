using CentralAPI.DTO;
using CentralAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.Services.IServices
{
    public interface IPaymentService
    {
        Task<ActionResult<ReservationPayment>> PayReservation(CentralReservationDTO centralReservationDTO);
        void PayOvertime(string reservationID, DateTime parkingEnd);
    }
}
