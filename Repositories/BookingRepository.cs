using Microsoft.EntityFrameworkCore;
using vyroute.Data;
using vyroute.Models;

namespace vyroute.Repositories
{
    public class BookingRepository: GenericRepository<Booking> , IBookingRepository
    {
        private readonly AppDbContext _dbContext;

        public BookingRepository(AppDbContext dbContext): base(dbContext)
        {
            _dbContext = dbContext;

        }
    }
    public interface IBookingRepository : IGenericRepository<Booking>
    {
    }
}
