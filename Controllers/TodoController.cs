using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.API.Entities.Models;
using WebApplication.API.Repositories;

[Route("api/todos")]
[ApiController]
public class TodoController : BaseController<TodoItem, TodoRepository>
{
    public TodoController(TodoRepository repository) : base(repository)
    {
    }
}
