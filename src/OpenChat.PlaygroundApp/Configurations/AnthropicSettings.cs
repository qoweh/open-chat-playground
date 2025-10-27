using OpenChat.PlaygroundApp.Abstractions;

namespace OpenChat.PlaygroundApp.Configurations;

/// <inheritdoc/>
public partial class AppSettings
{
    /// <summary>
    /// Gets or sets the <see cref="AnthropicSettings"/> instance.
    /// </summary>
    public AnthropicSettings? Anthropic { get; set; }
}

/// <summary>
/// This represents the app settings entity for Anthropic Claude.
/// </summary>
public class AnthropicSettings : LanguageModelSettings
{
    /// <summary>
    /// Gets or sets the API key for Anthropic Claude.
    /// </summary>
    public string? ApiKey { get; set; }

    /// <summary>
    /// Gets or sets the model name of Anthropic Claude.
    /// </summary>
    public string? Model { get; set; }

    /// <summary>
    /// Gets or sets the maximum tokens for response.
    /// </summary>
    public int? MaxTokens { get; set; }
}