namespace BraveFish.WorkflowMaster.Utilities
{
    public static class IdGenerator
    {
        private static string GenerateId(string prefix)
        {
            return $"{prefix}-{Guid.NewGuid().ToString()}";
        }

        public static string GeneratePlanId()
        {
            return GenerateId(AppConstants.EntityFramework.IdPrefixes.PLAN);
        }

        public static string GeneratePipelineId()
        {
            return GenerateId(AppConstants.EntityFramework.IdPrefixes.PIPELINE);
        }

        public static string GenerateTransitionId()
        {
            return GenerateId(AppConstants.EntityFramework.IdPrefixes.TRANSITION);
        }
    }
}
