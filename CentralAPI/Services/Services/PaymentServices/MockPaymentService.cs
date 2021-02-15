using AutoMapper;
using CentralAPI.DTO;
using CentralAPI.Models;
using CentralAPI.Repositories.IRepository;
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
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;

        public MockPaymentService(ClientHelper helper, IMapper mapper, IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
            _helper = helper;
            _mapper = mapper;
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
                    finalPrice = paymentDTO.finalPrice,
                    paymentMethod = "MockPay"
                };
                return failure;
            }
            var payment = _mapper.Map<PaymentDTO, Payment>(paymentDTO);
            await _paymentRepository.SavePayment(payment);
            PaymentDTOOperation paymentDTOOperation = new PaymentDTOOperation
            {
                message = "Operation done.",
                isSuccess = true,
                paymentID = paymentDTO.paymentID,
                userID = paymentDTO.userID,
                timeStamp = DateTime.Now,
                finalPrice = paymentDTO.finalPrice,
                paymentMethod = "MockPay"

            };
            return paymentDTOOperation;

        }
    }
}
