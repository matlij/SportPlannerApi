using SportPlanner.DataLayer.Models;
using SportPlanner.DataLayer.Specifications.Abstract;
using System;

namespace SportPlanner.DataLayer.Specifications
{
    public class GetAddresssByIdSpecification : SpecificationBase<Address>
    {
        public GetAddresssByIdSpecification(Guid id) : base(e => e.Id == id)
        {
        }
    }
}
