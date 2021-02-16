using FluentValidation;
using PrivateParkAPI.DTO;

namespace PrivateParkAPI.Utils
{
    public class ReservationValidator : AbstractValidator<ReservationDTO>
    {

        public ReservationValidator()
        {
            RuleFor(ReservationDTO => ReservationDTO.startTime).NotEmpty();
            RuleFor(ReservationDTO => ReservationDTO.hours).GreaterThan(-1);
            RuleFor(ReservationDTO => ReservationDTO.parkingSpotID).NotEmpty();


        }


    }
}
