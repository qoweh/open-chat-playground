using Microsoft.Extensions.AI;

using OpenChat.PlaygroundApp.Abstractions;
using OpenChat.PlaygroundApp.Configurations;
using OpenChat.PlaygroundApp.Connectors;

namespace OpenChat.PlaygroundApp.Tests.Abstractions;

public class LanguageModelConnectorTests
{
    private static AppSettings BuildAppSettings(
        ConnectorType connectorType = ConnectorType.GitHubModels,
        string? endpoint = "https://models.github.ai/inference",
        string? token = "test-token",
        string? model = "openai/gpt-4o-mini")
    {
        return new AppSettings
        {
            ConnectorType = connectorType,
            GitHubModels = new GitHubModelsSettings
            {
                Endpoint = endpoint,
                Token = token,
                Model = model
            }
        };
    }

    [Trait("Category", "UnitTest")]
    [Fact]
    public async Task Given_GitHubModels_Settings_When_CreateChatClient_Invoked_Then_It_Should_Return_ChatClient()
    {
        // Arrange
        var settings = BuildAppSettings();

        // Act
        var client = await LanguageModelConnector.CreateChatClientAsync(settings);

        // Assert
        client.ShouldNotBeNull();
        client.ShouldBeAssignableTo<IChatClient>();
    }

    [Trait("Category", "UnitTest")]
    [Fact]
    public void Given_Invalid_Settings_When_CreateChatClientAsync_Invoked_Then_It_Should_Throw()
    {
        // Arrange
        var settings = BuildAppSettings(endpoint: null);

        // Act
        Func<Task> func = async () => await LanguageModelConnector.CreateChatClientAsync(settings);

        // Assert
        func.ShouldThrow<NullReferenceException>()
            .Message.ShouldContain("Object reference not set to an instance of an object.");
    }

    [Trait("Category", "UnitTest")]
    [Fact]
    public void Given_Null_Settings_When_CreateChatClient_Invoked_Then_It_Should_Throw()
    {
        // Arrange
        AppSettings settings = null!;

        // Act
        Func<Task> func = async () => await LanguageModelConnector.CreateChatClientAsync(settings);

        // Assert
        func.ShouldThrow<NullReferenceException>()
            .Message.ShouldContain("Object reference not set to an instance of an object.");
    }

    [Trait("Category", "UnitTest")]
    [Theory]
    [InlineData(ConnectorType.Unknown)]
    [InlineData(ConnectorType.GoogleVertexAI)]
    [InlineData(ConnectorType.Naver)]
    public void Given_Unsupported_ConnectorType_When_CreateChatClient_Invoked_Then_It_Should_Throw(ConnectorType connectorType)
    {
        // Arrange
        var settings = BuildAppSettings(connectorType: connectorType);

        // Act
        Func<Task> func = async () => await LanguageModelConnector.CreateChatClientAsync(settings);

        // Assert
        func.ShouldThrow<NotSupportedException>()
            .Message.ShouldContain($"Connector type '{connectorType}'");
    }
}