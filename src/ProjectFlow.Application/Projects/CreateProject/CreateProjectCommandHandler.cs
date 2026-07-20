using ProjectFlow.Application.Common.Interfaces;
using ProjectFlow.Domain.Entities;

namespace ProjectFlow.Application.Projects.CreateProject;

public class CreateProjectCommandHandler
{
    private readonly IProjectRepository _projectRepository;

    public CreateProjectCommandHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<ProjectResponse> Handle(CreateProjectCommand command)
    {
        var project = new Project(command.Name, command.Description);

        
        await _projectRepository.AddAsync(project);

        return new ProjectResponse(
            project.Id,
            project.Name,
            project.Description,
            project.Status,
            project.CreatedAt
        );
    }
}