using CentralAPI.DTO;
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
        private readonly WalletPaymentService _walletPaymentService;

        public WalletPaymentController(WalletPaymentService walletPaymentService)
        {
            _walletPaymentService = walletPaymentService;
        }

        //public async Task<ActionResult<ReservationPaymentDTOOperation>> PayReservation(CentralReservationDTO centralReservationDTO)
        //{
        //    return await _walletPaymentService.PayReservation(centralReservationDTO);
        //}

    }
}
