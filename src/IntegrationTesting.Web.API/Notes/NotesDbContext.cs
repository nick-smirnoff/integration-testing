using Microsoft.EntityFrameworkCore;

namespace IntegrationTesting.Web.API.Notes
{
    public class NotesDbContext : DbContext
    {
        public DbSet<Note> Notes { get; set; }

        public NotesDbContext(DbContextOptions options)
            : base(options)
        {
            Notes = Set<Note>();
        }
    }
}
