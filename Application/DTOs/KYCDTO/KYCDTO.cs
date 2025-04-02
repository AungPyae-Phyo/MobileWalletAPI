using Application.DTOs.KYCDTO;
using AutoMapper;
using Domain.Contracts;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
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
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public BankStatus? Status { get; set; }

    }
}
public class KYCProfile : Profile
{
    public KYCProfile()
    {
        CreateMap<KYCDTO, KYC>()
            .ForMember(dest => dest.Status, opt =>
                opt.MapFrom(src => src.Status.HasValue ? src.Status.ToString() : null));
        // Convert Enum → String (Handles Null)

        CreateMap<KYC, KYCDTO>()
            .ForMember(dest => dest.Status, opt =>
                opt.ConvertUsing(new StringToEnumConverter(), src => src.Status));
        // Convert String → Enum using a custom converter
    }
}

public class StringToEnumConverter : IValueConverter<string, BankStatus?>
{
    public BankStatus? Convert(string sourceMember, ResolutionContext context)
    {
        return Enum.TryParse<BankStatus>(sourceMember, true, out var result) ? result : (BankStatus?)null;
    }
}