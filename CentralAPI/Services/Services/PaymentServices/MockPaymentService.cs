using CentralAPI.DTO;
using CentralAPI.Services.IServices.IPaymentServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CentralAPI.Services.Services.PaymentServices
{
    public class MockPaymentService : IMockPaymentService
    {
        private readonly ClientHelper _helper;

        public MockPaymentService(ClientHelper helper)
        {
            _helper = helper;
        }

        public async Task<ActionResult<PaymentDTOOperation>> MockPay(PaymentDTO paymentDTO, string myUrI)
        {
            var content = new StringContent(JsonConvert.SerializeObject(paymentDTO), Encoding.UTF8, "application/json");
            var response = await _helper.PayClientAsync(myUrI, "paymentapi/payment", content);
            var responseContent = await response.Content.ReadAsAsync<bool>();
            if (responseContent != true)
            {
                PaymentDTOOperation failure = new PaymentDTOOperation
                {
                    message = "Operation not sucessfull, negative value.",
                    isSuccess = false,
                    paymentID = paymentDTO.paymentID,
                    userID = paymentDTO.userID,
                    timeStamp = DateTime.Now,
                    finalPrice = paymentDTO.finalPrice
                };
                return failure;
            }
            PaymentDTOOperation paymentDTOOperation = new PaymentDTOOperation
            {
                message = "Operation done.",
                isSuccess = true,
                paymentID = paymentDTO.paymentID,
                userID = paymentDTO.userID,
                timeStamp = DateTime.Now,
                finalPrice = paymentDTO.finalPrice
            };
            return paymentDTOOperation;

        }
    }
}
