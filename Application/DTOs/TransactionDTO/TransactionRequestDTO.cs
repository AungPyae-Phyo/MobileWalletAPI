using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTOs.TransactionDTO
{
    public class TransactionRequestDTO
    {
        public string SenderWalletId { get; set; }

        public string ReceiverAccountNumber { get; set; } 

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TransactionType? TransactionType { get; set; }

        public decimal Amount { get; set; }
    }
}
