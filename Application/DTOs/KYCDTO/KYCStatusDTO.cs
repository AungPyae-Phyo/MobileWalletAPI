using AutoMapper;
using Domain.Enums;
using Domain.Contracts;
using System;
using System.Text.Json.Serialization;

namespace Application.DTOs.KYCDTO
{
    public class KYCStatusDTO
    {
        public string UserId { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public BankStatus Status { get; set; }
    }

    public class KYCStatusProfile : Profile
    {
        public KYCStatusProfile()
        {
            // Map KYC Entity → KYCStatusDTO (String to Enum using custom resolver)
            CreateMap<KYC, KYCStatusDTO>()
                .ForMember(dest => dest.Status, opt =>
                    opt.ConvertUsing(new StringToEnumConverter(), src => src.Status));

            // Map KYCStatusDTO → KYC Entity (Enum to String)
            CreateMap<KYCStatusDTO, KYC>()
                .ForMember(dest => dest.Status, opt =>
                    opt.MapFrom(src => src.Status.ToString()));
        }
    }

    // Custom Value Resolver to Convert String → Enum
    public class StringToEnumConverter : IValueConverter<string, BankStatus>
    {
        public BankStatus Convert(string sourceMember, ResolutionContext context)
        {
            return Enum.TryParse<BankStatus>(sourceMember, true, out var status) ? status : BankStatus.NEW_USER;
        }
    }
}
