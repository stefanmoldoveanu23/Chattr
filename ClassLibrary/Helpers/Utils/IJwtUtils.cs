using Discord_Copycat.Models;
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

        public Guid ValidateJwtToken(String token);
    }
}
