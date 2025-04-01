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
        public BankStatus? Status { get; set; }

    }
}

public class KYCDTOProfile : Profile
{
    public KYCDTOProfile()
    {
        // Map KYCDTO → KYC
        CreateMap<KYCDTO, KYC>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) // Ignore Id (set in service)
            .ForMember(dest => dest.CreatedOn, opt => opt.Ignore()) // Ignore CreatedOn (set in service)
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status ?? BankStatus.NEW_USER)); // Ensure default is set if null

        // Map KYC → KYCDTO (for GET responses)
        CreateMap<KYC, KYCDTO>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.HasValue ? src.Status : BankStatus.NEW_USER)); // Ensure default when returning
    }
}