﻿using BraveFish.WorkflowMaster.Entities;
using BraveFish.WorkflowMaster.EntityFramework;
using BraveFish.WorkflowMaster.Exceptions;
using BraveFish.WorkflowMaster.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BraveFish.WorkflowMaster.Application
{
    public class PipelineService : IPipelineService
    {
        private readonly WorkflowDbContext _workflowDbContext;

        public PipelineService(WorkflowDbContext workflowDbContext)
        {
            _workflowDbContext = workflowDbContext;
        }

        public async Task<PipelineInitShiftResponseModel> Init(PipelineInitRequestModel initRequest)
        {
            var foundPlan = await _workflowDbContext
                .Plans
                .Where(p => p.Id == initRequest.PlanId)
                .FirstOrDefaultAsync();

            if (foundPlan == null)
            {
                throw new RecordNotFoundException();
            }

            var pipeline = new Pipeline
            {
                PlanId = initRequest.PlanId,
                PlanJsonDefinition = foundPlan.JsonDefinition,
                JsonParams = JsonConvert.SerializeObject(initRequest.Params),
            };

            await _workflowDbContext.Pipelines.AddAsync(pipeline);
            await _workflowDbContext.SaveChangesAsync();

            return ToPipelineInitShiftResponseModel(pipeline);
        }

        public async Task<PipelineInitShiftResponseModel> Shift(PipelineShiftRequestModel shiftRequest)
        {
            var foundPipeline = await _workflowDbContext
                .Pipelines
                .Where(p => p.Id == shiftRequest.PipelineId)
                .FirstOrDefaultAsync();

            if (foundPipeline == null)
            {
                throw new RecordNotFoundException();
            }

            var planDefinition = JsonConvert.DeserializeObject<PlanDefinition>(foundPipeline.PlanJsonDefinition);
            var transitionPossible = (planDefinition
                .Items
                .Where(p => (p.FromState == foundPipeline.CurrentStatus) && (p.StateName == shiftRequest.ToState))
                .Count() == 1);

            if (!transitionPossible)
            {
                throw new BadRequestException();
            }

            foundPipeline.CurrentStatus = shiftRequest.ToState;

            var updatedParams = new Dictionary<string, string>();
            var oldParams = JsonConvert.DeserializeObject<Dictionary<string, string>>(foundPipeline.JsonParams);
            foreach(var oldParam in oldParams)
            {
                updatedParams.Add(oldParam.Key, oldParam.Value);
            }
            foreach (var newParam in shiftRequest.Params)
            {
                updatedParams.Add(newParam.Key, newParam.Value);
            }

            foundPipeline.JsonParams = JsonConvert.SerializeObject(updatedParams);

            _workflowDbContext.Update(foundPipeline);
            await _workflowDbContext.SaveChangesAsync();

            return ToPipelineInitShiftResponseModel(foundPipeline);
        }

        private PipelineInitShiftResponseModel ToPipelineInitShiftResponseModel(Pipeline pipeline)
        {
            return new PipelineInitShiftResponseModel
            {
                Id = pipeline.Id,
                PlanId = pipeline.PlanId,
                PlanDefinition = JsonConvert.DeserializeObject<PlanDefinition>(pipeline.PlanJsonDefinition),
                CurrentStatus = pipeline.CurrentStatus,
                Params = JsonConvert.DeserializeObject<Dictionary<string, string>>(pipeline.JsonParams),
                CreatedAt = pipeline.CreatedAt
            };
        }
    }
}
