using CentralAPI.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.Utils
{
    public class WalletValidator : AbstractValidator<WalletDTO>
    {
        public WalletValidator()
        { 
            RuleFor(WalletDTO => WalletDTO.totalAmount).GreaterThan(0);
            RuleFor(WalletDTO => WalletDTO.userID).NotEmpty();
        }
    }
}
