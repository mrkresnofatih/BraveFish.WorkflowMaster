using BraveFish.WorkflowMaster.Entities;
using Microsoft.EntityFrameworkCore;

namespace BraveFish.WorkflowMaster.EntityFramework
{
    public class WorkflowDbContext : DbContext
    {
        public WorkflowDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Plan> Plans { get; set; }

        public DbSet<Pipeline> Pipelines { get; set; }

        public DbSet<Transition> Transitions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pipeline>()
                .HasOne(p => p.Plan)
                .WithMany(p => p.Pipelines)
                .HasForeignKey(p => p.PlanId);

            modelBuilder.Entity<Transition>()
                .HasOne(p => p.Pipeline)
                .WithMany(p => p.Transitions)
                .HasForeignKey(p => p.PipelineId);
        }
    }
}
