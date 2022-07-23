using BraveFish.WorkflowMaster.Models;

namespace BraveFish.WorkflowMaster.Application
{
    public interface IPipelineService
    {
        Task<PipelineInitShiftResponseModel> Init(PipelineInitRequestModel initRequest);

        Task<PipelineInitShiftResponseModel> Shift(PipelineShiftRequestModel shiftRequest);
    }
}
