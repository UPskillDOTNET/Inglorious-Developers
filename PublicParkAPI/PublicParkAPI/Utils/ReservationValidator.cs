<<<<<<< HEAD
﻿using FluentValidation;
using PublicParkAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicParkAPI.Utils
{
    public class ReservationValidator : AbstractValidator<ReservationDTO>
    {
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using PublicParkAPI.DTO;

namespace PublicParkAPI.Utils
{
    public class ReservationValidator : AbstractValidator<ReservationDTO> 
    {

>>>>>>> c6bd7685ce42d6279ba3d7412fcbb0d0779b1b3c
        public ReservationValidator()
        {
            RuleFor(ReservationDTO => ReservationDTO.startTime).NotEmpty();
            RuleFor(ReservationDTO => ReservationDTO.hours).GreaterThan(0);
            RuleFor(ReservationDTO => ReservationDTO.parkingSpotID).NotEmpty();
        }


    }
}
<<<<<<< HEAD
=======
    
>>>>>>> c6bd7685ce42d6279ba3d7412fcbb0d0779b1b3c
