using AgorusApi.Helpers;
using AgorusService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AgorusApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;
        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        // PUT: api/File/5/History/1
        [HttpPut("{id}/History/{historyId}")]
        public async Task<IActionResult> PutFile(int id, int historyId, [Required] IFormFile file)
        {
            var fileDto = FileHelper.FormFileToFileDto(file);
            var fileHistoryDto = await new FileHistoryHelper().FormFileToFileHistoryDto(file);
            fileHistoryDto.FileModelId = id;
            fileHistoryDto.FileHistoryModelId = historyId;
            var (isSuccess, message) = await _fileService.UpdateHistory(fileDto, fileHistoryDto);
            if (isSuccess)
                return NoContent();
            else
                return NotFound(message);
        }

        // POST: api/File
        [HttpPost]
        public async Task<IActionResult> PostFile([Required] IFormFile file)
        {
            var fileDto = FileHelper.FormFileToFileDto(file);
            var fileHistoryDto = await new FileHistoryHelper().FormFileToFileHistoryDto(file);

            var newfileHistoryDto = await _fileService.InsertHistory(fileDto, fileHistoryDto);

            return Created($"api/File/{newfileHistoryDto.FileModelId}", newfileHistoryDto);
        }
    }
}
