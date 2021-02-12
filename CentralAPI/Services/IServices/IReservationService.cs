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
        Task<ActionResult<CentralReservationDTO>> PostReservation(CentralReservationDTO CentralreservationDTO, int pLotID);
        Task<ActionResult<ReservationDTO>> PatchReservation(string id, int pLotID);
        ValidationResult Validate(ReservationDTO reservationDTO);        
    }
}
