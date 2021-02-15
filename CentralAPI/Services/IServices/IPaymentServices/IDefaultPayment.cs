using CentralAPI.DTO;
using CentralAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.Services.IServices
{
    public interface IDefaultPayment
    {
        Task<ActionResult<PaymentDTOOperation>> DefaultPayments(PaymentDTO paymentDTO, string preferedMethod);
    }
}
