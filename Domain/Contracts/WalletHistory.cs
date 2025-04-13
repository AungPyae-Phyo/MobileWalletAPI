using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    [Table("WalletHistory")]
    public class WalletHistory : BaseEntity<string>
    {
        public string? WalletId { get; set; }
        public decimal ChangeAmount { get; set; }
        public decimal PreviousBalance { get; set; }
        public decimal NewBalance { get; set; }
    }
}