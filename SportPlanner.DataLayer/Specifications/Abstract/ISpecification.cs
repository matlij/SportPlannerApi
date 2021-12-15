using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SportPlanner.DataLayer.Specifications.Abstract
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Filter { get; }
        List<string> Includes { get; }
    }
}
