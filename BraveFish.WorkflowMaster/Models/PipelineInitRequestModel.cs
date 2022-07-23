namespace BraveFish.WorkflowMaster.Models
{
    public class PipelineInitRequestModel
    {
        public string PlanId { get; set; }

        public Dictionary<string, string> Params { get; set; }
    }
}
