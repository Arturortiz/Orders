﻿using Orders.Backend.Repositories.Implementations;
using Orders.Backend.Repositories.Interfaces;
using Orders.Backend.UnitsOfWork.Interfaces;
using Orders.Shared.DTOs;
using Orders.Shared.Entities;
using Orders.Shared.Responses;

namespace Orders.Backend.UnitsOfWork.Implementations
{
    public class CategoriesUnitOfWork : GenericUnitOfWork<Category>, ICategoriesUnitOfWork
    {
        private readonly ICategoriesRepository _categoriesRepository;
        public CategoriesUnitOfWork(IGenericRepository<Category> repository, ICategoriesRepository categoriesRepository) : base(repository)
        {
            _categoriesRepository = categoriesRepository;
        }

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO paginationDTO) => await _categoriesRepository.GetTotalPagesAsync(paginationDTO);
        public override async Task<ActionResponse<IEnumerable<Category>>> GetAsync(PaginationDTO pagination) => await _categoriesRepository.GetAsync(pagination);
    }
}
