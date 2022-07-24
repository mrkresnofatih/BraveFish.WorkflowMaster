using BraveFish.WorkflowMaster.Entities;
using BraveFish.WorkflowMaster.EntityFramework;
using BraveFish.WorkflowMaster.Models;
using Newtonsoft.Json;

namespace BraveFish.WorkflowMaster.Application
{
    public class TransitionService : ITransitionService
    {
        private readonly WorkflowDbContext _workflowDbContext;

        public TransitionService(WorkflowDbContext workflowDbContext)
        {
            _workflowDbContext = workflowDbContext;
        }

        public async Task<TransitionCreateResponseModel> Create(TransitionCreateRequestModel createRequest)
        {
            var transition = new Transition
            {
                PipelineId = createRequest.PipelineId,
                FromStatus = createRequest.FromState,
                ToStatus = createRequest.ToState,
                JsonParams = JsonConvert.SerializeObject(createRequest.Params)
            };

            await _workflowDbContext.Transitions.AddAsync(transition);
            await _workflowDbContext.SaveChangesAsync();

            return ToTransitionCreateResponseModel(transition);
        }

        private TransitionCreateResponseModel ToTransitionCreateResponseModel(Transition transition)
        {
            return new TransitionCreateResponseModel
            {
                Id = transition.Id,
                PipelineId = transition.PipelineId,
                CreatedAt = transition.CreatedAt,
                FromState = transition.FromStatus,
                Params = JsonConvert.DeserializeObject<Dictionary<string, string>>(transition.JsonParams),
                ToState = transition.ToStatus
            };
        }
    }
}
