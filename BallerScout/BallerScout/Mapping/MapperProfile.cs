using AutoMapper;
using BallerScout.Entities;
using BallerScout.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BallerScout.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Post, PostModel>();
            CreateMap<PostModel, Post>();
            CreateMap<Game, GameModel>();
            CreateMap<GameModel, Game>();
            CreateMap<Skills, SkillsModel>();
            CreateMap<SkillsModel, Skills>();
            CreateMap<ApplicationUser, ApplicationUserModel>();
            CreateMap<ApplicationUserModel, ApplicationUser>();
        }
    }
}
