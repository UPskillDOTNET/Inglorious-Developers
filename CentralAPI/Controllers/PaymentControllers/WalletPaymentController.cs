using CentralAPI.DTO;
using CentralAPI.Models;
using CentralAPI.Repositories.IRepository;
using CentralAPI.Services.IServices;
using CentralAPI.Services.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IDefaultPayment _defaultPayment;
        private readonly IWalletPaymentService _walletPaymentService;

        public PaymentController(IDefaultPayment defaultPayment, IWalletPaymentService walletPaymentService)
        {
            _defaultPayment = defaultPayment;
            _walletPaymentService = walletPaymentService;
        }

        [Route("/api/payment/{preferedMethod}")]
        public async Task<ActionResult<PaymentDTOOperation>> PayReservation(PaymentDTO paymentDTO, string preferedMethod)
        {
            return await _defaultPayment.DefaultPayments(paymentDTO, preferedMethod );
        }

        [Route("/api/refund/")]
        public async Task<ActionResult<PaymentDTOOperation>> RefundReservation(PaymentDTO paymentDTO)
        {
            return await _walletPaymentService.Refund(paymentDTO);
        }

    }
}
