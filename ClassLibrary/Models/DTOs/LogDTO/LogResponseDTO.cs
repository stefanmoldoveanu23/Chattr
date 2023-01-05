using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Models.DTOs.LogDTO
{
    public class LogResponseDTO
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }

        public Guid SenderId { get; set; }
        public String Message { get; set; } = "";

        public LogResponseDTO() { }

        public LogResponseDTO(LogRequestDTO logRequestDTO)
        {
            Id = new Guid(logRequestDTO.Id);
            DateCreated = logRequestDTO.DateCreated;
            SenderId = new Guid(logRequestDTO.SenderId);
            Message = logRequestDTO.Message;
        }
    }
}
