namespace BraveFish.WorkflowMaster.Utilities
{
    public static class AppConstants
    {
        public static class EntityFramework
        {
            public static class IdPrefixes
            {
                public const string PLAN = "PLAN";

                public const string PIPELINE = "PPLN";

                public const string TRANSITION = "TRST";
            }
        }

        public static class ErrorStatus
        {
            public const string BAD_REQUEST = "BAD_REQUEST";

            public const string UNHANDLED = "UNHANDLED";
        }

        public static class Pipeline
        {
            public static class Status
            {
                public const string INIT = "INIT";
            }
        }
    }
}
