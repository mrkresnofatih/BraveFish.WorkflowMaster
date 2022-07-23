using BraveFish.WorkflowMaster.Models;

namespace BraveFish.WorkflowMaster.Application
{
    public interface IPlanService
    {
        Task<PlanRegisterResponseModel> Register(PlanRegisterRequestModel planRegisterRequest);
    }
}
