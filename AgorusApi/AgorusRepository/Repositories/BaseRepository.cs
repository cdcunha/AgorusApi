using AgorusApi.Context;

namespace AgorusRepository.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly AppDbContext _context;

        public BaseRepository(AppDbContext context)
        {
            context.Database.EnsureCreated();
            _context = context;
        }
    }
}
