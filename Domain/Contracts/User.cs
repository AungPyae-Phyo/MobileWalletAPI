using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    [Table("Users")]
    public class User : BaseEntity<string>
    {
        public string? PhoneNumber { get; set; } = default!;

        public string? HashPassword { get; set; } = default!;

        public string? Email { get; set; } = default!;

        public decimal? Balance { get; set; } = default!;

        public string? RefreshToken { get; set; }

        public string? Role { get; set; } = Enum.GetName(typeof(Role), Domain.Enums.Role.None);

        public DateTime? RefreshTokenExpiry { get; set; } = DateTime.UtcNow;
    }
}
