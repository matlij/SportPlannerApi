using SportPlanner.Repository.Models.Abstract;

namespace SportPlanner.Repository.Models
{
    public class Address : TableEntity
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
