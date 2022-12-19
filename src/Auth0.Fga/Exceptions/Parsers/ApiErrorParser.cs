//
// Auth0 Fine Grained Authorization (FGA)/.NET SDK for Auth0 Fine Grained Authorization (FGA)
//
// API version: 0.1
// Website: https://fga.dev
// Documentation: https://docs.fga.dev
// Support: https://discord.gg/8naAwJfWN6
// License: [MIT](https://github.com/auth0-lab/fga-dotnet-sdk/blob/main/LICENSE)
//
// NOTE: This file was auto generated. DO NOT EDIT.
//


using System.Text.Json;
using System.Text.Json.Serialization;

namespace Auth0.Fga.Exceptions.Parsers;

public class ApiErrorParser {
    /// <summary>
    /// Description of the failing HTTP Status Code.
    /// </summary>
    [JsonPropertyName("error")]
    public string? Error { get; set; }

    /// <summary>
    /// Error code returned by the API.
    /// </summary>
    [JsonPropertyName("code")]
    public string? ErrorCode { get; set; }

    /// <summary>
    /// Description of the error.
    /// </summary>
    [JsonPropertyName("message")]
    public string? Message { get; set; }

    /// <summary>
    /// Parse a <see cref="HttpResponseMessage"/> into an <see cref="ApiErrorParser"/> asynchronously.
    /// </summary>
    /// <param name="response"><see cref="HttpResponseMessage"/> to parse.</param>
    /// <returns><see cref="Task"/> representing the operation and associated <see cref="ApiErrorParser"/> on
    /// successful completion.</returns>
    public static async Task<ApiErrorParser?> Parse(HttpResponseMessage response) {
        if (response == null || response.Content == null)
            return null;

        var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        if (string.IsNullOrEmpty(content))
            return null;

        return Parse(content);
    }

    internal static ApiErrorParser Parse(string content) {
        try {
            return JsonSerializer.Deserialize<ApiErrorParser>(content) ?? throw new InvalidOperationException();
        }
        catch (JsonException) {
            return new ApiErrorParser { Error = content, Message = content };
        }
    }
}