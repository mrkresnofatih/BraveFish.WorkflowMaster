using System.ComponentModel.DataAnnotations;

namespace BraveFish.WorkflowMaster.Models
{
    public class TransitionCreateResponseModel
    {
        public string Id { get; set; }

        public string PipelineId { get; set; }

        public string FromState { get; set; }

        public string ToState { get; set; }

        public Dictionary<string, string> Params { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
