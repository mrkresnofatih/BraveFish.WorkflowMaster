using BraveFish.Base;
using BraveFish.WorkflowMaster.Application;
using BraveFish.WorkflowMaster.Models;
using Microsoft.AspNetCore.Mvc;

namespace BraveFish.WorkflowMaster.Controllers
{
    [ApiController]
    [Route("api/workflow/plan")]
    public class PlanController : ControllerBase
    {
        private readonly IPlanService _planService;

        public PlanController(IPlanService planService)
        {
            _planService = planService;
        }

        [HttpPost("register")]
        public async Task<BaseResponseModel<PlanRegisterResponseModel>> Register([FromBody] PlanRegisterRequestModel requestModel)
        {
            var planRegisterResponseModel = await _planService.Register(requestModel);
            return ResponseWrapper.WrapSuccess(planRegisterResponseModel);
        }
    }
}
