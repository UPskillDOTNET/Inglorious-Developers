using CentralAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using PrivateParkAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.Services.IServices {
    public interface IReservationService {

        /*------------------------------------PRIVATE---------------------------------*/
        Task<ActionResult<IEnumerable<PrivateParkAPI.DTO.ReservationDTO>>> GetAllPrivateReservations();
        Task<ActionResult<IEnumerable<PrivateParkAPI.DTO.ReservationDTO>>> GetAllNotCanceledPrivateReservations();
        Task<ActionResult<PrivateParkAPI.DTO.ReservationDTO>> GetPrivateReservationById(string id);
        //Task<ActionResult<IEnumerable<PrivateParkAPI.DTO.ReservationDTO>>> GetReservationsNotCancelled();
        Task<ActionResult<PrivateParkAPI.DTO.ReservationDTO>> PostReservation(ReservationDTO reservationDTO);
        //Task<ActionResult<CentralReservationDTO>> PostUserReservation(ReservationDTO reservationDTO);
        Task<ActionResult<PrivateParkAPI.DTO.ReservationDTO>> GetEndTimeandFinalPrice(PrivateParkAPI.DTO.ReservationDTO reservationDTO);
        //Task<bool> FindCentralReservationAny(string id);
        Task<ActionResult<PrivateParkAPI.DTO.ReservationDTO>> PatchPrivateReservation(string id);
        //ValidationResult Validate(CentralReservationDTO centralReservationDTO);

        /*------------------------------------PUBLIC---------------------------------*/
        Task<ActionResult<IEnumerable<PublicParkAPI.DTO.ReservationDTO>>> GetAllPublicReservations();
        Task<ActionResult<IEnumerable<PublicParkAPI.DTO.ReservationDTO>>> GetAllNotCanceledPublicReservations();
        Task<ActionResult<PublicParkAPI.DTO.ReservationDTO>> GetPublicReservationById(string id);
        Task<ActionResult<PublicParkAPI.DTO.ReservationDTO>> PatchPublicReservation(string id);
    }
}
