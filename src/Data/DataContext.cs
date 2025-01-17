
using Microsoft.EntityFrameworkCore;
using userManagementSystemBack.src.Data.Maps;
using userManagementSystemBack.src.Models;


/**
** @author Ramadan Ismael
*/
namespace userManagementSystemBack.src.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions <DataContext> ram) : base(ram)
        {}
        public required DbSet<UserModel> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder model)
        {
            try
            {
                model.ApplyConfiguration(new UserMap());
                base.OnModelCreating(model);
            }
            catch (Exception error) { throw new Exception($@"Error : {error.Message}"); }
        }
    }
}