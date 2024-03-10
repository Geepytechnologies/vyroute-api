

using System.ComponentModel.DataAnnotations.Schema;

namespace vyroute.Models
{
    public enum VehicleType
    {
        car,
        bus,
        truck,
        trailer
    }
    public enum Status
    {
        Available,
        Unavailable,
    }
    public class Vehicle
    {
        public Guid Id { get; set; }
        public string VehicleNumber { get; set; }
        public string VehicleColor { get; set; }

        public int VehiclePassengerCap { get; set; }

        public VehicleType VehicleType { get; set; }

        public int Seats { get; set; }

        public Status Status { get; set; }

        public List<string> ImagePaths { get; set; }

        public ICollection<Transit> Transits {  get; set; } 

        [ForeignKey("TransporterId")]
        public Guid TransporterId { get; set; }
        public Transporter Transporter { get; set; }


        [ForeignKey("TerminalId")]
        public Guid TerminalId { get; set; }
        public Terminal Terminal { get; set; }
    }
}
