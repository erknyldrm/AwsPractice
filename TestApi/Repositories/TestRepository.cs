
using System.Text.Json;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using DynamoDbTestApi.Dtos;
using DynamoDbTestApi.Models;

namespace DynamoDbTestApi.Repositories;

public sealed class TestRepository(IAmazonDynamoDB amazonDynamoDB)
{

    private readonly string tableName = "testdb";


    public async Task<Boolean> CreateAsync(CreateTestDto request)
    {
        Test test = new()
        {
            Name = request.Name
        };

        var testAsJon = JsonSerializer.Serialize(test);

        var customerAsAttributes = Document.FromJson(testAsJon).ToAttributeMap();

        var itemRequest = new PutItemRequest
        {
            TableName = tableName,
            Item = customerAsAttributes,
            ConditionExpression = "attribute_not_exists(pk) and attribute_not_exists(sk)"
        };

        var response = await amazonDynamoDB.PutItemAsync(itemRequest);

        return response.HttpStatusCode == System.Net.HttpStatusCode.OK;

    }

    public async Task<Boolean> UpdateAsync(UpdateTestDto request, DateTime requestStarted)
    {
        Test test = new()
        {
            Id = request.Id,
            Name = request.Name,
            UpdateAt = DateTime.UtcNow
        };

        var testAsJon = JsonSerializer.Serialize(test);

        var customerAsAttributes = Document.FromJson(testAsJon).ToAttributeMap();

        var itemRequest = new PutItemRequest
        {
            TableName = tableName,
            Item = customerAsAttributes,
            ConditionExpression = "UpdatedAt < :requestStarted",
            ExpressionAttributeValues = new Dictionary<string, AttributeValue>
            {
               {
                 ":requestStarted", new AttributeValue{S = requestStarted.ToString("O")}
               }
            }
        };

        var response = await amazonDynamoDB.PutItemAsync(itemRequest);

        return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
    }

    public async Task<Boolean> DeleteByIdAsync(Guid guid)
    {
        var deletedItem = new DeleteItemRequest
        {
            TableName = tableName,
            Key = new Dictionary<string, AttributeValue>
            {
                {"pk", new AttributeValue { S = guid.ToString()}},
                {"sk", new AttributeValue { S = guid.ToString()}}
            }
        };

        var response = await amazonDynamoDB.DeleteItemAsync(deletedItem);

        return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
    }

    public async Task<IEnumerable<TestDto>> GetAllAsync()
    {
        var scanRequest = new ScanRequest
        {
            TableName = tableName
        };

        var response = await amazonDynamoDB.ScanAsync(scanRequest);

        // bu yöntem gerçek uygulamalarda kullanılmamalı, sürekli istek atıldığında maliyet artmakta.
        return response.Items.Select(s =>
        {
            var json = Document.FromAttributeMap(s).ToJson();
            return JsonSerializer.Deserialize<TestDto>(json);
        })!;
    }
}
