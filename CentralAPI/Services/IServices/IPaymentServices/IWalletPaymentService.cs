using CentralAPI.DTO;
using CentralAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.Services.IServices
{
    public interface IWalletPaymentService
    {
        Task<ActionResult<PaymentDTOOperation>> Pay(PaymentDTO paymentDTO);
        Task<ActionResult<PaymentDTOOperation>> Refund(CentralReservationDTO centralReservationDto);
    }
}
