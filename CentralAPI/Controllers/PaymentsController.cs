using CentralAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CentralAPI.Controllers
{
    [ApiController]
    [Route("teste")]
    public class PaymentsController : ControllerBase
    {
        public PaymentsController()
        {
        }

        public async Task<ActionResult<ReservationPaymentDTO>> PayReservation(ReservationPaymentDTO reservationPaymentDTO)
        {
            using (HttpClient client = new HttpClient())
            {

                StringContent content = new StringContent(JsonConvert.SerializeObject(reservationPaymentDTO), Encoding.UTF8, "application/json");
                string endpoint = "https://localhost:44327/paymentapi/payment";
                var response = await client.PostAsync(endpoint, content);
            }
            return reservationPaymentDTO;
        }

    }
}
