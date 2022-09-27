using AgorusApi.Context;
using AgorusApi.Model;
using AgorusRepository.Repositories;
using AgorusService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace AgorusService.Repositories
{
    public class FileRepository : BaseRepository, IFileRepository
    {   
        public FileRepository(AppDbContext context) : base(context)
        {
            
        }

        public async Task<IEnumerable<FileModel>> ReadAllWithDetails()
        {
            return await _context.Files.Include(e => e.FileHistoryModels).ToListAsync();
        }

        public async Task<FileModel?> ReadByIdWithDetail(int? id)
        {
            return await _context.Files.Include(e => e.FileHistoryModels)
                .Where(e => e.FileModelId == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<FileModel?> ReadByIdWithoutDetail(int? id)
        {
            return await _context.Files.FindAsync(id);
        }

        public async Task<FileModel?> ReadByName(string? name)
        {
            return await _context.Files.Where(e => e.Name == name).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<FileModel?> Insert(FileModel fileModel)
        {
            var fileModelFound = await ReadByName(fileModel.Name);

            if (fileModelFound == null)
            {
                _context.Files.Add(fileModel);
                await _context.SaveChangesAsync();
                return fileModel;
            }

            return fileModelFound;
        }

        public async Task<(bool IsSuccess, string Message)> Delete(int id)
        {
            var fileModel = await ReadByIdWithoutDetail(id);
            if (fileModel == null)
                return (false, $"Id {id} not found");

            _context.Files.Remove(fileModel);
            await _context.SaveChangesAsync();

            return (true, "Success");
        }
    }
}
