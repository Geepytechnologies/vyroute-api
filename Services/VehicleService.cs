using Microsoft.EntityFrameworkCore;
using vyroute.Data;
using vyroute.Models;
using vyroute.Repositories;

namespace vyroute.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly AppDbContext _dbContext;

        public VehicleService(IVehicleRepository vehicleRepository, AppDbContext dbContext)
        {
            _vehicleRepository = vehicleRepository;
            _dbContext = dbContext;

        }
        public async Task<IEnumerable<Vehicle>> GetAllVehiclesAsync()
        {
            return await _vehicleRepository.GetAllAsync();
        }

        public async Task<Vehicle> GetVehicleByIdAsync(Guid vehicleId)
        {
            return await _vehicleRepository.GetByIdAsync(vehicleId);
        }

        public Vehicle AddVehicle(Vehicle vehicle)
        {
            _vehicleRepository.Add(vehicle);
            _dbContext.SaveChanges();
            return vehicle;
        }

        public void UpdateVehicle(Vehicle vehicle)
        {
            _vehicleRepository.Update(vehicle);
        }

        public void DeleteVehicle(Vehicle vehicle)
        {
            _vehicleRepository.Delete(vehicle);
        }
        public IEnumerable<Vehicle> GetAllVehiclesWithTransporter()
        {
            return _vehicleRepository.GetAllWithTransporter();
        }
        public IEnumerable<Vehicle> GetAllVehiclesWithTransits()
        {
            return _vehicleRepository.GetAllWithTransit();
        }
        public Vehicle GetAvailableVehicleForTransit(Guid terminalID)
        {
            return _vehicleRepository.GetAvailableVehicleForTransit(terminalID);
        }
    }
    public interface IVehicleService
    {
        Task<IEnumerable<Vehicle>> GetAllVehiclesAsync();
        IEnumerable<Vehicle> GetAllVehiclesWithTransits();
        Task<Vehicle> GetVehicleByIdAsync(Guid vehicleId);
        Vehicle AddVehicle(Vehicle vehicle);
        void UpdateVehicle(Vehicle vehicle);
        void DeleteVehicle(Vehicle vehicle);
        IEnumerable<Vehicle> GetAllVehiclesWithTransporter();
        Vehicle GetAvailableVehicleForTransit(Guid terminalID);
    }
}
