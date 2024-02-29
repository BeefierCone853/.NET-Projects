using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class IdentityDataContext(DbContextOptions<IdentityDataContext> options) : IdentityDbContext(options);