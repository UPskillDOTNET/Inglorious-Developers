using CentralAPI.DTO;
using CentralAPI.Models;
using CentralAPI.Repositories.IRepository;
using CentralAPI.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.Services.Services
{
    public class DefaultPayment : IDefaultPayment
    {
        private readonly IPaymentMethodRepository _paymentMethodRepository;
        private readonly IWalletPaymentService _walletPaymentService;

        public DefaultPayment(IPaymentMethodRepository paymentMethodRepository, IWalletPaymentService walletPaymentService)
        {
            _paymentMethodRepository = paymentMethodRepository;
            _walletPaymentService = walletPaymentService;
        }

        public async Task<ActionResult<PaymentMethod>> DefaultPayments(string userID, PaymentDTO paymentDTO)
        {
            var paymentMethod = await _paymentMethodRepository.GetAll().Where(p => p.userID == userID).FirstOrDefaultAsync();

            switch (paymentMethod.name)
            {
                case "wallet":
                    await _walletPaymentService.Pay(paymentDTO);
                    break;
                //case "mock":
                    //await 
                default:
                    break;
            }
            return null;
        }
    }
}
