using Microsoft.EntityFrameworkCore;

namespace Todo.Api.Infrastructure;

public sealed class TodoDbContext : DbContext
{
    public DbSet<Domain.Todo> Todos { get; set; }

    public TodoDbContext(DbContextOptions<TodoDbContext> options): base(options)
    {
        Database.EnsureCreated(); 
        Database.Migrate();
    }
}