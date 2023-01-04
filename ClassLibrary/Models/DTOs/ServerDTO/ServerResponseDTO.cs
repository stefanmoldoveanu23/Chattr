using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Models.DTOs.ServerDTO
{
    internal class ServerResponseDTO
    {
        public Guid Id { get; set; }
        public String Name { get; set; } = "";
        public String Description { get; set; } = "";
    }
}
