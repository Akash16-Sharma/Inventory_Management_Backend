
using BackendAPI.Models.Roles;
using Microsoft.EntityFrameworkCore;

namespace BackendAPI.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Organisation_Info> Organisation_Info { get; set; }
        public DbSet<StateList> State_List { get; set; }
        public DbSet<Cities_List> Cities_List { get; set; }
        public DbSet<User_Info> User_Info { get; set; }
        public DbSet<User_Login> User_Login { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<UnitType> Unit_Type { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Access> StaffAccess { get; set; }
        public DbSet<Vendor> Vendor { get; set; }
        public DbSet<Inc_Order> Inc_Order { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Out_Order> Out_Order { get; set; }
    }
}
