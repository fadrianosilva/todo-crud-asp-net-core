using FSTodoList.EntityConfigs;
using FSTodoList.Models;
using Microsoft.EntityFrameworkCore;

namespace FSTodoList.Contexts;

public class AppDbContext : DbContext
{
    public DbSet<Todo> Todos => Set<Todo>();

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseSqlServer("Server=localhost;Database=FSTodoList;Trusted_Connection=true;MultipleActiveResultSets=true;TrustServerCertificate=True;user id=sa;password=Br@sil01");
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new TodoEntityConfig());
    }

}