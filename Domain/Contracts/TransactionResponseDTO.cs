using Application.DTOs.TransactionDTO;
using Domain.Contracts;
using Domain.Enums;
using System.Text.Json.Serialization;


namespace Application.DTOs.TransactionDTO
{
   public class TransactionResponseDTO
    {
        public string Id { get; set; }

        public string SenderName { get; set; }

        public string ReceiverAccountNumber { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TransactionType? TransactionType { get; set; }

        public decimal Amount { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime LastModifiedOn { get; set; }
    }
}
