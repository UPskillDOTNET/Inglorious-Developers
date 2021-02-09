using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CentralAPI.Models;
using CentralAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CentralAPI.Services.IServices {
    public interface ICentralReservationService {
        Task<ActionResult<IEnumerable<CentralReservationDTO>>> GetAllCentralReservations();
        Task<ActionResult<CentralReservationDTO>> GetCentralReservationById(string id);
        Task<ActionResult<IEnumerable<CentralReservationDTO>>> GetCentralReservationsNotCancelled();
        Task<ActionResult<CentralReservationDTO>> PostCentralReservation(CentralReservationDTO centralReservationDTO);
        Task<ActionResult<CentralReservationDTO>> GetEndTimeandFinalPrice(CentralReservationDTO centralReservation);
        Task<bool> FindCentralReservationAny(string id);
        Task<ActionResult<CentralReservation>> PatchCentralReservation(string id);
        //ValidationResult Validate(CentralReservationDTO centralReservationDTO);
    }
}
