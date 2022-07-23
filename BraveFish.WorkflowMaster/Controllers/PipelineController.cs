using BraveFish.Base;
using BraveFish.WorkflowMaster.Application;
using BraveFish.WorkflowMaster.Models;
using Microsoft.AspNetCore.Mvc;

namespace BraveFish.WorkflowMaster.Controllers
{
    [ApiController]
    [Route("api/workflow/pipeline")]
    public class PipelineController : ControllerBase
    {
        private readonly IPipelineService _pipelineService;

        public PipelineController(IPipelineService pipelineService)
        {
            _pipelineService = pipelineService;
        }

        [HttpPost("init")]
        public async Task<BaseResponseModel<PipelineInitShiftResponseModel>> Init([FromBody] PipelineInitRequestModel initRequest)
        {
            var pipelineInitResponse = await _pipelineService.Init(initRequest);
            return ResponseWrapper.WrapSuccess(pipelineInitResponse);
        }

        [HttpPost("shift")]
        public async Task<BaseResponseModel<PipelineInitShiftResponseModel>> Shift([FromBody] PipelineShiftRequestModel shiftRequest)
        {
            var updatedPipeline = await _pipelineService.Shift(shiftRequest);
            return ResponseWrapper.WrapSuccess(updatedPipeline);
        }
    }
}
