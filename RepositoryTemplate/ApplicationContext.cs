using Microsoft.EntityFrameworkCore;

namespace RepositoryTemplate
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }
    }
}