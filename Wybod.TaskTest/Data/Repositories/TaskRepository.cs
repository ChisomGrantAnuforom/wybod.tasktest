using Wybod.TaskTest.Data.Models;

namespace Wybod.TaskTest.Data.Repositories;

public interface ITaskRepository
{
    // IEnumerable<TaskItem> GetAll();
    // TaskItem? GetById(Guid id);
    // TaskItem Create(TaskItem task);
    // bool Update(Guid id, TaskItem task);
    // bool Delete(Guid id);

        Task<IEnumerable<TaskItem>> GetAllAsync(string? status = null);
        Task<TaskItem?> GetByIdAsync(Guid id);
        Task<TaskItem> CreateAsync(TaskItem task);
        Task<bool> UpdateAsync(TaskItem task);
        Task<bool> DeleteAsync(Guid id);
}

public class TaskRepository : ITaskRepository
{
    
    private readonly List<TaskItem> _tasks = new();

    public Task<IEnumerable<TaskItem>> GetAllAsync(string? status = null)
    {
        IEnumerable<TaskItem> result = _tasks;
        if (status == "completed") result = result.Where(t => t.IsCompleted);
        else if (status == "pending") result = result.Where(t => !t.IsCompleted);
        return Task.FromResult(result);
    }

    public Task<TaskItem?> GetByIdAsync(Guid id) =>
        Task.FromResult(_tasks.FirstOrDefault(t => t.Id == id));

    public Task<TaskItem> CreateAsync(TaskItem task)
    {
        task.Id = Guid.NewGuid();
        task.CreatedAt = DateTime.UtcNow;
        _tasks.Add(task);
        return Task.FromResult(task);
    }

    public Task<bool> UpdateAsync(TaskItem task)
    {
        var index = _tasks.FindIndex(t => t.Id == task.Id);
        if (index == -1) return Task.FromResult(false);

        task.CompletedAt = task.IsCompleted ? DateTime.UtcNow : null;
        _tasks[index] = task;
        return Task.FromResult(true);
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);
        if (task == null) return Task.FromResult(false);
        _tasks.Remove(task);
        return Task.FromResult(true);
    }
    
    
    
    // private readonly IDataContext _dataContext;
    //
    //
    // public TaskRepository(IDataContext dataContext)
    // {
    //     _dataContext = dataContext;
    // }
    
    
    // public IEnumerable<TaskItem> GetAll()
    // {
    //     return _dataContext.Tasks;
    // }
    
    

    // public TaskItem? GetById(Guid id)
    // {
    //     // TODO: Implement
    //     throw new NotImplementedException();
    // }
    //
    // public TaskItem Create(TaskItem task)
    // {
    //     // TODO: Implement
    //     throw new NotImplementedException();
    // }
    //
    // public bool Update(Guid  id, TaskItem task)
    // {
    //     // TODO: Implement
    //     throw new NotImplementedException();
    // }
    //
    // public bool Delete(Guid id)
    // {
    //     // TODO: Implement
    //     return false;
    // }
}