﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.UserDTO
{
    public class UserLoginDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string HashPassword { get; set; }

    }
}

