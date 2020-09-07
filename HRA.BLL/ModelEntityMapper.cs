using AutoMapper;
using HRA.Common.Models;

namespace HRA.BLL
{
    public class ModelEntityMapper: Profile
    {
        public ModelEntityMapper()
        {
            CreateMap<User, HRA.DAL.Entity.User>();
            CreateMap<Videos, HRA.DAL.Entity.Videos>();
            CreateMap<UserVideos, HRA.DAL.Entity.UserVideos>(); 
        }
    }
}
