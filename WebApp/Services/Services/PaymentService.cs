using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApp.DTO;
using WebApp.Services.IServices;
using WebApp.Services.Services.Utils;

namespace WebApp.Services.Services
{
    public class PaymentService:IPaymentService
    {
        private readonly APIHelper _helper;

        public PaymentService( APIHelper helper)
        {
            _helper = helper;
        }

        public async Task<ActionResult<PaymentDTOOperation>> PayReservation(PaymentDTO paymentDTO, string preferedMethod)
        {
            var content = new StringContent(JsonConvert.SerializeObject(paymentDTO), Encoding.UTF8, "application/json");
            var response = await _helper.PostClientAsync("/central/payment/" + preferedMethod, content);
            return await response.Content.ReadAsAsync<PaymentDTOOperation>();

        }

    }
}
