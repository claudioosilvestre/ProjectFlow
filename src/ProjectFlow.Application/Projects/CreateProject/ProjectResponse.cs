using ProjectFlow.Domain.Enums;

namespace ProjectFlow.Application.Projects.CreateProject;

public record ProjectResponse(
    Guid Id,
    string Name,
    string Description,
    ProjectStatus Status,
    DateTimeOffset CreatedAt
    );
