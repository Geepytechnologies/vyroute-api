using vyroute.Data;
using vyroute.Models;
using vyroute.Repositories;

namespace vyroute.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly AppDbContext _dbContext;

        public BookingService(IBookingRepository bookingRepository, AppDbContext dbContext)
        {
            _bookingRepository = bookingRepository;
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Booking>> GetAllBookings()
        {
            return await _bookingRepository.GetAllAsync();
        }

        public async Task<Booking> GetBookingByIdAsync(Guid bookingId)
        {
            return await _bookingRepository.GetByIdAsync(bookingId);
        }

        public async Task<Booking> CreateBooking(Booking booking)
        {
            _bookingRepository.Add(booking);
            _dbContext.SaveChanges();
            return booking;
        }
    }

    public interface IBookingService
    {
        Task<IEnumerable<Booking>> GetAllBookings();
        Task<Booking> GetBookingByIdAsync(Guid bookingId);
        Task<Booking> CreateBooking(Booking booking);
    }
}
