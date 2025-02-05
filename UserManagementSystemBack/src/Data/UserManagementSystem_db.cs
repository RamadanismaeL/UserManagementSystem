/*
*@author Ramadan Ismael
*/

using Microsoft.EntityFrameworkCore;
using UserManagementSystemBack.src.Configs;
using UserManagementSystemBack.src.Models;

namespace UserManagementSystemBack.src.Data
{
    public class UserManagementSystem_db : DbContext
    {
        public UserManagementSystem_db(DbContextOptions<UserManagementSystem_db> options) : base(options)
        {}
        public required DbSet<UserModel> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            try
            {
                modelBuilder.ApplyConfiguration(new UserConfig());
                base.OnModelCreating(modelBuilder);
            }
            catch(Exception error)
            {
                throw new Exception("Error to create table", error);
            }
        }
    }
}