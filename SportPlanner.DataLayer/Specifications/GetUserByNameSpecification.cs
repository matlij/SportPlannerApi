using SportPlanner.DataLayer.Models;
using SportPlanner.DataLayer.Specifications.Abstract;

namespace SportPlanner.DataLayer.Specifications
{
    public class GetUserByNameSpecification : SpecificationBase<User>
    {
        public GetUserByNameSpecification(string name) : base(u => u.Name == name)
        {
        }
    }
}
