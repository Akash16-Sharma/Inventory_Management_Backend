
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
        public DbSet<User_Info> User_Infos { get; set; }
        public DbSet<User_Login> User_Login { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<UnitType> Unit_Type { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Vendor> Vendor { get; set; }
    }
}
