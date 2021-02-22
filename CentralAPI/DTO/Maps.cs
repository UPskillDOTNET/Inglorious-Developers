using AutoMapper;
using CentralAPI.Models;

namespace CentralAPI.DTO
{
    public class Maps : Profile
    {
        public Maps()
        {
            CreateMap<ParkingLot, ParkingLotDTO>().ReverseMap();
            CreateMap<PrivateParkAPI.Models.Reservation, PrivateParkAPI.DTO.ReservationDTO>().ReverseMap();
            CreateMap<CentralReservation, CentralReservationDTO>().ReverseMap();
            CreateMap<PrivateParkAPI.Models.Reservation, PrivateParkAPI.DTO.ReservationDTO>().ReverseMap();
            CreateMap<Wallet, WalletDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Sublet, SubletDTO>().ReverseMap();
            CreateMap<Sublet, CentralReservationDTO>().ReverseMap();
            CreateMap<ReservationPayment, ReservationPaymentDTO>().ReverseMap();
            CreateMap<CentralReservationDTO, ReservationPaymentDTO>().ReverseMap();
            CreateMap<Wallet, WalletDTOOperation>().ReverseMap();
            CreateMap<Payment, PaymentDTO>().ReverseMap();
            CreateMap<PaymentDTO, ReservationPayment>().ReverseMap();
        }
    }
}



