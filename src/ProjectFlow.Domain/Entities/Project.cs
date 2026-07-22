using ProjectFlow.Domain.Enums;

namespace ProjectFlow.Domain.Entities;

public class Project
{
    public Guid Id{get; private set;}

    public string Name{get; private set;}

    public string Description{get; private set;}

    public ProjectStatus Status{get; private set;}

    public DateTimeOffset CreatedAt{get; private set;}

    private readonly List<ProjectTask> _tasks = new();

    public IReadOnlyCollection<ProjectTask> Tasks => _tasks.AsReadOnly();

    private Project()
    {
        // Required by Entity Framework
    }
    public Project(string name, string description)
    {
        if(string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Project name cannot be empty.");
        }

        string normalizedName = name.Trim();

        Id = Guid.NewGuid();

        CreatedAt = DateTimeOffset.UtcNow;

        Status = ProjectStatus.ToDo;

        Name = normalizedName;
        Description = description;
    }

    public void AddTask(ProjectTask task)
    {
        if(task == null)
        {
            throw new ArgumentNullException(nameof(task));
        }

        if(_tasks.Contains(task))
        {
            throw new InvalidOperationException("Task already exists in this project.");
        }

        if(Status == ProjectStatus.Done)
        {
            throw new InvalidOperationException("Cannot add tasks to a completed project.");
        }

        task.AssignToProject(Id);

        _tasks.Add(task);
    }

    public void ChangeName(string newName)
    {
        if(string.IsNullOrWhiteSpace(newName))
        {
            throw new ArgumentException("New name is invalid.");
        }

        string normalizedName = newName.Trim();

        if(Name.Equals(normalizedName))
        {
            throw new ArgumentException("New name is equal");
        }

        Name = normalizedName;
    }

    public void ChangeDescription(string newDescription)
    {
        Description = newDescription;
    }

    public void ChangeStatus(ProjectStatus newStatus)
    {
        if(Status == newStatus)
        {
            throw new InvalidOperationException("The project is already in this status.");
        }

        if(!CanChangeStatus(newStatus))
        {
            throw new InvalidOperationException($"Cannot change status from {Status} to {newStatus}");
        }

        Status = newStatus;
    }

    private bool CanChangeStatus(ProjectStatus newStatus)
    {
        return (Status, newStatus) switch
        {
            (ProjectStatus.ToDo, ProjectStatus.InProgress) => true,
            (ProjectStatus.InProgress, ProjectStatus.Done) => true,
            _ => false
        };
    }
}