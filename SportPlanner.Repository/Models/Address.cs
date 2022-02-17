using SportPlanner.Repository.Models.Abstract;

namespace SportPlanner.DataLayer.Models
{
    public class Address : TableEntityBase
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
