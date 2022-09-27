using AgorusApi.Dto;
using AgorusApi.Model;
using AgorusService.Repositories.Interfaces;
using AgorusService.Services.Interfaces;
using AutoMapper;

namespace AgorusService.Services
{
    public class FileService : IFileService
    {
        private readonly IMapper _mapper;
        private readonly IFileRepository _fileRepository;
        private readonly IFileHistoryRepository _fileHistoryRepository;

        public FileService(IMapper mapper, IFileRepository fileRepository, IFileHistoryRepository fileHistoryRepository)
        {
            _mapper = mapper;
            _fileRepository = fileRepository;
            _fileHistoryRepository = fileHistoryRepository;
        }

        public async Task<IEnumerable<FileDto>> ReadAllWithDetails()
        {
            var fileModels = await _fileRepository.ReadAllWithDetails();
            return _mapper.Map<IEnumerable<FileDto>>(fileModels);
        }

        public async Task<FileDto> ReadByIdWithDetail(int? id)
        {
            var fileModel = await _fileRepository.ReadByIdWithDetail(id);
            return _mapper.Map<FileDto>(fileModel);
        }

        public async Task<(bool IsSuccess, string Message, FileModel fileModel, FileHistoryModel fileHistoryModel)> IsIdAndDetailIdExist(int? id, int? fileHistoryId)
        {
            var fileModel = await _fileRepository.ReadByIdWithDetail(id);
            if (fileModel == null)
                return (false, $"Id {id} not found", null, null);

            var fileHistory = fileModel.FileHistoryModels.Where(e => e.FileHistoryModelId == fileHistoryId).FirstOrDefault();
            if (fileHistory == null)
                return (false, $"History Id {fileHistoryId} not found", null, null);

            return (true, "Success", fileModel, fileHistory);
        }

        public async Task<(bool IsSuccess, string Message)> Delete(int id)
        {
            return await _fileRepository.Delete(id);
        }

        public async Task<(bool IsSuccess, string Message)> UpdateHistory(FileDto fileDto, FileHistoryDto fileHistoryDto)
        {
            var result = await IsIdAndDetailIdExist(fileHistoryDto.FileModelId, fileHistoryDto.FileHistoryModelId);
            if (result.IsSuccess)
            {
                if (result.fileModel.Name != fileDto.Name)
                    return (false, "The file name does't match with the data on the database");

                var fileHistory = _mapper.Map<FileHistoryModel>(fileHistoryDto);
                await _fileHistoryRepository.Update(fileHistory);
            }

            return (result.IsSuccess, result.Message);
        }

        public async Task<FileHistoryDto> InsertHistory(FileDto fileDto, FileHistoryDto fileHistoryDto)
        {
            //Insert Master
            var fileModel = _mapper.Map<FileModel>(fileDto);
            var newFileModel = await _fileRepository.Insert(fileModel);

            //Insert Detail
            var fileHistoryModel = _mapper.Map<FileHistoryModel>(fileHistoryDto);
            fileHistoryModel.FileModelId = newFileModel.FileModelId;
            var newFileHistoryModel = await _fileHistoryRepository.Insert(fileHistoryModel);
            return _mapper.Map<FileHistoryDto>(newFileHistoryModel);
        }

        public async Task<(bool IsSuccess, string Message)> DeleteHistory(int id, int fileHistoryId)
        {
            var result = await IsIdAndDetailIdExist(id, fileHistoryId);
            if (result.IsSuccess)
                await _fileHistoryRepository.Delete(result.fileHistoryModel);
            
            return (result.IsSuccess, result.Message);
        }
    }
}
