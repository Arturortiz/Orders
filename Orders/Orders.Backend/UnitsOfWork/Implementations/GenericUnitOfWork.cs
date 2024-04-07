using Orders.Backend.UnitsOfWork.Interfaces;
using Orders.Shared.Responses;

namespace Orders.Backend.UnitsOfWork.Implementations
{
    public class GenericUnitOfWork<T> : IGenericUnitOfWork<T> where T : class
    {
        private readonly IGenericUnitOfWork<T> _repository;
        public GenericUnitOfWork(IGenericUnitOfWork<T> repository) 
        {
            _repository = repository;  
        }
        public Task<ActionResponse<T>> AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResponse<T>> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResponse<T>> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResponse<IEnumerable<T>>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ActionResponse<T>> UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
