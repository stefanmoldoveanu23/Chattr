using AutoMapper;
using ClassLibrary.Models.DTOs;
using ClassLibrary.Models.DTOs.ChatDTO;
using ClassLibrary.Models.DTOs.LogDTO;
using ClassLibrary.Models.DTOs.ServerDTO;
using ClassLibrary.Models.DTOs.UserDTO;
using ClassLibrary.Models;
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
            CreateMap<UserRequestDTO, User>(MemberList.Source);

            CreateMap<ServerRequestDTO, Server>(MemberList.Source);
            CreateMap<Server, ServerResponseDTO>(MemberList.Destination);

            CreateMap<ChatRequestDTO, Chat>(MemberList.Source);
            CreateMap<Chat, ChatResponseDTO>(MemberList.Destination);

            CreateMap<FriendLog, LogResponseDTO>(MemberList.Destination);
            CreateMap<ChatLog, LogResponseDTO>(MemberList.Destination);
        }
    }
}
