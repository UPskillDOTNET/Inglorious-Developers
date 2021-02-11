using AutoMapper;
using CentralAPI.DTO;
using CentralAPI.Models;
using CentralAPI.Repositories.IRepository;
using CentralAPI.Services.IServices;
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

        public WalletPaymentService(IMapper mapper, IReservationPaymentRepository reservationPaymentRepository)
        {
            _mapper = mapper;
            _reservationPaymentRepository = reservationPaymentRepository;
        }

        public void PayOvertime(string reservationID, DateTime parkingEnd)
        {
            throw new NotImplementedException();
        }

        public void PayReservation(ReservationDTO reservationDTO, ReservationPaymentDTO reservationPaymentDTO)
        {

            ReservationPaymentDTO reservationPaymentDTO1 = new ReservationPaymentDTO
            {
               reservationID = "1",
               value = 12.12m,
               userID = "1"
            };

            //var wallet = FindWallet(reservationPaymentDTO.userID)
            
            // verificar se a 
            //se deu ralha - Not sucessfull response
            //se deu certo

            //(reservationPaymentDTO1.value > Wallet.totalAmount) {
            //    return Conflict("Insufficient funds in wallet");
            //}

            //Create Reservation aqui

            var reservationPayment = _mapper.Map<ReservationPaymentDTO, ReservationPayment>(reservationPaymentDTO1);
            _reservationPaymentRepository.SaveReservationPayment(reservationPayment);
            //return {"Operation sucessfull" + paymentDTO}

      
        }
    }
}
