using AutoMapper;
using CentralAPI.DTO;
using CentralAPI.Models;
using CentralAPI.Repositories.IRepository;
using CentralAPI.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.Services.Services
{
    public class SubletService:ISubletService
    {
        private readonly ISubletRepository _subletRepository;
        private readonly IMapper _mapper;

        public async Task<ActionResult<IEnumerable<SubletDTO>>> GetSublets()
        {
            var sublets = await _subletRepository.GetSublets();
            var subletDTOs = _mapper.Map<List<Sublet>, List<SubletDTO>>(sublets.ToList());
            return subletDTOs;
        }
        public async Task<ActionResult<IEnumerable<SubletDTO>>> GetSubletsByMainUserId(string id)
        {
            var sublets = await _subletRepository.GetSubletsByMainUserId(id);
            var subletDTOs = _mapper.Map<List<Sublet>, List<SubletDTO>>(sublets.ToList());
            return subletDTOs;
        }
        public async Task<ActionResult<IEnumerable<SubletDTO>>> GetSubletsBySubUserId(string id)
        {
            var sublets = await _subletRepository.GetSubletsBySubUserId(id);
            var subletDTOs = _mapper.Map<List<Sublet>, List<SubletDTO>>(sublets.ToList());
            return subletDTOs;
        }
        public async Task<ActionResult<IEnumerable<SubletDTO>>> GetActiveSublets()
        {
            var sublets = await _subletRepository.GetActiveSublets();
            var subletDTOs = _mapper.Map<List<Sublet>, List<SubletDTO>>(sublets.ToList());
            return subletDTOs;
        }
        public async Task<ActionResult<IEnumerable<SubletDTO>>> GetSubletsbyDate(DateTime startDate, DateTime endDate)
        {
            var sublets = await _subletRepository.GetActiveSublets();
            var subletDTOs = _mapper.Map<List<Sublet>, List<SubletDTO>>(sublets.ToList());
            return subletDTOs;
        }
        public async Task<ActionResult<SubletDTO>> GetSublet(string id)
        {
            var sublet = await _subletRepository.GetSublet(id);
            var subletDTO = _mapper.Map<Sublet, SubletDTO>(sublet);
            return subletDTO;

        }
        public async Task<ActionResult<SubletDTO>> CreateSublet(SubletDTO subletDTO)
        {
            var sublet = _mapper.Map<SubletDTO , Sublet>(subletDTO);
            
            await _subletRepository.CreateSublet(sublet);

            return subletDTO;

        }
        public async Task<ActionResult<SubletDTO>> CancelSublet(string id)
        {
            var sublet= await _subletRepository.Find(id);
            sublet.isCancelled = true;
            var subletReturn = await _subletRepository.CancelSublet(sublet);
            var subletDTOReturn = _mapper.Map<Sublet, SubletDTO>(subletReturn);
            return subletDTOReturn;
        }
    }
}
