namespace BraveFish.WorkflowMaster.Models
{
    public class PipelineInitShiftResponseModel
    {
        public string Id { get; set; }

        public string PlanId { get; set; }

        public PlanDefinition PlanDefinition { get; set; }

        public string CurrentStatus { get; set; }

        public Dictionary<string, string> Params { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
