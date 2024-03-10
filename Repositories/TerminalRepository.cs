using Microsoft.EntityFrameworkCore;
using vyroute.Data;
using vyroute.Models;

namespace vyroute.Repositories
{
    public class TerminalRepository: GenericRepository<Terminal> , ITerminalRepository
    {
        private readonly AppDbContext _dbContext;

        public TerminalRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Terminal> GetAllTerminals()
        {
            var terminals = GetAllAsync().Result;
            return terminals;
        }
        public async Task<IEnumerable<Terminal>> GetTerminalByDeparture(string departure)
        {
            var terminals = await _dbContext.Terminals
                .Where(t => t.Departure == departure)
                .ToListAsync();
            return terminals;
        }
        public async Task<IEnumerable<Terminal>> GetAvailableVehiclesInATerminal(string departure)
        {
            var terminals = await _dbContext.Terminals
                .Where(t => t.Departure == departure)
                .ToListAsync();
            return terminals;
        }
    }

    public interface ITerminalRepository: IGenericRepository<Terminal>
    {
        IEnumerable<Terminal> GetAllTerminals();
        Task<IEnumerable<Terminal>> GetTerminalByDeparture(string departure);
    }
}
