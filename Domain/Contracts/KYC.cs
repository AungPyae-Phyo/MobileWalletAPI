using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Contracts;
[Table("KYC")]
public class KYC : BaseEntity<string>
{
    public string? UserID { get; set; }
    public string? DocumentType{ get; set; }
    public string? FrontImage { get; set; }
    public string? BackImage { get; set; }
    public string? SelfieImage { get; set; }
    public string? Status { get; set; }
}   