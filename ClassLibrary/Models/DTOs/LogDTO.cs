using Discord_Copycat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Models.DTOs
{
    public class LogDTO
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }

        public Guid SenderId { get; set; }
        public String Message { get; set; } = "";
    }
}
