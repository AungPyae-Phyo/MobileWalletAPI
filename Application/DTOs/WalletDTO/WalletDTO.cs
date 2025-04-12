using Application.DTOs.WalletDTO;
using AutoMapper;
using Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.WalletDTO
{
    public class WalletDTO
    {
        public string? WalletId { get; set; }
        public decimal Balance { get; set; }
    }
}
public class WalletMappingProfile : Profile
{
    public WalletMappingProfile()
    {
        CreateMap<Wallet, WalletDTO>().ReverseMap();
    }
}