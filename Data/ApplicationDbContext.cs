namespace EventoContabil.Data
{
    using EventoContabil.Models;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Evento> Eventos { get; set; }
    }

}