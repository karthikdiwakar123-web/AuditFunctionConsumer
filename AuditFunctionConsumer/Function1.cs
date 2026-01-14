using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AuditFunctionConsumer;

public class AuditFunctionConsumer
{
    private readonly ILogger<AuditFunctionConsumer> _logger;

    public AuditFunctionConsumer(ILogger<AuditFunctionConsumer> logger)
    {
        _logger = logger;
    }

    [Function(nameof(AuditFunctionConsumer))]
    public async Task Run(
        [ServiceBusTrigger("%ServiceBusConnection__queueName%", Connection = "ServiceBusConnection")]
        ServiceBusReceivedMessage message,
        ServiceBusMessageActions messageActions)
    {
        _logger.LogInformation("Message ID: {id}", message.MessageId);
        _logger.LogInformation("Message Body: {body}", message.Body);
        _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

        // Complete the message
        await messageActions.CompleteMessageAsync(message);
    }
}