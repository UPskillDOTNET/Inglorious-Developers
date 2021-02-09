using AutoMapper;
using CentralAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.DTO
{
    public class Maps : Profile
    {
        public Maps()
        {
            CreateMap<Wallet, WalletDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Transaction, TransactionDTO>().ReverseMap();
            CreateMap<Sublet, SubletDTO>().ReverseMap();
        }
    }
}
