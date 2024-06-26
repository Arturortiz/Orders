﻿using Orders.Shared.DTOs;
using Orders.Shared.Responses;

namespace Orders.Backend.UnitsOfWork.Interfaces
{
    public interface IGenericUnitOfWork<T> where T : class //donde la T corresponde a una clase
    {
        Task<ActionResponse<T>> GetAsync(int id);
        Task<ActionResponse<IEnumerable<T>>> GetAsync();
        //----------------------------------------------------------------------
        Task<ActionResponse<IEnumerable<T>>> GetAsync(PaginationDTO pagination);

        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);
        //----------------------------------------------------------------------
        Task<ActionResponse<T>> AddAsync(T entity);
        Task<ActionResponse<T>> DeleteAsync(int id);
        Task<ActionResponse<T>> UpdateAsync(T entity);
    }
}
