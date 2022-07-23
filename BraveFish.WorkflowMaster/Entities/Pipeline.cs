using BraveFish.WorkflowMaster.Utilities;
using System.ComponentModel.DataAnnotations;

namespace BraveFish.WorkflowMaster.Entities
{
    public class Pipeline
    {
        [Key]
        [Required]
        public string Id { get; set; } = IdGenerator.GeneratePipelineId();

        [Required]
        [StringLength(50, MinimumLength = 6)]
        public string PlanId { get; set; } = IdGenerator.GeneratePlanId();

        [Required]
        public string PlanJsonDefinition { get; set; }

        [Required]
        [StringLength(30)]
        public string CurrentStatus { get; set; } = AppConstants.Pipeline.Status.INIT;

        [Required]
        public string JsonParams { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Plan Plan { get; set; }

        public List<Transition> Transitions { get; set; }
    }
}
