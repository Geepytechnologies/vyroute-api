using AutoMapper;
using Microsoft.EntityFrameworkCore;
using vyroute.Data;
using vyroute.Dto;
using vyroute.Models;
using vyroute.Repositories;

namespace vyroute.Services
{
    public class TransitService : ITransitService
    {
        private readonly ITransitRepository _transitRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IMapper _mapper;
        private readonly AppDbContext _dbContext;

        public TransitService(ITransitRepository transitRepository, IVehicleRepository vehicleRepository, AppDbContext dbContext, IMapper mapper)
        {
            _transitRepository = transitRepository;
            _vehicleRepository = vehicleRepository;
            _mapper = mapper;
            _dbContext = dbContext;

        }
        public async Task<IEnumerable<TransitResponseDTO>> GetOrCreateTransit(TransitwithoutvehicleDto transit)
        {
            var existingTransits = await _transitRepository.GetTransitByDepartureDate(transit.DepartureDate, transit.TerminalId);

            if (existingTransits.Any())
            {
                return _mapper.Map<IEnumerable<TransitResponseDTO>>(existingTransits);
            }
            else
            {
                //get a vehicle for the transit
                var vehicle = _vehicleRepository.GetAvailableVehicleForTransit(transit.TerminalId);


                var newTransit = new Transit
                {
                    DepartureDate = transit.DepartureDate,
                    //ArrivalDate = transit.ArrivalDate,
                    TerminalId = transit.TerminalId,
                    VehicleID = vehicle.Id,
                };

                 _transitRepository.Add(newTransit);
                _dbContext.SaveChanges();
                var transitWithVehicle = _dbContext.Transits
    .Include(t => t.Vehicle)
    .FirstOrDefault(t => t.Id == newTransit.Id);

                return _mapper.Map<IEnumerable<TransitResponseDTO>>(transitWithVehicle);
            }
        }

        public async Task<Transit> CreateTransit(Transit transit)
        {
            _transitRepository.Add(transit);
            _dbContext.SaveChanges();
            return transit;
        }
        public async Task<IEnumerable<Transit>> GetAllTransits()
        {
            return await _transitRepository.GetAllAsync();
        }

        public async Task<Transit> GetTransitById(Guid transitId)
        {
            return await _transitRepository.GetByIdAsync(transitId);
        }
        public async Task<IEnumerable<TransitResponseDTO>> GetTransitByDepartureDate(DateOnly departure, Guid TerminalID)
        {
            var transit =  await _transitRepository.GetTransitByDepartureDate(departure, TerminalID);
            return _mapper.Map<IEnumerable<TransitResponseDTO>>(transit);
        }
    }

    public interface ITransitService
    {
        Task<IEnumerable<Transit>> GetAllTransits();

        Task<Transit> GetTransitById(Guid transitId);

        Task<Transit> CreateTransit(Transit transit);
        Task<IEnumerable<TransitResponseDTO>> GetOrCreateTransit(TransitwithoutvehicleDto transit);
        Task<IEnumerable<TransitResponseDTO>> GetTransitByDepartureDate(DateOnly departure, Guid TerminalID);
    }
}
