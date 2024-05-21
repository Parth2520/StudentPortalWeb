using Microsoft.EntityFrameworkCore;
using StudentPortalWeb.Models.Entities;

namespace StudentPortalWeb.Data
{
    public class ApplicationDBContext : DbContext

    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext>options):base(options) 
        {
            
        }
        public  DbSet<Student> Students { get; set; }
        }
    
}
