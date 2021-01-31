using PublicParkAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicParkAPI.Services.IServices
{
    public interface IParkingLotService
    {
        Task<IEnumerable<ParkingLotDTO>> GetParkingLots();
    }
}
