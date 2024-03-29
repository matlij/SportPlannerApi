﻿using SportPlanner.DataLayer.Models;
using SportPlanner.DataLayer.Specifications;
using SportPlanner.DataLayer.Specifications.Abstract;
using SportPlanner.ModelsDto.Enums;

namespace SportPlanner.DataLayer
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<Tdto>> GetAll<Tdto>(int limit = 100);

        Task<IEnumerable<Tdto>> Get<Tdto>(ISpecification<T> spec, int limit = 100);

        Task<(CrudResult result, Tdto dto)> Add<Tdto>(Tdto entityDto, ISpecification<T>? duplicateCheck = null);

        Task<CrudResult> Delete(Guid id);

        Task<CrudResult> Update<Tdto>(GetByIdSpecification<T> spec, Tdto entityDto);
    }
}
