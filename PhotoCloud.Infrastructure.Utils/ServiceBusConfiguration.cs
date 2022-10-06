using Azure.Messaging.ServiceBus.Administration;

namespace PhotoCloud.Infrastructure.Utils;

public static class ServiceBusConfiguration
{
    public static async Task InfrastructureQueue(string connectionString, string queueName)
    {
        var client = new ServiceBusAdministrationClient(connectionString);

        if (await client.QueueExistsAsync(queueName))
        {
            await client.DeleteQueueAsync(queueName);
        }

        await client.CreateQueueAsync(queueName);
    }

    public static async Task InfrastructurePubSub(string connectionString, string topicName, string rushSubscription)
    {
        var client = await Cleanup(connectionString, topicName, rushSubscription);
        
        var subscriptionOptions = new CreateSubscriptionOptions(topicName, rushSubscription);
        await client.CreateSubscriptionAsync(subscriptionOptions);
    }

    private static async Task<ServiceBusAdministrationClient> Cleanup(string connectionString, string topicName,
        string rushSubscription)
    {
        var client = new ServiceBusAdministrationClient(connectionString);

        if (await client.SubscriptionExistsAsync(topicName, rushSubscription))
        {
            await client.DeleteSubscriptionAsync(topicName, rushSubscription);
        }


        if (await client.TopicExistsAsync(topicName))
        {
            await client.DeleteTopicAsync(topicName);
        }

        var topicDescription = new CreateTopicOptions(topicName);
        await client.CreateTopicAsync(topicDescription);

        return client;
    }
}