namespace vyroute.Dto
{
    public class TransitDto
    {
        public DateOnly DepartureDate { get; set; }

        public DateOnly? ArrivalDate { get; set; }

        public Guid TerminalId { get; set; }

        public Guid VehicleID { get; set; }

    }
    public class TransitwithoutvehicleDto
    {
        public DateOnly DepartureDate { get; set; }

        public DateOnly? ArrivalDate { get; set; }

        public Guid TerminalId { get; set; }



    }
}
