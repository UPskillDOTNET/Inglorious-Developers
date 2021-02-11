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
        //private readonly IPaymentRepository _paymentRepository;

        public WalletPaymentController(IWalletPaymentService walletPaymentService)
        {
            _walletPaymentService = walletPaymentService;
        }

        public async Task<ActionResult<ReservationPaymentDTOOperation>> PayReservation(CentralReservationDTO centralReservationDTO)
        {
            return await _walletPaymentService.PayReservation(centralReservationDTO);
        }

        //[HttpGet("/seeallpayments")]
        //public IEnumerable<Payment> GetPayments()
        //{
        //    return _paymentRepository.GetPayments();
        //}

    }
}
