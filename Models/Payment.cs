using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vyroute.Models
{
    public class Payment
    {
        public Guid Id { get; set; }

        public int Amount { get; set; }

        public string? Receipt { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("BookingID")]
        public Guid BookingID { get; set; }
        public Booking Booking { get; set; }

    }
}
