using AgorusApi.Dto;

namespace AgorusApi.Helpers
{
    public static class FileHelper
    {
        public static FileDto FormFileToFileDto(IFormFile file)
        {
            return new FileDto()
            {
                ContentType = file.ContentType,
                Name = file.FileName,
            };
        }
    }
}
