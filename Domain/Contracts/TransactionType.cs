using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts;
[Table("Transaction_Type")]
public class TransactionType:BaseEntity<string>
{
    public string? Type { get; set; }
    public string? Description { get; set; }
}