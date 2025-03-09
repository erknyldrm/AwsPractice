using System.Runtime.CompilerServices;
using Amazon.SQS;
using Amazon.SQS.Model;

var sqsClient = new AmazonSQSClient();

var queueUrlResponse = await sqsClient.GetQueueUrlAsync("TestQueue");

var receiveMessageRequest = new ReceiveMessageRequest
{
    QueueUrl = queueUrlResponse.QueueUrl,
    MessageSystemAttributeNames = ["All"],
    MessageAttributeNames = ["All"]
};

var cts = new CancellationTokenSource();

while(!cts.IsCancellationRequested)
{
    var response = await sqsClient.ReceiveMessageAsync(receiveMessageRequest);

    foreach(var message in response.Messages)
    {
        Console.WriteLine($"Message Id:{message.MessageId}");

        await sqsClient.DeleteMessageAsync(queueUrlResponse.QueueUrl, message.ReceiptHandle);
    }

    await Task.Delay(100, cts.Token);
}
