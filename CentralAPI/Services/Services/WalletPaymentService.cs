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
        private readonly IReservationPaymentRepository _reservationPaymentRepository;
        private readonly IWalletService _walletService;
        private readonly ICentralReservationService _centralReservationService;

        public WalletPaymentService(IMapper mapper, IReservationPaymentRepository reservationPaymentRepository, IWalletService walletService, ICentralReservationService centralReservationService)
        {
            _mapper = mapper;
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
      
            var wallet = _walletService.GetWalletById(centralReservationDTO.userID).Result.Value;

            centralReservationDTO = _centralReservationService.GetEndTimeandFinalPrice(centralReservationDTO).Result.Value;

            var reservationToPayment = _mapper.Map<CentralReservationDTO, ReservationPaymentDTO>(centralReservationDTO);

            if (wallet.totalAmount < reservationToPayment.finalPrice)
            {
                ReservationPaymentDTOOperation failure = new ReservationPaymentDTOOperation
                {
                    message = "Operation not sucessfull, insufficient funds.",
                    isSuccess = false,
                    reservationID = reservationToPayment.reservationID,
                    userID = reservationToPayment.userID,
                    finalPrice = reservationToPayment.finalPrice
                };
                return failure;
            }

            var reservationPayment = _mapper.Map<ReservationPaymentDTO, ReservationPayment>(reservationToPayment);

            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {

                try
                {
                    await _walletService.WithdrawFromWallet(wallet.walletID, reservationToPayment.finalPrice);
                    await _centralReservationService.PostCentralReservation(centralReservationDTO);
                    await _reservationPaymentRepository.SaveReservationPayment(reservationPayment);
                }
                catch (Exception ex)
                {
                    Transaction.Current.Rollback(ex);

                }
                scope.Complete();
            }

            //await _walletService.WithdrawFromWallet(wallet.walletID, reservationToPayment.finalPrice);
            //await _centralReservationService.PostCentralReservation(centralReservationDTO);
            //await _reservationPaymentRepository.SaveReservationPayment(reservationPayment);

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
