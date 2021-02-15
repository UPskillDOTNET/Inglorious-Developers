using CentralAPI.DTO;
using CentralAPI.Models;
using CentralAPI.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.Services.IServices.TransactionTest
{
    public class WalletReservationPayment
    {

        private readonly ITransaction _transaction;
        private readonly IWalletService _walletService;
        private readonly ICentralReservationService _centralReservationService;
        private readonly IReservationPaymentRepository _reservationPaymentRepository;

        public WalletReservationPayment(ITransaction transaction, IWalletService walletService, ICentralReservationService centralReservationService, IReservationPaymentRepository reservationPaymentRepository)
        {
            _transaction = transaction;
            _walletService = walletService;
            _centralReservationService = centralReservationService;
            _reservationPaymentRepository = reservationPaymentRepository;
        }

        public void PaymentLogic(WalletDTO wallet, CentralReservationDTO centralReservationDTO, ReservationPayment reservationPayment, ReservationPaymentDTO reservationToPayment )
        {

            try
            {
                _transaction.Begin();
                _walletService.WithdrawFromWallet(wallet.walletID, reservationToPayment.finalPrice);
                _centralReservationService.PostCentralReservation(centralReservationDTO);
                _reservationPaymentRepository.SaveReservationPayment(reservationPayment);

                _transaction.Commit();
            }
            catch (Exception ex)
            {
                _transaction.Rollback();
                throw;
            }
        }
    }
}
