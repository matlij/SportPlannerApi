using SportPlanner.DataLayer.Models;
using SportPlanner.DataLayer.Specifications.Abstract;
using System;

namespace SportPlanner.DataLayer.Specifications
{
    public class GetUserByIdSpecification : SpecificationBase<User>
    {
        public GetUserByIdSpecification(Guid id) : base(e => e.Id == id)
        {
        }
    }
}
