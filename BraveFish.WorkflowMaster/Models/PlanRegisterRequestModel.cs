namespace BraveFish.WorkflowMaster.Models
{
    public class PlanRegisterRequestModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public PlanDefinition PlanDefinition { get; set; }
    }
}
