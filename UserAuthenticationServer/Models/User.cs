using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.Models
{

    [Index(nameof(nif), IsUnique = true)]
    public class User : IdentityUser
    {
 
        public override string UserName { get; set; }
        [Required]
        [RegularExpression(@"^([a-zA-Z]{2,}\\s[a-zA-Z]{1,}'?-?[a-zA-Z]{2,}\\s?([a-zA-Z]{1,})?)", ErrorMessage = "Valid Characters include (A-Z) (a-z) (', space and -)")]
        public string name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public override string Email { get; set; }

        [Required]
        [RegularExpression(@"^\\d{9}$", ErrorMessage = "NIF length must be 9 numbers")]
        public string nif { get; set; }

        [ForeignKey("PaymentMethod")]
        public string paymentMethodID { get; set; }
    }
}