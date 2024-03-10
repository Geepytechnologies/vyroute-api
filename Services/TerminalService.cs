using Microsoft.EntityFrameworkCore;
using vyroute.Data;
using vyroute.Models;
using vyroute.Repositories;

namespace vyroute.Services
{
    public class TerminalService : ITerminalService
    {
        private readonly ITerminalRepository _terminalRepository;
        private readonly AppDbContext _dbContext;

        public TerminalService(ITerminalRepository terminalRepository, AppDbContext dbContext)
        {
            _terminalRepository = terminalRepository;
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Terminal>> GetAllTerminals()
        {
            return await _terminalRepository.GetAllAsync();
        }
        public async Task<Terminal> CreateTerminal(Terminal terminal)
        {
            _terminalRepository.Add(terminal);
            _dbContext.SaveChanges();
            return terminal;
        }
        public async Task<Terminal> GetTerminalById(Guid terminalId)
        {
            return await _terminalRepository.GetByIdAsync(terminalId);
        }

        public async Task<IEnumerable<Terminal>> GetTerminalByDeparture(string departure)
        {
            return await _terminalRepository.GetTerminalByDeparture(departure);
        }
        public void UpdateTerminal(Terminal terminal)
        {
            _terminalRepository.Update(terminal);
        }
    }

    public interface ITerminalService
    {
        Task<IEnumerable<Terminal>> GetAllTerminals();
        Task<Terminal> CreateTerminal(Terminal terminal);
        Task<Terminal> GetTerminalById(Guid terminalId);
        Task<IEnumerable<Terminal>> GetTerminalByDeparture(string departure);

        void UpdateTerminal(Terminal terminal);
    }
}
