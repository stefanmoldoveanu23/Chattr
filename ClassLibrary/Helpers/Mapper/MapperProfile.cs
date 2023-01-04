using AutoMapper;
using ClassLibrary.Models.DTOs.ChatDTO;
using ClassLibrary.Models.DTOs.ServerDTO;
using ClassLibrary.Models.DTOs.UserDTO;
using Discord_Copycat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Helpers.Mapper
{
    internal class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<UserRequestDTO, User>();

            CreateMap<ServerRequestDTO, Server>();
            CreateMap<Server, ServerResponseDTO>();

            CreateMap<ChatRequestDTO, Chat>();
            CreateMap<Chat, ChatResponseDTO>();
        }
    }
}
