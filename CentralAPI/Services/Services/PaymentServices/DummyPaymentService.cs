using CentralAPI.DTO;
using CentralAPI.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.Services.Services
{
    public class DummyPaymentService : IWalletPaymentService
    {
        public Task<ActionResult<PaymentDTOOperation>> Pay(PaymentDTO paymentDTO)
        {
            throw new NotImplementedException();
        }

        public void PayOvertime(string reservationID, DateTime parkingEnd)
        {
        }

        public void PayReservation(string reservationID)
        {
        }

        public void PayReservation(CentralReservationDTO centralReservationDTO, ReservationPaymentDTO reservationPaymentDTO)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<ReservationPaymentDTOOperation>> PayReservation(CentralReservationDTO centralReservationDTO)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<PaymentDTOOperation>> Refund(CentralReservationDTO centralReservationDTO)
        {
            throw new NotImplementedException();
        }
    }
}
