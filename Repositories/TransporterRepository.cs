using vyroute.Data;
using vyroute.Models;

namespace vyroute.Repositories
{
    public class TransporterRepository: GenericRepository<Transporter>, ITransporterRepository
    {
        private readonly AppDbContext _dbContext;

        public TransporterRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Transporter> GetAllTransporters()
        {
            var transporters = GetAllAsync().Result;

            return transporters;
        }

       

       

    }
    public interface ITransporterRepository : IGenericRepository<Transporter>
    {
        IEnumerable<Transporter> GetAllTransporters();
    }
}
