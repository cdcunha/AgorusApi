using AgorusApi.Dto;
using AgorusApi.Model;
using AutoMapper;

namespace AgorusApi.Profiles
{
    public class FileProfile : Profile
    {
        public FileProfile()
        {
            CreateMap<FileDto, FileModel>().ReverseMap();
            CreateMap<FileHistoryDto, FileHistoryModel>().ReverseMap();
        }
    }
}
