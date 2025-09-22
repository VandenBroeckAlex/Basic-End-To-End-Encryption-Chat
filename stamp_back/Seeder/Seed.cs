using stamp_back.Data;
using stamp_back.Models;
using System.Diagnostics.Metrics;

namespace stamp_back.Seeder
{
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            this.dataContext = context;
        }
        public void SeedDataContext()
        {
            if (!dataContext.Users.Any())
            {
                var users = new List<User>()
                {
                    new User() { UserName = "Test", Email = "Test@test.com", Password = "Password123" },
                    new User() { UserName = "Alice", Email = "alice@example.com", Password = "Password123" },
                    new User() { UserName = "Bob", Email = "bob@example.com", Password = "Password123" },
                    new User() { UserName = "Charlie", Email = "charlie@example.com", Password = "Password123" },
                    new User() { UserName = "Diana", Email = "diana@example.com", Password = "Password123" },
                    new User() { UserName = "Eve", Email = "eve@example.com", Password = "Password123" },
                    new User() { UserName = "Frank", Email = "frank@example.com", Password = "Password123" },
                    new User() { UserName = "Grace", Email = "grace@example.com", Password = "Password123" },
                    new User() { UserName = "Heidi", Email = "heidi@example.com", Password = "Password123" },
                    new User() { UserName = "Ivan", Email = "ivan@example.com", Password = "Password123" },
                    new User() { UserName = "Judy", Email = "judy@example.com", Password = "Password123" }
                };

                dataContext.Users.AddRange(users);
                dataContext.SaveChanges();
            }
        }
    }
}

