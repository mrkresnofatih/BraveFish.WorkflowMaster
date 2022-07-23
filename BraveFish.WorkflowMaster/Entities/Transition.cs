using BraveFish.WorkflowMaster.Utilities;
using System.ComponentModel.DataAnnotations;

namespace BraveFish.WorkflowMaster.Entities
{
    public class Transition
    {
        [Key]
        [Required]
        public string Id { get; set; } = IdGenerator.GenerateTransitionId();

        [Required]
        [StringLength(50, MinimumLength = 6)]
        public string PipelineId { get; set; } = IdGenerator.GenerateTransitionId();

        [Required]
        [StringLength(30)]
        public string FromStatus { get; set; } = AppConstants.Pipeline.Status.INIT;

        [Required]
        [StringLength(30)]
        public string ToStatus { get; set; } = AppConstants.Pipeline.Status.INIT;

        [Required]
        public string JsonParams { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Pipeline Pipeline { get; set; }
    }
}
