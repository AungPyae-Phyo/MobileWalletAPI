using Application.DTOs.KYCDTO;
using AutoMapper;
using Domain.Contracts;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.KYCDTO
{
    public class KYCDTO
    {
        public string? UserID { get; set; }
        public string? DocumentType { get; set; }
        public string? FrontImage { get; set; }
        public string? BackImage { get; set; }
        public string? SelfieImage { get; set; }
        public string? Status { get; set; } = Enum.GetName(typeof(BankStatus), BankStatus.NEW_USER);

    }
}

public class KYCDTOProfile : Profile
{
    public KYCDTOProfile()
    {
        // Map KYCDTO → KYC
        CreateMap<KYCDTO, KYC>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) // Ignore Id (it will be generated in service)
            .ForMember(dest => dest.CreatedOn, opt => opt.Ignore()) // Ignore CreatedOn (set in service)
            .ForMember(dest => dest.Status, opt => opt.Ignore()); // Ignore Status (set in service)

        // Map KYC → KYCDTO (if needed)
        CreateMap<KYC, KYCDTO>();
    }
}