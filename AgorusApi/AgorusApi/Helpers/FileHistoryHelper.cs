using AgorusApi.Dto;
using AgorusApi.Model;

namespace AgorusApi.Helpers
{
    public class FileHistoryHelper
    {
        public async Task<FileHistoryDto> FormFileToFileHistoryDto(IFormFile file)
        {
            using var memoryStream = new MemoryStream();

            if (memoryStream.Length < 2097152)
            {
                await file.CopyToAsync(memoryStream);
                var fileContent = memoryStream.ToArray();

                var fileHistoryDto = new FileHistoryDto()
                {
                    Date = DateTime.Now,
                    FileContent = fileContent,
                    Length = file.Length,
                };

                return fileHistoryDto;
            }
            else
            {
                throw new Exception("The file is too large. The file maximum size is 2MB");
            }
        }
    }
}
