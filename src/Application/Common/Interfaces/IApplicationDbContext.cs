using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<ApplicationUser> Users { get; }
    DbSet<Game> Games { get; }
    DbSet<Player> Players { get; }

    Task<int> SaveChangesAsync(CancellationToken ct = new());
}