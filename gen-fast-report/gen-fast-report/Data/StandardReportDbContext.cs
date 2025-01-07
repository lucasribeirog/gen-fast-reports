using gen_fast_report.Enums;
using gen_fast_report.Models;
using Microsoft.EntityFrameworkCore;

namespace gen_fast_report.Data
{
    public class StandardReportDbContext(DbContextOptions<StandardReportDbContext> options):DbContext(options)
    {
        public DbSet<StandardReport> StandardReports => Set<StandardReport>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StandardReport>().HasData(
                new StandardReport
                {
                    Id = 1,
                    Name = "Padrao Balistica",
                    Area = Area.Balistica
                },
                new StandardReport
                {
                    Id = 2,
                    Name = "Padrao Trânsito",
                    Area = Area.Transito
                },
                new StandardReport
                {
                    Id = 3,
                    Name = "Padrao Vida",
                    Area = Area.Vida
                },
                new StandardReport
                {
                    Id = 4,
                    Name = "Padrao Documentoscopia",
                    Area = Area.Documentoscopia
                },
                new StandardReport
                {
                    Id = 5,
                    Name = "Padrao Meio Ambiente",
                    Area = Area.MeioAmbiente
                });
        }
    }
}
