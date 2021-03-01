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
        Task<ActionResult<CentralReservationDTO>> GetCentralReservationByUserId(string userID);
        Task<ActionResult<IEnumerable<CentralReservationDTO>>> GetCentralReservationsNotCancelled();
        Task<ActionResult<CentralReservationDTO>> PostCentralReservation(CentralReservationDTO centralReservationDTO);
        Task<ActionResult<CentralReservationDTO>> PostCentralReservationNotCompleted(CentralReservationDTO centralReservationDTO);
        Task<ActionResult<CentralReservationDTO>> GetEndTimeandFinalPrice(CentralReservationDTO centralReservation);
        Task<bool> FindCentralReservationAny(string id);
        Task<bool> FindCentralReservationAnyByUser(string userID);
        Task<bool> subletReservationExists(CentralReservationDTO centralReservationDTO);
        Task<ActionResult<CentralReservationDTO>> PatchCentralReservation(string id);
        Task<ActionResult<CentralReservationDTO>> CompleteCentralReservation(string id);
        Task<ActionResult<CentralReservationDTO>> SubletCentralReservation(string id);
        Task<ActionResult<CentralReservation>> GetEndTimeandFinalPriceForComplete(CentralReservation centralReservation);

        //ValidationResult Validate(CentralReservationDTO centralReservationDTO);
    }
}
