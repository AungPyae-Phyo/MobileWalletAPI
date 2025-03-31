using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts;
[Table("Wallet")]
public class Wallet : BaseEntity<string>
{
    public string? UserId { get; set; }
    public decimal? Balance { get; set; }

}
