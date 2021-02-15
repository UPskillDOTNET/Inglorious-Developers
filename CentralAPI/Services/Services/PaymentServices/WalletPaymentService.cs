using AutoMapper;
using CentralAPI.DTO;
using CentralAPI.Models;
using CentralAPI.Repositories.IRepository;
using CentralAPI.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace CentralAPI.Services.Services
{
    public class WalletPaymentService : IWalletPaymentService
    { 
        private readonly IMapper _mapper;
        //private readonly IReservationPaymentRepository _reservationPaymentRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IWalletService _walletService;
        private readonly ICentralReservationService _centralReservationService;

        public WalletPaymentService(IMapper mapper, IPaymentRepository paymentRepository, IWalletService walletService, ICentralReservationService centralReservationService)
        {
            _mapper = mapper;
            _paymentRepository = paymentRepository;
            _walletService = walletService;
            _centralReservationService = centralReservationService;
        }

        public async Task<ActionResult<PaymentDTOOperation>> Pay(PaymentDTO paymentDTO)
        {
            var wallet = await _walletService.GetWalletById(paymentDTO.userID);
            var walletDTO = wallet.Value;

            if (walletDTO.totalAmount < paymentDTO.finalPrice)
            {
                PaymentDTOOperation failure = new PaymentDTOOperation
                {
                    message = "Operation not sucessfull, insufficient funds.",
                    isSuccess = false,
                    paymentID = paymentDTO.paymentID,
                    userID = paymentDTO.userID,
                    timeStamp = DateTime.Now,
                    finalPrice = paymentDTO.finalPrice,
                    paymentMethod = "WalletPayment"
                };
                return failure;
            }

            var payment = _mapper.Map<PaymentDTO, Payment>(paymentDTO);

            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    await _walletService.WithdrawFromWallet(walletDTO.walletID, paymentDTO.finalPrice);
                    await _paymentRepository.SavePayment(payment);
                }
                catch (Exception)
                {

                    throw;
                } finally
                {
                    scope.Complete();
                }
            }

                PaymentDTOOperation paymentDTOOperation = new PaymentDTOOperation
            {
                message = "Operation done.",
                isSuccess = true,
                paymentID = paymentDTO.paymentID,
                userID = paymentDTO.userID,
                timeStamp = DateTime.Now,
                finalPrice = paymentDTO.finalPrice,
                paymentMethod = "WalletPayment"
                };

            return paymentDTOOperation;
        }

        public async Task<ActionResult<PaymentDTOOperation>> Refund(PaymentDTO paymentDTO)
        {
            var wallet = await _walletService.GetWalletById(paymentDTO.userID);
            var walletDTO = wallet.Value;

            if (paymentDTO.finalPrice < 0)
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

            var payment = _mapper.Map<PaymentDTO, Payment>(paymentDTO);

            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    await _walletService.DepositToWallet(walletDTO.walletID, paymentDTO.finalPrice);
                    await _paymentRepository.SavePayment(payment);
                }
                catch (Exception)
                {

                    throw;
                } finally
                {
                    scope.Complete();
                }
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

