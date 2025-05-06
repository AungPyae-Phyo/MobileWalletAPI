using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.TransactionDTO
{
    public class TransactionHistoryDto
    {
        public string Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string SenderName { get; set; }
        public string ReceiverName { get; set; }
        public decimal Amount { get; set; }
        public string TransactionType { get; set; }
        public string Description { get; set; }
    }
}
