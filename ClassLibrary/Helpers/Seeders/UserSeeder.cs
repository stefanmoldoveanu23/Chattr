using Chattr.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary.Models;
using ClassLibrary.Helpers.UOW;
using BCryptNet = BCrypt.Net.BCrypt;

namespace ClassLibrary.Helpers.Seeders
{
    public class UserSeeder
    {
        public readonly IUnitOfWork _unitOfWork;

        public UserSeeder(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SeedAdmin()
        {
            if (!_unitOfWork._userRepository.GetAllAsQueryable().Any())
            {
                User User = new User
                {
                    Username = "admin",
                    Password = BCryptNet.HashPassword("extraword12"),
                    Email = "characterme1001@gmail.com",
                    Role = ClassLibrary.Models.Enums.Roles.Admin,
                };

                _unitOfWork._userRepository.CreateAsync(User);
                _unitOfWork.SaveAsync();
            }
        }
    }
}
