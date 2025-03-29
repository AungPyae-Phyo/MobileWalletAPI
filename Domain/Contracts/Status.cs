using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts;
[Table("Status")]
public class Status: BaseEntity<string>
{
    public string? description { get; set; }
    public BankStatus StatusName { get; set; }
}
