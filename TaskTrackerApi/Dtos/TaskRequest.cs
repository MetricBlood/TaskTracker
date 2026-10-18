using System.ComponentModel.DataAnnotations;
using TaskTrackerApi.Models;

namespace TaskTrackerApi.Dtos;

public class TaskRequest
{
    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [StringLength(1000)]
    public string? Description { get; set; }

    [EnumDataType(typeof(TaskItemStatus))]
    public TaskItemStatus Status { get; set; } = TaskItemStatus.Todo;

    public DateTime? DueDate { get; set; }
}