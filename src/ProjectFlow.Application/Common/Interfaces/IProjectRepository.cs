using ProjectFlow.Domain.Entities;

namespace ProjectFlow.Application.Common.Interfaces;

public interface IProjectRepository
{
    Task AddAsync(Project project);

    Task<Project?> GetByIdAsync(Guid id);

    Task DeleteAsync(Project project);
}