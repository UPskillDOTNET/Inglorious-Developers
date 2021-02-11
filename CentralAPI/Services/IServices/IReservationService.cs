using CentralAPI.DTO;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using PrivateParkAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.Services.IServices {
    public interface IReservationService {

        /*------------------------------------PRIVATE---------------------------------*/
        Task<ActionResult<IEnumerable<ReservationDTO>>> GetAllReservations(int pLotID);
        Task<ActionResult<IEnumerable<ReservationDTO>>> GetAllNotCanceledReservations(int pLotID);
        Task<ActionResult<ReservationDTO>> GetReservationById(string id, int pLotID);        
        Task<ActionResult<ReservationDTO>> PostReservation(ReservationDTO reservationDTO, int pLotID);
        Task<ActionResult<CentralReservationDTO>> PostUserReservation([FromBody] CentralReservationDTO reservationDTO, int pLotID);
        Task<ActionResult<ReservationDTO>> GetEndTimeAndFinalPrice(ReservationDTO reservationDTO, int id);        
        Task<ActionResult<ReservationDTO>> PatchReservation(string id, int pLotID);
        ValidationResult Validate(ReservationDTO reservationDTO);        
    }
}
