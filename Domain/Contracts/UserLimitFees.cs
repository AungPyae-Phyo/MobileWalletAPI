using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts;
[Table("User_Limit_Fees")]
public class UserLimitFees:BaseEntity<string>
{
    public string? UserId { get; set; }
    public string? LimitId { get; set; }


}