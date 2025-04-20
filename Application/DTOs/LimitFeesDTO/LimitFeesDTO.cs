using Application.DTOs.LimitFeesDTO;
using AutoMapper;
using Domain.Contracts;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTOs.LimitFeesDTO
{
    public class LimitFeesDTO
    {
     //   public string? limit_fees_id { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TransactionType? TransactionType { get; set; }
        public string? description { get; set; }
        public decimal? min_amount { get; set; }
        public decimal? max_amount { get; set; }
        public decimal? percent_fees { get; set; }
    }
}

public class LimitFeesProfile : Profile
{
    public LimitFeesProfile()
    {
        // Map LimitFees <-> LimitFeesDTO
        CreateMap<LimitFees, LimitFeesDTO>().ReverseMap();
    }
}