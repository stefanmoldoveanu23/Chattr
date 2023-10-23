using ClassLibrary.Models.DTOs.ServerDTO;
using ClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Helpers.Utils
{
    public interface IJwtUtils
    {
        public String GenerateJwtToken(User user);
        public String GenerateJwtToken(ServerResponseDTO server);

        public Guid ValidateUserJwtToken(String token);
        public Guid ValidateServerJwtToken(String token);
    }
}
