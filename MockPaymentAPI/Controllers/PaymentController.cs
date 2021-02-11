using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MockPaymentAPI.Controllers
{
    [ApiController]
    [Route("paymentapi/payment")]
    public class PaymentController : Controller
    {
        [HttpPost]
        public ActionResult Pay(ReservationPaymentDTO reservationPaymentDTO)
        {
            if (reservationPaymentDTO == null)
            {
                return Conflict("Olá Tiago, isto deu mal");
            }
            return Ok("Olá Tiago, isto deu certo");
        }
    }
}
