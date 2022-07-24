namespace BraveFish.WorkflowMaster.Models
{
    public class TransitionCreateRequestModel : PipelineShiftRequestModel
    {
        public string FromState { get; set; }
    }
}
