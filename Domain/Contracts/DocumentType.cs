using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts;
[Table("Document_Type")]
public class DocumentType : BaseEntity<string>
{
    public string? description { get; set; }

}