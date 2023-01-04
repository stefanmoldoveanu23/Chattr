using Discord_Copycat.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord_Copycat.Models;

namespace ClassLibrary.Helpers.Seeders
{
    public class UserSeeder
    {
        public readonly DiscordContext _discordContext;

        public UserSeeder(DiscordContext discordContext)
        {
            _discordContext = discordContext;
        }

        public void SeedAdmin()
        {
            if (!_discordContext.Users.Any())
            {
                User User = new User
                {
                    Username = "admin",
                    Password = "extraword12",
                    Email = "characterme1001@gmail.com",
                    Role = Discord_Copycat.Models.Enums.Roles.Admin,
                };

                _discordContext.Add(User);
                _discordContext.SaveChanges();
            }
        }
    }
}
