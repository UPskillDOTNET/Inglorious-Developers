using AutoMapper;
using CentralAPI.DTO;
using CentralAPI.Models;
using CentralAPI.Repositories.IRepository;
using CentralAPI.Services.IServices;
using CentralAPI.Services.IServices.TransactionTest;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.Services.Services
{
    public class WalletPaymentService : IWalletPaymentService
    { 
        private readonly IMapper _mapper;
        private readonly ITransaction _transaction;
        private readonly IReservationPaymentRepository _reservationPaymentRepository;
        private readonly IWalletService _walletService;
        private readonly ICentralReservationService _centralReservationService;

        public WalletPaymentService(IMapper mapper, ITransaction transaction, IReservationPaymentRepository reservationPaymentRepository, IWalletService walletService, ICentralReservationService centralReservationService)
        {
            _mapper = mapper;
            _transaction = transaction;
            _reservationPaymentRepository = reservationPaymentRepository;
            _walletService = walletService;
            _centralReservationService = centralReservationService;
        }

        public void PayOvertime(string reservationID, DateTime parkingEnd)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<ReservationPaymentDTOOperation>> PayReservation(CentralReservationDTO centralReservationDTO)
        {
      
            var reservationToPayment = _mapper.Map<CentralReservationDTO, ReservationPaymentDTO>(centralReservationDTO);

            var wallet = _walletService.GetWalletById(reservationToPayment.userID).Result.Value;

            if (wallet.totalAmount < reservationToPayment.finalPrice)
            {
                ReservationPaymentDTOOperation failure = new ReservationPaymentDTOOperation
                {
                    message = "Operation not sucessfull, insufficient funds.",
                    isSuccess = false,
                };
                return failure;
            }

            var reservationPayment = _mapper.Map<ReservationPaymentDTO, ReservationPayment>(reservationToPayment);

            try
            {
                _transaction.Begin();
                await _walletService.WithdrawFromWallet(wallet.walletID, reservationToPayment.finalPrice);
                await _centralReservationService.PostCentralReservation(centralReservationDTO);
                await _reservationPaymentRepository.SaveReservationPayment(reservationPayment);

                _transaction.Commit();
            }
            catch (Exception ex)
            {
                _transaction.Rollback();
                throw;
            }
            //await _walletService.WithdrawFromWallet(wallet.walletID, reservationToPayment.finalPrice);

            //await _centralReservationService.PostCentralReservation(centralReservationDTO);

            //await _reservationPaymentRepository.SaveReservationPayment(reservationPayment);
            //WalletReservationPayment.PaymentLogic(wallet, centralReservationDTO, reservationPayment, reservationToPayment);

            ReservationPaymentDTOOperation reservationPaymentDTOOperation = new ReservationPaymentDTOOperation
            {
                reservationID = reservationPayment.reservationID,
                finalPrice = reservationPayment.finalPrice,
                userID = reservationPayment.userID,
                message = "Operation done",
                isSuccess = true,
            };

            return reservationPaymentDTOOperation;
        }
    }
}
