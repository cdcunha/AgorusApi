using AgorusApi.Context;
using AgorusApi.Model;
using AgorusRepository.Repositories;
using AgorusService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AgorusService.Repositories
{
    public class FileHistoryRepository : BaseRepository, IFileHistoryRepository
    {
        public FileHistoryRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<FileHistoryModel?> ReadById(int? id, int? historyId)
        {
            return await _context.FileHistories
                .Where(e => e.FileModelId == id && e.FileHistoryModelId == historyId).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task Update(FileHistoryModel fileHistoryModel)
        {
            _context.Entry(fileHistoryModel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<FileHistoryModel> Insert(FileHistoryModel fileHistoryModel)
        {
            _context.FileHistories.Add(fileHistoryModel);
            await _context.SaveChangesAsync();
            return fileHistoryModel;
        }

        public async Task Delete(FileHistoryModel fileHistoryModel)
        {
            _context.FileHistories.Remove(fileHistoryModel);
            await _context.SaveChangesAsync();
        }
    }
}
