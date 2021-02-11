using FluentValidation;
using PrivateParkAPI.DTO;

namespace CentralAPI.Utils {
    public class ReservationValidator : AbstractValidator<ReservationDTO> {

        public ReservationValidator() {
            RuleFor(ReservationDTO => ReservationDTO.startTime).NotEmpty();
            RuleFor(ReservationDTO => ReservationDTO.hours).GreaterThan(0);
            RuleFor(ReservationDTO => ReservationDTO.parkingSpotID).NotEmpty();
        }
    }
}


