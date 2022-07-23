namespace BraveFish.WorkflowMaster.Models
{
    public class PlanRegisterResponseModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public PlanDefinition PlanDefinition { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsDeprecated { get; set; } = false;
    }
}
