using System;

namespace DynamoDbTestApi.Dtos;

public sealed record UpdateTestDto(Guid Id,string Name);