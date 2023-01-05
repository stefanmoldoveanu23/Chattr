using Discord_Copycat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Models.DTOs.LogDTO
{
    public class LogRequestDTO
    {
        public String Id { get; set; } = "";
        public DateTime DateCreated { get; set; }

        public String SenderId { get; set; } = "";
        public String Message { get; set; } = "";
    }
}
