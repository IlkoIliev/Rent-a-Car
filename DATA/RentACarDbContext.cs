using DATA.Entities;
using DATA.Initializers;
using System.Data.Entity;

namespace DATA
{
    public class RentACarDbContext : DbContext
    {
        public RentACarDbContext()
            : base("name=RentACarDbContext")
        {
            Database.SetInitializer(new RentACarDbDropCreateIfModelChanges());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Car>();

            modelBuilder.Entity<Car>().Property(e => e.Id);

            modelBuilder.Entity<Car>().Property(e => e.Brand)
                    .IsRequired()
                    .HasMaxLength(50);

            modelBuilder.Entity<Car>().Property(e => e.Description).HasColumnType("ntext");

            modelBuilder.Entity<Car>().Property(e => e.Model)
                    .IsRequired()
                    .HasMaxLength(50);

            modelBuilder.Entity<Car>().Property(e => e.Price).HasColumnType("smallmoney");

            modelBuilder.Entity<Car>().Property(e => e.Seats);

            modelBuilder.Entity<Car>().Property(e => e.Year);

            /////////////////////////////////////////////////////////////////////////////////
            modelBuilder.Entity<Rent>();

            modelBuilder.Entity<Rent>().Property(e => e.Id);

            modelBuilder.Entity<Rent>().Property(e => e.CarId);

            modelBuilder.Entity<Rent>().Property(e => e.EndDate);

            modelBuilder.Entity<Rent>().Property(e => e.StartDate);

            modelBuilder.Entity<Rent>().Property(e => e.UserId);

            modelBuilder.Entity<Rent>().Property(e => e.State).HasColumnType("tinyint").IsRequired();

            modelBuilder.Entity<Rent>().HasRequired(d => d.Car)
                .WithMany(p => p.Rent)
                .HasForeignKey(d => d.CarId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Rent>().HasRequired(d => d.User)
                .WithMany(p => p.Rent)
                .HasForeignKey(d => d.UserId)
                .WillCascadeOnDelete(false);

            ////////////////////////////////////////////////////////////////////////////////
            modelBuilder.Entity<Role>();

            modelBuilder.Entity<Role>().Property(e => e.Id);

            modelBuilder.Entity<Role>().Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            ////////////////////////////////////////////////////////////////////////////////
            modelBuilder.Entity<User>().Property(e => e.Id);

            modelBuilder.Entity<User>().Property(e => e.Egn)
                    .HasColumnName("EGN")
                    .HasMaxLength(10)
                    .IsFixedLength()
                    .IsRequired();

            modelBuilder.Entity<User>().Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

            modelBuilder.Entity<User>().Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(50);

            modelBuilder.Entity<User>().Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

            modelBuilder.Entity<User>().Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasColumnName("Phone_number")
                    .HasMaxLength(10)
                    .IsFixedLength();

            modelBuilder.Entity<User>().Property(e => e.Sirname)
                    .IsRequired()
                    .HasMaxLength(50);

            modelBuilder.Entity<User>().Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50);

            modelBuilder.Entity<User>().HasRequired(d => d.Role)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.RoleId)
                    .WillCascadeOnDelete(false);


            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Rent> Rents { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
