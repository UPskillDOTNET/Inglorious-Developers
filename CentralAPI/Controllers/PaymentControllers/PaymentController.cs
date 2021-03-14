using CentralAPI.DTO;
using CentralAPI.Models;
using CentralAPI.Repositories.IRepository;
using CentralAPI.Services.IServices;
using CentralAPI.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("central/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IDefaultPaymentService _defaultPayment;
        private readonly IWalletPaymentService _walletPaymentService;

        public PaymentController(IDefaultPaymentService defaultPayment, IWalletPaymentService walletPaymentService)
        {
            _defaultPayment = defaultPayment;
            _walletPaymentService = walletPaymentService;
        }

        [Route("/central/payment/{preferedMethod}")]
        public async Task<ActionResult<PaymentDTOOperation>> PayReservation(PaymentDTO paymentDTO, string preferedMethod)
        {
            return await _defaultPayment.DefaultPayments(paymentDTO, preferedMethod );
        }


    }
}
