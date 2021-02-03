using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using PrivateParkAPI.DTO;

namespace PrivateParkAPI.Utils
{
    public class ParkingSpotValidator : AbstractValidator<ParkingSpotDTO>
    {
        public ParkingSpotValidator()
        {
            RuleFor(ParkingSpotDTO => ParkingSpotDTO.priceHour).GreaterThan(0).LessThan(100);
            RuleFor(ParkingSpotDTO => ParkingSpotDTO.parkingLotID).NotEmpty();
        }
    }
    
    
}
