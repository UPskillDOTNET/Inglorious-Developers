using CentralAPI.DTO;
using CentralAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.Services.IServices
{
    public interface ISubletService
    {
        Task<ActionResult<IEnumerable<SubletDTO>>> GetSublets();
        Task<ActionResult<IEnumerable<SubletDTO>>> GetSubletsByMainUserId(string id);
        Task<ActionResult<IEnumerable<SubletDTO>>> GetSubletsBySubUserId(string id);
        Task<ActionResult<IEnumerable<SubletDTO>>> GetActiveSublets();
        Task<ActionResult<IEnumerable<SubletDTO>>> GetSubletsbyDate(DateTime startDate, DateTime endDate);
        Task<ActionResult<SubletDTO>> GetSublet(string id);
        Task<ActionResult<SubletDTO>> CreateSublet(SubletDTO subletDTO);
        Task<ActionResult<SubletDTO>> CancelSublet(string id);
    }
}
