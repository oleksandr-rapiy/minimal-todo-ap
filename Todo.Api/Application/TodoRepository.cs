using Microsoft.EntityFrameworkCore;
using Todo.Api.Domain;
using Todo.Api.Infrastructure;

namespace Todo.Api.Application;

public class TodoRepository : ITodoRepository
{
    private readonly TodoDbContext _context;

    public TodoRepository(TodoDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Domain.Todo todo)
    {
        await _context.AddAsync(todo);

        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Domain.Todo>> GetAllAsync()
    {
        return await _context.Todos.ToListAsync();
    }

    public async Task<Domain.Todo?> GetByIdAsync(int id)
    {
        return await _context.Todos.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task UpdateAsync(Domain.Todo todo)
    {
        _context.Todos.Update(todo);
        await _context.SaveChangesAsync();
    }

    public Task DeleteAsync(int id)
    {
        var todo = _context.Todos.FirstOrDefault(x => x.Id == id);

        if (todo is not null)
        {
            _context.Todos.Remove(todo);
        }

        return _context.SaveChangesAsync();
    }
}