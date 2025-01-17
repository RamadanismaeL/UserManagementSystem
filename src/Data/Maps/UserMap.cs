
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using userManagementSystemBack.src.Enums;
using userManagementSystemBack.src.Models;


/**
** @auhtor Ramadan Ismael
*/
namespace userManagementSystemBack.src.Data.Maps
{
    public class UserMap : IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {
            try
            {
                builder.ToTable("tbUser");

                builder.Property(u => u.Id)
                .HasColumnName("id")
                .HasColumnType("bigint unsigned")
                .ValueGeneratedOnAdd()
                .IsRequired();
                builder.HasKey(u => u.Id);

                builder.Property(u => u.Name)
                .HasColumnName("name")
                .HasColumnType("varchar(45)")
                .HasMaxLength(45)
                .IsRequired();

                builder.Property(u => u.LastName)
                .HasColumnName("lastName")
                .HasColumnType("varchar(45)")
                .HasMaxLength(45)
                .IsRequired();

                builder.Property(u => u.Email)
                .HasColumnName("email")
                .HasColumnType("varchar(50)")
                .HasMaxLength(45)
                .IsRequired();
                builder.HasIndex(u => u.Email).IsUnique();

                builder.Property(u => u.Password)
                .HasColumnName("password")
                .HasColumnType("varchar(121)")
                .HasMaxLength(121)
                .IsRequired();

                builder.Property<UserProfileEnum>(u => u.Profile)
                .HasColumnName("profile")
                .HasDefaultValue(UserProfileEnum.User)
                .IsRequired();

                builder.Property<UserStatusEnum>(u => u.Status)
                .HasColumnName("status")
                .HasDefaultValue(UserStatusEnum.Inactive)
                .IsRequired();

                builder.Property<DateTime>(u => u.DateRegister)
                .HasColumnName("dateRegister")
                .HasColumnType("datetime")
                .HasDefaultValueSql("current_timestamp")
                .IsRequired();

                builder.Property<DateTime>(u => u.DateUpdate)
                .HasColumnType("dateUpdate")
                .HasColumnType("datetime");
            }
            catch (Exception error)
            {
                throw new Exception($@"Error: {error.Message}");
            }
        }
    }
}