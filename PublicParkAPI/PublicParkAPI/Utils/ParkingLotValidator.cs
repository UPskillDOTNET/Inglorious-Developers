﻿using FluentValidation;
using PublicParkAPI.DTO;

namespace PublicParkAPI.Utils
{
    public class ParkingLotValidator : AbstractValidator<ParkingLotDTO>
    {
        public ParkingLotValidator()
        {
            RuleFor(ParkingLotDTO => ParkingLotDTO.name).MinimumLength(3).MaximumLength(24);
            RuleFor(ParkingLotDTO => ParkingLotDTO.municipality).MinimumLength(3).MaximumLength(24);
            RuleFor(ParkingLotDTO => ParkingLotDTO.location).MinimumLength(3);
            RuleFor(ParkingLotDTO => ParkingLotDTO.capacity).NotEmpty();
            RuleFor(ParkingLotDTO => ParkingLotDTO.openingTime).NotEmpty();
            RuleFor(ParkingLotDTO => ParkingLotDTO.closingTime).NotEmpty();
        }





    }
}
