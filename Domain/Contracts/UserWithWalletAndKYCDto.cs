﻿using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTOs.UserDTO
{
    public class UserWithWalletAndKYCDto
    {
        public string UserID { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UserName { get; set; }
        public string DocumentType { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string AccountNumber { get; set; }
        public string Status { get; set; }
        public decimal Balance { get; set; }
    }
}
