using SportPlanner.Repository.Models.Abstract;
using System.Collections.Generic;

namespace SportPlanner.DataLayer.Models
{
    public class User : TableEntity
    {
        public string Name { get; set; } = string.Empty;
    }
}
