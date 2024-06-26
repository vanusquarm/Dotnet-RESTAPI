using Microsoft.EntityFrameworkCore;
using WebApplication.API.Entities.Models;
using WebApplication.API.Persistence;

namespace WebApplication.API.Repositories
{
    public class TodoRepository : BaseRepositorySync<TodoItem>
    {
        public TodoRepository(ApplicationDbContext context) : base(context)
        {
        }


        // Additional methods implementations specific to repository, if needed
    }
}
