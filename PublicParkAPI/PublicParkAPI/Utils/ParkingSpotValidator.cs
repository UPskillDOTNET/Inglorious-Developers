using FluentValidation;
using PublicParkAPI.DTO;

namespace PublicParkAPI.Utils
{
    public class ParkingSpotValidator : AbstractValidator<ParkingSpotDTO>
    {
        public ParkingSpotValidator()
        {
            RuleFor(ParkingSpotDTO => ParkingSpotDTO.priceHour).GreaterThan(0).LessThan(100);
        }
    }

}
