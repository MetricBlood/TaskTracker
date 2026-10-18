using Microsoft.AspNetCore.Mvc;
using TaskTrackerApi.Data;
using TaskTrackerApi.Dtos;
using TaskTrackerApi.Models;

namespace TaskTrackerApi.Controllers;

[ApiController]
[Route("api/projects")]
public class ProjectsController : ControllerBase
{
    private readonly InMemoryData _data;

    public ProjectsController(InMemoryData data)
    {
        _data = data;
    }

    // GET /api/projects
    [HttpGet]
    public ActionResult<List<Project>> GetAll()
    {
        return Ok(_data.Projects);
    }

    // GET /api/projects/1
    [HttpGet("{id:int}")]
    public ActionResult<Project> GetById(int id)
    {
        var project = _data.Projects.FirstOrDefault(p => p.Id == id);
        if (project == null)
        {
            return NotFound();
        }

        return Ok(project);
    }

    // POST /api/projects
    [HttpPost]
    public ActionResult<Project> Create(ProjectRequest request)
    {
        var project = new Project
        {
            Id = _data.NextProjectId(),
            Name = request.Name,
            Description = request.Description,
            CreatedAt = DateTime.UtcNow
        };

        _data.Projects.Add(project);

        return CreatedAtAction(nameof(GetById), new { id = project.Id }, project);
    }

    // PUT /api/projects/1
    [HttpPut("{id:int}")]
    public ActionResult<Project> Update(int id, ProjectRequest request)
    {
        var project = _data.Projects.FirstOrDefault(p => p.Id == id);
        if (project == null)
        {
            return NotFound();
        }

        project.Name = request.Name;
        project.Description = request.Description;

        return Ok(project);
    }

    // DELETE /api/projects/1
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var project = _data.Projects.FirstOrDefault(p => p.Id == id);
        if (project == null)
        {
            return NotFound();
        }

        _data.Projects.Remove(project);

        return NoContent();
    }
}