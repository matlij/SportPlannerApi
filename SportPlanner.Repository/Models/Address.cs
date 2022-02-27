using SportPlanner.Repository.Models.Abstract;

namespace SportPlanner.DataLayer.Models
{
    public class Address : TableEntity
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
