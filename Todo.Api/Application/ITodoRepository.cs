using Todo.Api.Domain;

namespace Todo.Api.Application;

public interface ITodoRepository
{
    Task AddAsync(Domain.Todo todo);
    Task<IEnumerable<Domain.Todo>> GetAllAsync();
    Task<Domain.Todo?> GetByIdAsync(int id);
    Task UpdateAsync(Domain.Todo todo);
    Task DeleteAsync(int id);
}