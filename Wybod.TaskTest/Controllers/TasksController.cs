using Microsoft.AspNetCore.Mvc;
using Wybod.TaskTest.Data.Models;
using Wybod.TaskTest.Data.Repositories;

namespace Wybod.TaskTest.Controllers;

[ApiController]
[Route("api/[controller]")]

public class TaskController : ControllerBase
{
    private readonly ITaskRepository _repo;

    public TaskController(ITaskRepository repo) => _repo = repo;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? status) =>
        Ok(await _repo.GetAllAsync(status));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var task = await _repo.GetByIdAsync(id);
        return task == null ? NotFound() : Ok(task);
    }

    [HttpPost]
    public async Task<IActionResult> Create(TaskItem task) =>
        Ok(await _repo.CreateAsync(task));

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, TaskItem updated)
    {
        updated.Id = id;
        updated.CompletedAt = updated.IsCompleted ? DateTime.UtcNow : null;
        var success = await _repo.UpdateAsync(updated);
        return success ? Ok(updated) : NotFound();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _repo.DeleteAsync(id);
        return success ? Ok() : NotFound();
    }
}






//
//
// public class TasksController : ControllerBase
// {
//     private readonly ITaskRepository _repo;
//
//     public TaskController(ITaskRepository repo) => _repo = repo;
//
//     [HttpGet]
//     public async Task<IActionResult> GetAll([FromQuery] string? status) =>
//         Ok(await _repo.GetAllAsync(status));
//
//     [HttpGet("{id:guid}")]
//     public async Task<IActionResult> Get(Guid id)
//     {
//         var task = await _repo.GetByIdAsync(id);
//         return task == null ? NotFound() : Ok(task);
//     }
//
//     [HttpPost]
//     public async Task<IActionResult> Create(TaskItem task) =>
//         Ok(await _repo.CreateAsync(task));
//
//     [HttpPut("{id:guid}")]
//     public async Task<IActionResult> Update(Guid id, TaskItem updated)
//     {
//         updated.Id = id;
//         updated.CompletedAt = updated.IsCompleted ? DateTime.UtcNow : null;
//         var success = await _repo.UpdateAsync(updated);
//         return success ? Ok(updated) : NotFound();
//     }
//
//     [HttpDelete("{id:guid}")]
//     public async Task<IActionResult> Delete(Guid id)
//     {
//         var success = await _repo.DeleteAsync(id);
//         return success ? Ok() : NotFound();
//     }
//     
//     
//     
//     // private readonly ITaskRepository _repository;
//     //
//     // public TasksController(ITaskRepository repository)
//     // {
//     //     _repository = repository;
//     // }
//     //
//     // [HttpGet]
//     // public ActionResult<IEnumerable<TaskItem>> GetTasks()
//     // {
//     //     return Ok(_repository.GetAll());
//     // }
//     //
//     // [HttpGet("{id:guid}")]
//     // public ActionResult<TaskItem> GetTask(Guid id)
//     // {
//     //     // TODO: Implement
//     //     throw new NotImplementedException();
//     // }
//     //
//     // [HttpPost]
//     // public ActionResult<TaskItem> CreateTask(TaskItem task)
//     // {
//     //     // TODO: Implement
//     //     throw new NotImplementedException();
//     // }
//     //
//     // [HttpPut("{id:guid}")]
//     // public IActionResult UpdateTask(Guid id, TaskItem task)
//     // {
//     //     // TODO: Implement
//     //     throw new NotImplementedException();
//     // }
//     //
//     // [HttpDelete("{id:guid}")]
//     // public IActionResult DeleteTask(Guid id)
//     // {
//     //     // TODO: Implement
//     //     throw new NotImplementedException();
//     // }
// }
