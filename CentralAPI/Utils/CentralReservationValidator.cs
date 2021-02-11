using FluentValidation;
using CentralAPI.DTO;

namespace CentralAPI.Utils {
    public class CentralReservationValidator : AbstractValidator<CentralReservationDTO> {

        public CentralReservationValidator() {
            RuleFor(CentralReservationDTO => CentralReservationDTO.startTime).NotEmpty();
            //RuleFor(CentralReservationDTO => CentralReservationDTO.hours).GreaterThan(0);
            RuleFor(CentralReservationDTO => CentralReservationDTO.parkingLotID).NotEmpty();
        }
    }
}
