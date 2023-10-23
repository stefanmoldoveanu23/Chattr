﻿using ClassLibrary.Models;
using ClassLibrary.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Models.DTOs.UserDTO
{
    public class UserResponseDTO
    {
        public Guid Id { get; set; }
        public String Username { get; set; } = "";
        public String Email { get; set; } = "";
        public Roles Role { get; set; }
        public String Token { get; set; } = "";

        public UserResponseDTO(User user, String token = "")
        {
            Id = user.Id;
            Username = user.Username;
            Email = user.Email;
            Role = user.Role;
            Token = token;
        }
    }
}
