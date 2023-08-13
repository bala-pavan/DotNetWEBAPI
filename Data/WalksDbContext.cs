using Microsoft.EntityFrameworkCore;
using WebSampleApplicationAPI.Models.Domain;

namespace WebSampleApplicationAPI.Data
{
    public class WalksDbContext: DbContext
    {
        public WalksDbContext(DbContextOptions<WalksDbContext> dbContextOptions): base(dbContextOptions)
        {
            
        }

        //dbsets -collections of entities in db
        public DbSet<Difficulty> Difficulties{ get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }

        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seed data for difficulties
            //Easy, medium, Hard
            var difficulties =new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id= Guid.Parse("bb888a63-824e-47d9-9b6d-e0ba7a6fc477"),
                    Name="Easy"
                },
                new Difficulty()
                {
                    Id= Guid.Parse("74e89cfd-3444-4035-a124-94c7758ad2ad"),
                    Name="Medium"
                },
                new Difficulty()
                {
                    Id= Guid.Parse("5c9ce373-4a0e-43ba-9a64-9a1a1c3f84fb"),
                    Name="Hard"
                }
            };
            //Seed difficulties to the database
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            //Seed data for Regions
            var regions = new List<Region>()
            {
                new Region()
                {
                    Id= Guid.Parse("296575e8-24a2-407b-a887-57ab7beb9562"),
                    Code= "BOB R",
                    Name= "Bay of Bengal",
                    RegionImageUrl=null
                },
                 new Region()
                {
                    Id= Guid.Parse("fbb75597-89c6-4510-9fc9-471c37ae0bd7"),
                    Code= "BOB R",
                    Name= "Bank of Bengal",
                    RegionImageUrl=null
                }
            };
            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}
