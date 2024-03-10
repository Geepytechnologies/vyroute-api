using Microsoft.EntityFrameworkCore;
using vyroute.Data;

namespace vyroute.Repositories
{
    public class GenericRepository<T>: IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _dbcontext;

        public GenericRepository(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbcontext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbcontext.Set<T>().FindAsync(id);
        }

        public void Add(T entity)
        {
            _dbcontext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _dbcontext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _dbcontext.Set<T>().Remove(entity);
        }
    }

    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
