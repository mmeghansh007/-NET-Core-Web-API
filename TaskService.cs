using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class TaskService
{
    private readonly DbContext _dbContext;

    public TaskService(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    //here I am Changed return type from Task<Task> to Task<MyTask>
    public async Task<MyTask> GetTaskAsync(int id)
    {
        try
        {
            // here I am  Used await with FirstOrDefaultAsync and changed DbSet to explicitly specify MyTask entity
            return await _dbContext.Set<MyTask>().FirstOrDefaultAsync(t => t.Id == id);
        }
        catch (Exception ex)
        {
            // Added error handling
            throw new ApplicationException($"Error fetching task with ID {id}", ex);
        }
    }

    // Here I am Changed return type from List<Task> to Task<List<MyTask>>...
    public async Task<List<MyTask>> GetAllTasksAsync()
    {
        try
        {
            // here Used await with ToListAsync and specified MyTask explicitly...
            return await _dbContext.Set<MyTask>().ToListAsync();
        }
        catch (Exception ex)
        {
            // Added error handling
            throw new ApplicationException("Error fetching all tasks", ex);
        }
    }
}

//test model is added..
public class MyTask
{
    public int Id { get; set; }
    public string Title { get; set; }
}
