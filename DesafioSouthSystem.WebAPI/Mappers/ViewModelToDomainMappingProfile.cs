using AutoMapper;
using DesafioSouthSystem.Domain.Commands;
using DesafioSouthSystem.Domain.Queries;
using DesafioSouthSystem.WebAPI.Models;

namespace DesafioSouthSystem.WebAPI.Mappers
{
    public class ViewModelToDomainMappingProfile: Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<FileUploadCommandVM, FileUploadCommand>();
            CreateMap<FileDownloadQueryVM, FileDownloadQuery>();
        }
    }
}
