using CentralAPI.DTO;
using CentralAPI.Models;
using CentralAPI.Repositories.IRepository;
using CentralAPI.Services.IServices;
using CentralAPI.Services.IServices.IPaymentServices;
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
        private readonly IUserRepository _userRepository;
        private readonly IPaymentMethodRepository _paymentMethodRepository;
        private readonly IWalletPaymentService _walletPaymentService;
        private readonly IMockPaymentService _mockPaymentService;

        public DefaultPayment(IUserRepository userRepository, IWalletPaymentService walletPaymentService,IPaymentMethodRepository paymentMethodRepository , IMockPaymentService mockPaymentService)
        {
            _userRepository = userRepository;
            _walletPaymentService = walletPaymentService;
            _mockPaymentService = mockPaymentService;
            _paymentMethodRepository = paymentMethodRepository;
        }

        public async Task<ActionResult<PaymentDTOOperation>> DefaultPayments(PaymentDTO paymentDTO, string preferedMethod)
        {
            if (preferedMethod == "default")
            {
                var user = await _userRepository.GetAll().Where(p => p.Id== paymentDTO.userID).FirstOrDefaultAsync();

                var paymentMethod = await _paymentMethodRepository.GetPaymentMethodByID(user.paymentMethodID);
                var paymentMethodName = paymentMethod.name;
                preferedMethod = paymentMethodName;
            }

            switch (preferedMethod)
            {
                case "Wallet":
                    return await _walletPaymentService.Pay(paymentDTO);
                case "MockPayment":
                    return await _mockPaymentService.MockPay(paymentDTO,"https://localhost:44327/");
                default:
                    break;
            }
            return null;
        }
    }
}
