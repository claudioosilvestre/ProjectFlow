using ProjectFlow.Application.Common.Interfaces;
using ProjectFlow.Domain.Entities;

namespace ProjectFlow.Infrastructure.Persistence.Repositories;

public class ProjectRepository : IProjectRepository
{

    private readonly AppDbContext _context;

    public ProjectRepository(AppDbContext appDbContext)
    {
        _context = appDbContext;
    }
    public async Task AddAsync(Project project)
    {
        _context.Projects.Add(project);

        
        await _context.SaveChangesAsync();
        
    }

    public Task DeleteAsync(Project project)
    {
        throw new NotImplementedException();
    }

    public Task<Project?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}

