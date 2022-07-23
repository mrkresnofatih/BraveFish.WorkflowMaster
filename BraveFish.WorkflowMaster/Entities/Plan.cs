using BraveFish.WorkflowMaster.Utilities;
using System.ComponentModel.DataAnnotations;

namespace BraveFish.WorkflowMaster.Entities
{
    public class Plan
    {
        [Key]
        [Required]
        public string Id { get; set; } = IdGenerator.GeneratePlanId();

        [Required]
        [StringLength(50, MinimumLength = 6)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        [Required]
        public string JsonDefinition { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public bool IsDeprecated { get; set; } = false;

        public List<Pipeline> Pipelines { get; set; }
    }
}
