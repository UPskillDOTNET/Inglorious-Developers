using FluentValidation;
using PrivateParkAPI.DTO;

namespace PrivateParkAPI.Utils
{
    public class ParkingSpotValidator : AbstractValidator<ParkingSpotDTO>
    {
        public ParkingSpotValidator()
        {
            RuleFor(ParkingSpotDTO => ParkingSpotDTO.priceHour).GreaterThan(0).LessThan(100);
        }
    }


}
