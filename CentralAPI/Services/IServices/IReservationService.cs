using CentralAPI.DTO;
using CentralAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CentralAPI.Services.IServices
{
    public interface IReservationService
    {

        /*------------------------------------PRIVATE---------------------------------*/
        Task<ActionResult<IEnumerable<CentralReservationDTO>>> GetAllReservations(int pLotID);
        Task<ActionResult<IEnumerable<CentralReservationDTO>>> GetAllNotCanceledReservations(int pLotID);
        Task<ActionResult<CentralReservationDTO>> GetReservationById(string id, int pLotID);
        Task<ActionResult<HttpResponseMessage>> PostReservation(CentralReservation Centralreservation, int pLotID);
        Task<ActionResult<HttpResponseMessage>> PatchReservation(string id, int pLotID);
        Task<ActionResult<HttpResponseMessage>> completeReservation(CentralReservationDTO centralReservationDTO);
    }
}
