using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts;
[Table("Limit_Fees")]
public class LimitFees : BaseEntity<string>
{
    public string? TransactionType { get; set; }
    public string? description { get; set; }
    public decimal? min_amount { get; set; }
    public decimal? max_amount { get; set; }
    public decimal? percent_fees { get; set; }
}