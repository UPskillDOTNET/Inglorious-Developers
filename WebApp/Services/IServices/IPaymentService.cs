using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.DTO;

namespace WebApp.Services.IServices
{
    public interface IPaymentService
    {
      Task<ActionResult<PaymentDTOOperation>> PayReservation(PaymentDTO paymentDTO, string preferedMethod);
    }
}
