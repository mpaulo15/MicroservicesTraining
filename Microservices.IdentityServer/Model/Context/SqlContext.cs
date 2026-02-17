using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Microservices.IdentityServer.Model.Context
{
    public class SqlContext : IdentityDbContext<ApplicationUser>
    {
        public SqlContext()
        { 
        }
        public SqlContext(DbContextOptions<SqlContext> options) : base(options)
        {
        }

    }
}
