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
        Task<ActionResult<IEnumerable<PrivateParkAPI.DTO.ReservationDTO>>> GetAllReservations(int pLotID);
        Task<ActionResult<IEnumerable<PrivateParkAPI.DTO.ReservationDTO>>> GetAllNotCanceledReservations(int pLotID);
        Task<ActionResult<PrivateParkAPI.DTO.ReservationDTO>> GetReservationById(string id, int pLotID);        
        Task<ActionResult<PrivateParkAPI.DTO.ReservationDTO>> PostReservation(ReservationDTO reservationDTO, int pLotID);
        Task<ActionResult<CentralReservationDTO>> PostUserReservation([FromBody] CentralReservationDTO reservationDTO, int pLotID);
        Task<ActionResult<PrivateParkAPI.DTO.ReservationDTO>> GetEndTimeAndFinalPrice(PrivateParkAPI.DTO.ReservationDTO reservationDTO);        
        Task<ActionResult<PrivateParkAPI.DTO.ReservationDTO>> PatchReservation(string id, int pLotID);
        //ValidationResult Validate(CentralReservationDTO centralReservationDTO);        
    }
}
