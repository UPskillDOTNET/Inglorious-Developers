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
    public class WalletPaymentController : ControllerBase
    {
        private readonly IWalletPaymentService _walletPaymentService;

        public WalletPaymentController(IWalletPaymentService walletPaymentService)
        {
            _walletPaymentService = walletPaymentService;
        }
        [Route("/api/[controller]/payment/")]
        public async Task<ActionResult<PaymentDTOOperation>> PayReservation(PaymentDTO paymentDTO)
        {
            return await _walletPaymentService.Pay(paymentDTO);
        }

        [Route("/api/[controller]/refund/")]
        public async Task<ActionResult<PaymentDTOOperation>> RefundReservation(PaymentDTO paymentDTO)
        {
            return await _walletPaymentService.Refund(paymentDTO);
        }

    }
}
