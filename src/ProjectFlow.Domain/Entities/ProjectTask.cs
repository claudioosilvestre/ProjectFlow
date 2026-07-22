using ProjectFlow.Domain.Enums;

namespace ProjectFlow.Domain.Entities;

public class ProjectTask
{
    public Guid Id{get; private set;}

    public string Title{get; private set;}

    public string Description{get; private set;}

    public ProjectTaskStatus Status{get; private set;}

    public DateTimeOffset CreatedAt{get; private set;}

    public Guid? ProjectId { get; private set; }

    private ProjectTask()
    {
    }

    public ProjectTask(string title, string description)
    {
        if(string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Task name cannot be empty.");
        }

        string normalizedTitle = title.Trim();

        Id = Guid.NewGuid();

        CreatedAt = DateTimeOffset.UtcNow;

        Status = ProjectTaskStatus.ToDo;

        Title = normalizedTitle;
        Description = description;
    }

    public void ChangeTitle(string newTitle)
    {
        if(string.IsNullOrWhiteSpace(newTitle))
        {
            throw new ArgumentException("Title must be valid");
        }

        string newTitleNormalized = newTitle.Trim();

        if(Title.Equals(newTitleNormalized))
        {
            throw new ArgumentException("New title is the same");
        }

        Title = newTitleNormalized;
    }

    public void ChangeDescription(string newDescription)
    {
        Description = newDescription;
    }

    public void ChangeStatus(ProjectTaskStatus newStatus)
    {
        if(Status == newStatus)
        {
            throw new InvalidOperationException("The task is already in this status.");
        }

        if(!CanChangeStatus(newStatus))
        {
            throw new InvalidOperationException($"Cannot change status from {Status} to {newStatus}");
        }

        Status = newStatus;
    }

    internal void AssignToProject(Guid projectId)
    {
        if(ProjectId.HasValue)
        {
            throw new InvalidOperationException("Task already belongs to a project.");
        }

        ProjectId = projectId;
    }

    private bool CanChangeStatus(ProjectTaskStatus newStatus)
    {
    return (Status, newStatus) switch
    {
        (ProjectTaskStatus.ToDo, ProjectTaskStatus.InProgress) => true,
        (ProjectTaskStatus.InProgress, ProjectTaskStatus.Done) => true,
        _ => false
    };
    }
}