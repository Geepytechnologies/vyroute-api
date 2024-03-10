using vyroute.Data;
using vyroute.Models;
using vyroute.Repositories;

namespace vyroute.Services
{
    public class TransporterService: ITransporterService
    {
        private readonly ITransporterRepository _transporterRepository;
        private readonly AppDbContext _dbContext;

        public TransporterService(ITransporterRepository transporterRepository, AppDbContext dbContext)
        {
            _transporterRepository = transporterRepository;
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Transporter>> GetAllTransporters()
        {
            return await _transporterRepository.GetAllAsync();
        }

        public Transporter AddTransporter(Transporter transporter)
        {
            _transporterRepository.Add(transporter);
            _dbContext.SaveChanges();
            return transporter;
        }

        public async Task<Transporter> FindTransporterByID(Guid transporterId)
        {
            return await _transporterRepository.GetByIdAsync(transporterId);
        }
    }

    public interface ITransporterService
    {
        Task<IEnumerable<Transporter>> GetAllTransporters();

        Transporter AddTransporter(Transporter transporter);

        Task<Transporter> FindTransporterByID(Guid transporterId);

    }
}
