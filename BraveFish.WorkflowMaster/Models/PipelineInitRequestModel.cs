using System.ComponentModel.DataAnnotations;

namespace BraveFish.WorkflowMaster.Models
{
    public class PipelineInitRequestModel
    {
        [Required]
        public string PlanId { get; set; }

        [Required]
        public Dictionary<string, string> Params { get; set; }
    }
}
