using CentralAPI.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.Services.Services
{
    public class DummyPaymentService : IPaymentService
    {
        public void PayOvertime(string reservationID, DateTime parkingEnd)
        {
        }

        public void PayReservation(string reservationID)
        {
        }
    }
}
