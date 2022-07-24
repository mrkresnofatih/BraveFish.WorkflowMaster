using BraveFish.RabbitBattleGear;

namespace BraveFish.WorkflowMaster.Contexts
{
    public static class RabbitContext
    {
        public static void AddRabbitContext(this IServiceCollection services)
        {
            var rabbitContext = new RabbitBattleGearContextBuilder()
                .SetHostName("localhost")
                .SetPort(5672)
                .SetUsername("guest")
                .SetPassword("guest")
                .SetMonoExchangeName("rabbit.ex")
                .Build();
            services.AddSingleton(rabbitContext);
        }
    }
}
