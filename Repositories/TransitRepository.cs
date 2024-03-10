using Microsoft.EntityFrameworkCore;
using vyroute.Data;
using vyroute.Dto;
using vyroute.Models;

namespace vyroute.Repositories
{
    public class TransitRepository: GenericRepository<Transit> , ITransitRepository
    {
        private readonly AppDbContext _dbContext;

        public TransitRepository(AppDbContext dbContext): base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Transit>> GetTransitByDepartureDate(DateOnly departure, Guid terminalID)
        {
            var transit = await _dbContext.Transits
                .Where(t => t.DepartureDate == departure && t.TerminalId == terminalID && t.Seatsbooked.Length < 13).Include(t => t.Vehicle)
                .ToListAsync();
            return transit;
        }
    }

    public interface ITransitRepository: IGenericRepository<Transit>
    {
        Task<IEnumerable<Transit>> GetTransitByDepartureDate(DateOnly departure, Guid terminalID);
    }
}
