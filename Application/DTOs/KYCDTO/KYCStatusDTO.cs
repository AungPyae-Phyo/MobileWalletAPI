using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTOs.KYCDTO
{
    public class KYCStatusDTO
    {
        public string UserId { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public BankStatus Status { get; set; } // Ensure this matches the expected enum
    }
}
