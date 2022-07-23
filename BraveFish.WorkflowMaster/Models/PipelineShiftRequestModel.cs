namespace BraveFish.WorkflowMaster.Models
{
    public class PipelineShiftRequestModel
    {
        public string PipelineId { get; set; }

        public string ToState { get; set; }

        public Dictionary<string, string> Params { get; set; }
    }
}
