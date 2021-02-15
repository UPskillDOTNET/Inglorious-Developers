using CentralAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.Services.IServices.IPaymentServices
{
    public interface IMockPaymentService
    {
        Task<ActionResult<PaymentDTOOperation>> MockPay(PaymentDTO paymentDTO, string url);
    }
}
