using AgorusApi.Dto;
using AgorusApi.Model;

namespace AgorusService.Repositories.Interfaces
{
    public interface IFileRepository
    {
        Task<IEnumerable<FileModel>> ReadAllWithDetails();
        Task<FileModel?> ReadByIdWithDetail(int? id);
        Task<FileModel?> ReadByIdWithoutDetail(int? id);
        Task<FileModel?> ReadByName(string? name);

        /// <summary>
        /// Insert FileModel if not exist. It will return the FileModel inserted or founded.
        /// </summary>
        /// <param name="fileModel"></param>
        /// <returns></returns>
        Task<FileModel?> Insert(FileModel fileModel);
        Task<(bool IsSuccess, string Message)> Delete(int id);
    }
}
