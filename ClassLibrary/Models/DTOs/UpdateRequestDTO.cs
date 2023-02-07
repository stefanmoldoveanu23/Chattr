using ClassLibrary.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Models.DTOs
{
    internal class UpdateRequestDTO
    {
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public string Email { get; set; } = "";
        public Themes Theme { get; set; }
        public bool Notifs { get; set; }
        public bool Appearance { get; set; }
    }
}
