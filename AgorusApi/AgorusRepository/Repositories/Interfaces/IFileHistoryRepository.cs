using AgorusApi.Dto;
using AgorusApi.Model;

namespace AgorusService.Repositories.Interfaces
{
    public interface IFileHistoryRepository
    {
        Task<FileHistoryModel?> ReadById(int? id, int? detailId);
        Task Update(FileHistoryModel fileHistoryModel);
        Task<FileHistoryModel> Insert(FileHistoryModel fileHistoryModel);
        Task Delete(FileHistoryModel fileHistoryModel);
    }
}
