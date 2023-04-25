using SportPlanner.Repository.Models.Abstract;

namespace SportPlanner.Repository.Models
{
    public class User : TableEntity
    {
        public string Name { get; set; } = string.Empty;
    }
}
