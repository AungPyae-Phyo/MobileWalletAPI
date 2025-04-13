using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts;
[Table("TransactionTable")]
public class Transaction:BaseEntity<string>
{
    public string SenderWalletId { get; set; } 

    public string ReceiverWalletId { get; set; }

    public string? TransactionType { get; set; } 

    public decimal Amount { get; set; }

}