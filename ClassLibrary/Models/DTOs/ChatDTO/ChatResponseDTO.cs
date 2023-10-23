﻿using ClassLibrary.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Models.DTOs.ChatDTO
{
    public class ChatResponseDTO
    {
        public Guid Id { get; set; }
        public String Name { get; set; } = "General";
        public Roles Role { get; set; }
    }
}
