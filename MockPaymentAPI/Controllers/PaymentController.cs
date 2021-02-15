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
        public ActionResult Pay(PaymentDTO PaymentDTO)
        {
            if (PaymentDTO.finalPrice == 100)
            {
                return Ok(false);
            }
            return Ok(true);
        }
    }
}
