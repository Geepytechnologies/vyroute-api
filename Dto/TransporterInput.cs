using vyroute.Models;

namespace vyroute.Dto
{
    public class TransporterInput
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateOnly Birthyear { get; set; }
        public string State { get; set; }
        public string Lga { get; set; }

        public List<IFormFile>? Images { get; set; }

        public List<string>? ImagePaths { get; set; }




    }

}
