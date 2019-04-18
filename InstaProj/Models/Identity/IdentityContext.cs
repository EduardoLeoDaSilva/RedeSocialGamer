using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaProj.Models.Identity
{
    public class IdentityContext: IdentityDbContext<UsuarioIdentity>
    {

        public IdentityContext(DbContextOptions<IdentityContext> dbContext):base(dbContext)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
        }



    }
}
