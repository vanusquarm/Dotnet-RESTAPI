using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.API.Repositories;

[Route("api/[controller]")]
[ApiController]
public class BaseController<TEntity, TRepository> : ControllerBase
    where TEntity : class
    where TRepository : BaseRepositorySync<TEntity>
{
    protected readonly TRepository Repository;

    public BaseController(TRepository repository)
    {
        Repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    [HttpGet]
    public IActionResult GetAll(int pageNumber = 1, int pageSize = 10)
    {
        var (items, totalCount) = Repository.GetAll(pageNumber, pageSize);

        var response = new
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize,
            PageCount = (int)Math.Ceiling((double)totalCount / pageSize)
        };

        return Ok(response);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var entity = Repository.GetById(id);

        if (entity == null)
            return NotFound();

        return Ok(entity);
    }

    [HttpPost]
    public IActionResult Create([FromBody] TEntity entity)
    {
        if (entity == null)
            return BadRequest();

        Repository.Add(entity);
        Repository.SaveChanges();

        return CreatedAtAction(nameof(GetById), new { id = GetEntityId(entity) }, entity);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] TEntity entity)
    {
        if (entity == null || id != GetEntityId(entity))
            return BadRequest();

        Repository.Update(entity);
        Repository.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        Repository.Delete(id);
        Repository.SaveChanges();

        return NoContent();
    }

    // Helper method to get the entity ID (override in derived classes if needed)
    protected virtual int GetEntityId(TEntity entity)
    {
        // Assuming the entity has an 'Id' property
        return (int)entity.GetType().GetProperty("Id").GetValue(entity);
    }
}
