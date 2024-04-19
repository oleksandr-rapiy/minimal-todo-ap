using Todo.Api.Domain;
using Todo.Api.Application;
using Todo.Api.Dtos;

namespace Todo.Api.Extensions;

public static class RouteBuilderExtension
{
    public static RouteGroupBuilder MapTodoApi(this RouteGroupBuilder groupBuilder)
    {
        groupBuilder.MapGet("/", async (ITodoRepository repository) => Results.Ok((object?)await repository.GetAllAsync())); 
        
        groupBuilder.MapGet("/{id}", async (ITodoRepository repository, int id) =>
        {
            var todo = await repository.GetByIdAsync(id);

            return todo is null ? Results.NotFound() : Results.Ok(todo);
        });
        
        groupBuilder.MapPost("/", async (ITodoRepository repository, TodoDto todoDto) =>
        {
            var todo = new Domain.Todo
            {
                Title = todoDto.Title,
                IsCompleted = todoDto.IsCompleted
            };
            
            await repository.AddAsync(todo);

            return Results.Ok(todo);
        });
        
        groupBuilder.MapPut("/{id}", async (ITodoRepository repository, int id, TodoDto todoDto) =>
        {
            var todo = await repository.GetByIdAsync(id);

            if (todo is null)
            {
                return Results.NotFound();
            }

            todo.Title = todoDto.Title;
            todo.IsCompleted = todoDto.IsCompleted;

            await repository.UpdateAsync(todo);

            return Results.Ok(todo);
        });
        
        groupBuilder.MapDelete("/{id}", async (ITodoRepository repository, int id) =>
        {
            await repository.DeleteAsync(id);

            return Results.NoContent();
        });

        return groupBuilder;
    }
}