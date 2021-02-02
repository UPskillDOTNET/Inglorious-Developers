using FluentValidation;
using PrivateParkAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateParkAPI.Utils
{
    public class ParkingLotValidator: AbstractValidator<ParkingLotDTO>
    {
        public ParkingLotValidator()
        {
            RuleFor(ParkingLotDTO => ParkingLotDTO.name).MinimumLength(3).MaximumLength(24);
            RuleFor(ParkingLotDTO => ParkingLotDTO.companyOwner).MinimumLength(3).MaximumLength(24);
            RuleFor(ParkingLotDTO => ParkingLotDTO.location).MinimumLength(3);
            RuleFor(ParkingLotDTO => ParkingLotDTO.capacity).NotEmpty();
            RuleFor(ParkingLotDTO => ParkingLotDTO.openingTime).NotEmpty();
            RuleFor(ParkingLotDTO => ParkingLotDTO.closingTime).NotEmpty();
        }




      
    }
}
