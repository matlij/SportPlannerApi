using SportPlanner.DataLayer.Models;
using SportPlanner.DataLayer.Specifications.Abstract;

namespace SportPlanner.DataLayer.Specifications
{
    public class GetByIdSpecification<T> : SpecificationBase<T> where T : BaseEntity
    {
        public GetByIdSpecification(Guid id) : base(e => e.Id == id)
        {
        }
    }
}
