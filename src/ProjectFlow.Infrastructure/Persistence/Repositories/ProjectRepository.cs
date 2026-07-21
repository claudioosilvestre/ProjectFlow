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

    public async Task DeleteAsync(Project project)
    {
        _context.Projects.Remove(project);

        await _context.SaveChangesAsync();
    }

    public async Task<Project?> GetByIdAsync(Guid id)
    {
        var project = await _context.FindAsync<Project>(id);

        return project;
    }
}

