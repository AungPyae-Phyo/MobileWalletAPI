using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts;
[Table("QR")]
public class QR:BaseEntity<string>
{
    public string? UserID { get; set; }
    public string? WalletID { get; set; }
    public string? QRCode { get; set; }
}