using Microsoft.AspNetCore.Mvc;
using TaskTrackerApi.Data;
using TaskTrackerApi.Dtos;
using TaskTrackerApi.Models;

namespace TaskTrackerApi.Controllers;

[ApiController]
[Route("api")]
public class TasksController : ControllerBase
{
    private readonly InMemoryData _data;

    public TasksController(InMemoryData data)
    {
        _data = data;
    }

    // GET /api/projects/1/tasks
    [HttpGet("projects/{projectId:int}/tasks")]
    public ActionResult<List<TaskItem>> GetByProject(int projectId)
    {
        var project = _data.Projects.FirstOrDefault(p => p.Id == projectId);
        if (project == null)
        {
            return NotFound();
        }

        return Ok(project.Tasks);
    }

    // GET /api/tasks/1
    [HttpGet("tasks/{id:int}")]
    public ActionResult<TaskItem> GetById(int id)
    {
        var task = FindTask(id);
        if (task == null)
        {
            return NotFound();
        }

        return Ok(task);
    }

    // POST /api/projects/1/tasks
    [HttpPost("projects/{projectId:int}/tasks")]
    public ActionResult<TaskItem> Create(int projectId, TaskRequest request)
    {
        var project = _data.Projects.FirstOrDefault(p => p.Id == projectId);
        if (project == null)
        {
            return NotFound();
        }

        var task = new TaskItem
        {
            Id = _data.NextTaskId(),
            ProjectId = project.Id,
            Title = request.Title,
            Description = request.Description,
            Status = request.Status,
            DueDate = request.DueDate,
            CreatedAt = DateTime.UtcNow
        };

        project.Tasks.Add(task);

        return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
    }

    // PUT /api/tasks/1
    [HttpPut("tasks/{id:int}")]
    public ActionResult<TaskItem> Update(int id, TaskRequest request)
    {
        var task = FindTask(id);
        if (task == null)
        {
            return NotFound();
        }

        task.Title = request.Title;
        task.Description = request.Description;
        task.Status = request.Status;
        task.DueDate = request.DueDate;

        return Ok(task);
    }

    // DELETE /api/tasks/1
    [HttpDelete("tasks/{id:int}")]
    public IActionResult Delete(int id)
    {
        var task = FindTask(id);
        if (task == null)
        {
            return NotFound();
        }

        var project = _data.Projects.First(p => p.Id == task.ProjectId);
        project.Tasks.Remove(task);

        return NoContent();
    }

    private TaskItem? FindTask(int id)
    {
        return _data.Projects
            .SelectMany(p => p.Tasks)
            .FirstOrDefault(t => t.Id == id);
    }
}