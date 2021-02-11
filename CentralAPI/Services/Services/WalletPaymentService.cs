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
      
            // Mapear a reserva para um pagamento
            var reservationToPayment = _mapper.Map<CentralReservationDTO, ReservationPaymentDTO>(centralReservationDTO);

            // Buscar a Wallet
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
            await _walletService.WithdrawFromWallet(wallet.walletID, reservationToPayment.finalPrice);

            // Está a deixar passar, mesmo que o saldo seja 
            await _centralReservationService.PostCentralReservation(centralReservationDTO);

            var reservationPayment = _mapper.Map<ReservationPaymentDTO, ReservationPayment>(reservationToPayment);

            await _reservationPaymentRepository.SaveReservationPayment(reservationPayment);

            ReservationPaymentDTOOperation reservationPaymentDTOOperation = new ReservationPaymentDTOOperation
            {
                reservationID = reservationPayment.reservationID,
                finalPrice = reservationPayment.finalPrice,
                userID = reservationPayment.userID,
                message = "Operation done",
                isSuccess = true,
            };

            //QR Code com os dados da Reserva - check

            //Email enviado para o user

            //Fim do flow
            return reservationPaymentDTOOperation;
        }
    }
}
