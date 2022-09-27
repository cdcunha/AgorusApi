using AgorusApi.Dto;
using AgorusApi.Model;

namespace AgorusService.Services.Interfaces
{
    public interface IFileService
    {
        Task<IEnumerable<FileDto>> ReadAllWithDetails();
        Task<FileDto> ReadByIdWithDetail(int? id);

        /// <summary>
        /// Check if the Ids of FileModel and FileHistoryModel exist.<br/>
        /// If any Id not found it will return false and a message <br/>
        /// If the Ids is find it will return true, a message and the FileHistoryModel found by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fileHistoryId"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, string Message, FileModel fileModel, FileHistoryModel fileHistoryModel)> IsIdAndDetailIdExist(int? id, int? fileHistoryId);

        Task<(bool IsSuccess, string Message)> Delete(int id);

        #region File History
        Task<(bool IsSuccess, string Message)> UpdateHistory(FileDto fileDto, FileHistoryDto fileHistoryDto);
        Task<FileHistoryDto> InsertHistory(FileDto fileDto, FileHistoryDto fileHistoryDto);
        Task<(bool IsSuccess, string Message)> DeleteHistory(int id, int fileHistoryId);
        #endregion
    }
}
