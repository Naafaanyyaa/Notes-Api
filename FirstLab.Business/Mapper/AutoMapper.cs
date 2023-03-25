using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FirstLab.Business.Models.Request;
using FirstLab.Business.Models.Response;
using FirstLab.Data.Models;

namespace FirstLab.Business.Mapper
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<RegisterRequest, User>();
            CreateMap<User, RegisterResponse>();
            CreateMap<User, UserResponse>();
            CreateMap<NoteRequest, Note>();
            CreateMap<ChangePasswordRequest, User>()
                .ForMember(x => x.Password, x => x.MapFrom(s => s.NewPassword));
            CreateMap<NoteEditRequest, Note>();
        }
    }
}
