using BraveFish.Base;
using BraveFish.WorkflowMaster.Exceptions;

namespace BraveFish.WorkflowMaster.Utilities
{
    public static class AppExceptionHandler
    {
        public static void UseAppExceptionHandler(this IApplicationBuilder app)
        {
            var exceptionHandlerConfig = new BraveFishExceptionHandlerConfigurationBuilder()
                .SetContentType("application/json")
                .SetStatusCode(200)
                .SetUnhandledStatus(AppConstants.ErrorStatus.UNHANDLED)
                .SetUnhandledStatusCode(400)

                .AddExceptionToHandle(typeof(BadRequestException), AppConstants.ErrorStatus.BAD_REQUEST)
                .AddExceptionToHandle(typeof(RecordNotFoundException), AppConstants.ErrorStatus.BAD_REQUEST)

                .Build();
            app.UseBaseExceptionHandler(exceptionHandlerConfig);
        }
    }
}
