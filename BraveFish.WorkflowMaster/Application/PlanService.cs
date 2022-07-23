using BraveFish.WorkflowMaster.Entities;
using BraveFish.WorkflowMaster.EntityFramework;
using BraveFish.WorkflowMaster.Models;
using Newtonsoft.Json;

namespace BraveFish.WorkflowMaster.Application
{
    public class PlanService : IPlanService
    {
        private readonly WorkflowDbContext _workflowDbContext;

        public PlanService(WorkflowDbContext workflowDbContext)
        {
            _workflowDbContext = workflowDbContext;
        }

        public async Task<PlanRegisterResponseModel> Register(PlanRegisterRequestModel planRegisterRequest)
        {
            var plan = new Plan
            {
                Name = planRegisterRequest.Name,
                Description = planRegisterRequest.Description,
                JsonDefinition = JsonConvert.SerializeObject(planRegisterRequest.PlanDefinition)
            };

            await _workflowDbContext.Plans.AddAsync(plan);
            await _workflowDbContext.SaveChangesAsync();

            return ToRegisterResponseModel(plan);
        }

        private static PlanRegisterResponseModel ToRegisterResponseModel(Plan plan)
        {
            return new PlanRegisterResponseModel
            {
                Id = plan.Id,
                Name = plan.Name,
                Description = plan.Description,
                PlanDefinition = JsonConvert.DeserializeObject<PlanDefinition>(plan.JsonDefinition),
                CreatedAt = plan.CreatedAt,
                IsDeprecated = plan.IsDeprecated,
            };
        }
    }
}
