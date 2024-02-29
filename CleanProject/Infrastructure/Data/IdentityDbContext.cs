using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

/// <inheritdoc cref="IdentityDbContext"/>
/// <param name="options">Database options.</param>
public class IdentityDataContext(DbContextOptions<IdentityDataContext> options) : IdentityDbContext(options);