using Microsoft.EntityFrameworkCore;
using Orders.Backend.Data;
using Orders.Backend.Repositories.Interfaces;
using Orders.Shared.Responses;

namespace Orders.Backend.Repositories.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DataContext _context; 

        private readonly DbSet<T> _entity;
        public GenericRepository(DataContext context)//al repositorio le injectamos el dataContext 
        {
            _context = context; 
            _entity = _context.Set<T>(); //context me va a representar toda la base de datos y entity va a ser la entidad que yo quiero manipular del repositorio
        }

        //-------------------------------------------------------------------------

        public async Task<ActionResponse<T>> AddAsync(T entity)
        {
            _context.Add(entity);
            try
            {
                await _context.SaveChangesAsync();
                return new ActionResponse<T>
                {
                    wasSuccess = true,
                    Result = entity,
                };
            }catch (DbUpdateException)
            {
                return DbUpdateExceptionActionRespones();
            }
            catch(Exception ex)
            {
                return ExceptionActionResponse(ex);
            }
        }

        public async Task<ActionResponse<T>> DeleteAsync(int id)
        {
            var row= await _entity.FindAsync(id);
            if(row == null)
            {
                return new ActionResponse<T>
                {
                    wasSuccess = false,
                    Message = "Registro no encontrado",
                };
            }

            try
            {
                _entity.Remove(row);
                await _context.SaveChangesAsync();
                return new ActionResponse<T>
                {
                    wasSuccess = true,
                };
            }
            catch
            {
                return new ActionResponse<T>
                {
                    wasSuccess= false,
                    Message="No se pude eliminar porque tiene registros relacionados",
                };
            }
        }

        public async Task<ActionResponse<T>> GetAsync(int id)
        {
            var row = await _entity.FindAsync(id);
            if (row == null)
            {
                return new ActionResponse<T>
                {
                    wasSuccess = false,
                    Message = "Registro no encontrado",
                };
            }

            return new ActionResponse<T>
            {
                wasSuccess = true,
                Result = row,
            };
        }

        public async Task<ActionResponse<IEnumerable<T>>> GetAsync()
        {
            return new ActionResponse<IEnumerable<T>>
            {
                wasSuccess = true,
                Result = await _entity.ToListAsync(),
            };
        }

        public async Task<ActionResponse<T>> UpdateAsync(T entity)
        {
            _context.Update(entity);
            try
            {
                await _context.SaveChangesAsync();
                return new ActionResponse<T>
                {
                    wasSuccess = true,
                    Result = entity,
                };
            }
            catch (DbUpdateException)
            {
                return DbUpdateExceptionActionRespones();//esta excepcion no tiene sentido ????
            }
            catch (Exception ex)
            {
                return ExceptionActionResponse(ex);
            }
        }

        //-------------------------------------------------------------------------------------------

        private ActionResponse<T> DbUpdateExceptionActionRespones()
        {
            return new ActionResponse<T>
            {
                wasSuccess = false,
                Message= "Ya existe el registro que estas intentando crear",
            };
        }

        private ActionResponse<T> ExceptionActionResponse(Exception ex)
        {
            return new ActionResponse<T>
            {
                wasSuccess = false,
                Message = ex.Message,
            };
        }
    }
}
