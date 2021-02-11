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
    public class WalletPaymentService : IPaymentService
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

        //public Task<ActionResult<ReservationPaymentDTOOperation>> PayReservation(CentralReservationDTO centralReservationDTO)
        //{

        //    CentralReservationDTO centralReservationTest = new CentralReservationDTO
        //    {
        //        reservationID = "1",
        //        startTime = DateTime.Parse("2021-10-22 07:00:00"),
        //        hours = 2,
        //        finalPrice = 12.2m,
        //        userID = "1"
        //    };

        //    // Mapear a reserva para um pagamento
        //    var reservationToPayment = _mapper.Map<CentralReservationDTO, ReservationPaymentDTO>(centralReservationTest);

        //    // Buscar a Wallet
        //    var wallet = _walletService.GetWalletById(reservationToPayment.userID).Result;
        //    var x = wallet.Value;

        //    var walletDTOOperation = _walletService.WithdrawFromWallet(x.walletID, reservationToPayment.finalPrice).Result.Value;

        //    if (!walletDTOOperation.isSuccess)
        //    {
        //        wa
        //    }
        //    _centralReservationService.PostCentralReservation(centralReservationTest);

        //    //QR Code com os dados da Reserva

        //    //Email enviado para o client

        //    //Fim do flow
        //    return reservationToPayment;
        //}
    }
}
