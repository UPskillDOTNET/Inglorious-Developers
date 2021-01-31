using Microsoft.AspNetCore.Mvc;
using PublicParkAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicParkAPI.Contracts
{
    public interface IParkingLotRepository : IBaseRepository<ParkingLot>
    {
        IEnumerable<ParkingLot> GetParkingLots();
        ParkingLot GetParkingLot(int id);
        bool PutParkingLot(int id, ParkingLot parkingLot);
        bool PostParkingLot(ParkingLot parkingLot);
        bool DeleteParkingLot(int id);
        bool ParkingLotExists(int id);
    }
}
