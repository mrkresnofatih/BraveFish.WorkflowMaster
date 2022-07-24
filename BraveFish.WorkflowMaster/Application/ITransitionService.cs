using BraveFish.WorkflowMaster.Models;

namespace BraveFish.WorkflowMaster.Application
{
    public interface ITransitionService
    {
        Task<TransitionCreateResponseModel> Create(TransitionCreateRequestModel createRequest);
    }
}
