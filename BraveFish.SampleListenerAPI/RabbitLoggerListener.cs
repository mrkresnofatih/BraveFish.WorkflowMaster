using BraveFish.RabbitBattleGear;
using RabbitMQ.Client.Events;

namespace BraveFish.SampleListenerAPI
{
    public class RabbitLoggerListener : RabbitBattleListener
    {
        private readonly ILogger<RabbitLoggerListener> _logger;

        public RabbitLoggerListener(ILogger<RabbitLoggerListener> logger, RabbitBattleGearContext rabbitBattleGearContext) : base(rabbitBattleGearContext)
        {
            _logger = logger;
        }

        protected override Dictionary<string, Action<string, RabbitBattleGearContext, BasicDeliverEventArgs>> GetBattleMessageHandlers()
        {
            var handlers = new Dictionary<string, Action<string, RabbitBattleGearContext, BasicDeliverEventArgs>>();

            handlers.Add("firstAddress", (message, context, eventArgs) =>
            {
                _logger.LogInformation("FIRST_ADDRESS: " + message);
                context.AcknowledgeMessage(eventArgs);
            });

            handlers.Add("secondAddress", (message, context, eventArgs) =>
            {
                _logger.LogInformation("SECOND_ADDRESS: " + message);
                context.AcknowledgeMessage(eventArgs);
            });

            return handlers;
        }

        protected override string GetQueueNameOfThisListener()
        {
            return "rabbitLogger";
        }
    }
}
