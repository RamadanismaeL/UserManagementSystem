/*
*@author Ramadan Ismael
*/

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagementSystemBack.src.Enums;
using UserManagementSystemBack.src.Models;

namespace UserManagementSystemBack.src.Configs
{
    public class UserConfig : IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {
            try
            {
                builder.ToTable("tbUsers");

                builder.Property(u => u.Id)
                .HasColumnName("id")
                .HasColumnType("bigint unsigned")
                .ValueGeneratedOnAdd()
                .IsRequired();
                builder.HasKey(u => u.Id);

                builder.Property(u => u.FirstName)
                .HasColumnName("firstName")
                .HasColumnType("varchar(50)")
                .HasMaxLength(50)
                .IsRequired();

                builder.Property(u => u.LastName)
                .HasColumnName("lastName")
                .HasColumnType("varchar(30)")
                .HasMaxLength(30)
                .IsRequired();

                builder.Property(u => u.PhoneNumber)
                .HasColumnName("phoneNumber")
                .HasColumnType("varchar(20)")
                .HasMaxLength(25);

                builder.Property(u => u.Email)
                .HasColumnName("email")
                .HasColumnType("varchar(50)")
                .HasMaxLength(50)
                .IsRequired();
                builder.HasIndex(u => u.Email).IsUnique();

                builder.Property(u => u.UserName)
                .HasColumnName("userName")
                .HasColumnType("varchar(30)")
                .HasMaxLength(30)
                .IsRequired();

                builder.Property(u => u.Password)
                .HasColumnName("password")
                .HasColumnType("varchar(255)")
                .HasMaxLength(255)
                .IsRequired();

                builder.Property<UserProfileEnum>(u => u.Profile)
                .HasColumnName("profile")
                .HasDefaultValue(UserProfileEnum.User)
                .IsRequired();

                builder.Property<UserStatusEnum>(u => u.Status)
                .HasColumnName("status")
                .HasDefaultValue(UserStatusEnum.Active)
                .IsRequired();

                builder.Property<DateTime>(u => u.DateRegister)
                .HasColumnName("dateRegister")
                .HasColumnType("datetime")
                .HasDefaultValueSql("current_timestamp")
                .IsRequired();

                builder.Property<DateTime?>(u => u.DateUpdate)
                .HasColumnName("dateUpdate")
                .HasColumnType("datetime");
            }
            catch(Exception error)
            {
                throw new Exception("Error to configure UserConfig", error);
            }
        }
    }
}