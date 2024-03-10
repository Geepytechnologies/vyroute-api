using Microsoft.EntityFrameworkCore;
using vyroute.Data;
using vyroute.Models;

namespace vyroute.Repositories
{
    public class VehicleRepository : GenericRepository<Vehicle>, IVehicleRepository
    {
        private readonly AppDbContext _dbContext;

        public VehicleRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }


        public IEnumerable<Vehicle> GetAllWithTransporter()
        {
            return _dbContext.Set<Vehicle>().Include(v => v.Transporter).ToList();
        }
        public IEnumerable<Vehicle> GetAllWithTransit()
        {
            return _dbContext.Set<Vehicle>().Include(v => v.Transits).ToList();
        }
        public Vehicle GetAvailableVehicleForTransit(Guid terminalID)
        {

            var vehicle = _dbContext.Set<Vehicle>()
                .Where(v => v.TerminalId == terminalID && v.Transits.Count != 0)
                .OrderBy(v => v.Transits.Min(t => t.CreatedAt))
                .Include(v => v.Transits)
                .FirstOrDefault();
            return vehicle;

        }

    }
    public interface IVehicleRepository : IGenericRepository<Vehicle>
    {
        IEnumerable<Vehicle> GetAllWithTransporter();
        IEnumerable<Vehicle> GetAllWithTransit();
        Vehicle GetAvailableVehicleForTransit(Guid terminalID);
    }
}
