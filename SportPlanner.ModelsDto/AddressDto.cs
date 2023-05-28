namespace SportPlanner.ModelsDto
{
    public class AddressDto
    {
        public Guid Id { get; set; }
        public string FullAddress { get; set; } = string.Empty;
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
