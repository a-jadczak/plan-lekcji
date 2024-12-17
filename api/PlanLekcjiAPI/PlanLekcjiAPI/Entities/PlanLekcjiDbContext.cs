using Microsoft.EntityFrameworkCore;

namespace PlanLekcjiAPI.Entities
{
    public class PlanLekcjiDbContext : DbContext
    {
        private const string _connectionString =
            "Server=(localdb)\\mssqllocaldb;Database=Restaurant;Trusted_Connection=True;";
    }
}
