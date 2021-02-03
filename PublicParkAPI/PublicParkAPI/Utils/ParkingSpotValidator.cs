using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using PublicParkAPI.DTO;

namespace PublicParkAPI.Utils
{
    public class ParkingSpotValidator : AbstractValidator<ParkingSpotDTO>
    {
        public ParkingSpotValidator()
        {
            RuleFor(ParkingSpotDTO => ParkingSpotDTO.priceHour).GreaterThan(0).LessThan(100);
            RuleFor(ParkingSpotDTO => ParkingSpotDTO.ParkingLotID).NotEmpty();
        }
    }    
    
}
