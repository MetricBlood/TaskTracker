using TaskTrackerApi.Models;

namespace TaskTrackerApi.Data;

public class InMemoryData
{
    public List<Project> Projects { get; } = new();

    private int _nextProjectId = 1;
    private int _nextTaskId = 1;

    public int NextProjectId() => _nextProjectId++;
    public int NextTaskId() => _nextTaskId++;

    public InMemoryData()
    {
        var website = new Project
        {
            Id = NextProjectId(),
            Name = "Website Redesign",
            Description = "Refresh the company website with a modern layout",
            CreatedAt = DateTime.UtcNow
        };
        AddSampleTask(website, "Create wireframes", TaskItemStatus.Done);
        AddSampleTask(website, "Design homepage mockup", TaskItemStatus.InProgress, DateTime.UtcNow.AddDays(3));
        AddSampleTask(website, "Write About Us content", TaskItemStatus.Todo, DateTime.UtcNow.AddDays(7), "Get company history from marketing");

        var mobileApp = new Project
        {
            Id = NextProjectId(),
            Name = "Mobile App Launch",
            CreatedAt = DateTime.UtcNow
        };
        AddSampleTask(mobileApp, "Fix login bug", TaskItemStatus.Todo, DateTime.UtcNow.AddDays(1));

        Projects.Add(website);
        Projects.Add(mobileApp);
    }

    private void AddSampleTask(Project project, string title, TaskItemStatus status,
        DateTime? dueDate = null, string? description = null)
    {
        project.Tasks.Add(new TaskItem
        {
            Id = NextTaskId(),
            ProjectId = project.Id,
            Title = title,
            Description = description,
            Status = status,
            DueDate = dueDate,
            CreatedAt = DateTime.UtcNow
        });
    }
}