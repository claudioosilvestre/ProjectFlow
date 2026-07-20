using ProjectFlow.Domain.Entities;

namespace ProjectFlow.Application.Projects.CreateProject;

public class CreateProjectCommandHandler
{
    private readonly IProjectRepository _projectRepository;

    public CreateProjectCommandHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public ProjectResponse Handle(CreateProjectCommand command)
    {
        var project = new Project(command.Name, command.Description);

        _projectRepository.Save(project);
    }
}