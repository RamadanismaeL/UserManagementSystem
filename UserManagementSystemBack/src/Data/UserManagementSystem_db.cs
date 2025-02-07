/*
*@author Ramadan Ismael
*/

using Microsoft.EntityFrameworkCore;
using UserManagementSystemBack.src.Configs;
using UserManagementSystemBack.src.Models;

namespace UserManagementSystemBack.src.Data
{
    /// <summary>
    /// Project DataBase
    /// </summary>
    public class UserManagementSystem_db : DbContext
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public UserManagementSystem_db(DbContextOptions<UserManagementSystem_db> options) : base(options)
        {}
        /// <summary>
        /// Send all configuration to database
        /// </summary>
        /// <value></value>
        public required DbSet<UserModel> Users { get; set; }

        /// <summary>
        /// Simple method to config
        /// </summary>
        /// <param name="modelBuilder"></param>
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