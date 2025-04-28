using Application.DTOs.KYCDTO;
using AutoMapper;
using Domain.Contracts;
using Domain.Enums;
using System.Text.Json.Serialization;

namespace Application.DTOs.KYCDTO
{
    public class KYCDTO
    {
        public string? UserID { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DocumentType? DocumentType { get; set; }
        public string? Identity { get; set; }
        public string? Address { get; set; }
        public string? FrontImage { get; set; }
        public string? BackImage { get; set; }
        public string? SelfieImage { get; set; }
        public DateTime? DOB { get; set; }
        public string? FatherName { get; set; }
 
      //  public KYCStatus? Status { get; set; }

    }
}
public class KYCProfile : Profile
{
    public KYCProfile()
    {
        CreateMap<KYCDTO, KYC>()
          //  .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.HasValue ? src.Status.ToString() : null))
            .ForMember(dest => dest.DocumentType, opt => opt.MapFrom(src => src.DocumentType.HasValue ? src.DocumentType.ToString() : null));

        CreateMap<KYC, KYCDTO>()
          //  .ForMember(dest => dest.Status, opt => opt.ConvertUsing(new StringToEnumConverter<KYCStatus>(), src => src.Status))
            .ForMember(dest => dest.DocumentType, opt => opt.ConvertUsing(new StringToEnumConverter<DocumentType>(), src => src.DocumentType));
    }
}

public class StringToEnumConverter<TEnum> : IValueConverter<string, TEnum?>
    where TEnum : struct, Enum
{
    public TEnum? Convert(string sourceMember, ResolutionContext context)
    {
        return Enum.TryParse<TEnum>(sourceMember, true, out var result) ? result : (TEnum?)null;
    }
}
