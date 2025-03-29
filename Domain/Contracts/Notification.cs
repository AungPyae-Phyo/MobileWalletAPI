using Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Contracts;
[Table("Notification")]
public class Notification : BaseEntity<string>
{

}