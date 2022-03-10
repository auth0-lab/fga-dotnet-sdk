//
// Auth0 Fine Grained Authorization (FGA)/.NET SDK for Auth0 Fine Grained Authorization (FGA)
//
// Auth0 Fine Grained Authorization (FGA) is an early-stage product we are building at Auth0 as part of Auth0Lab to solve fine-grained authorization at scale. If you are interested in learning more about our plans, please reach out via our Discord chat.  The limits and information described in this document is subject to change.
//
// API version: 0.1
// Website: https://fga.dev
// Documentation: https://docs.fga.dev
// Support: https://discord.gg/8naAwJfWN6
// License: [MIT](https://github.com/auth0-lab/fga-dotnet-sdk/blob/main/LICENSE)
//
// NOTE: This file was auto generated. DO NOT EDIT.
//


using System.Runtime.Serialization;

namespace Auth0.Fga.Exceptions;

public class ApiException : Exception {
    /// <summary>
    /// Initializes a new instance of the <see cref="ErrorApiException"/> class.
    /// </summary>
    protected ApiException()
        : base() {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    protected ApiException(string message)
        : base(message) {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiException"/> class with a specified error message
    /// and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or a null
    /// reference if no inner exception is specified.</param>
    protected ApiException(string message, Exception innerException)
        : base(message, innerException) {
    }

    /// <inheritdoc />
    protected ApiException(SerializationInfo serializationInfo, StreamingContext streamingContext)
        : base(serializationInfo, streamingContext) {
    }

    /// <summary>
    /// Create an instance of the specific exception related to a particular response.
    /// </summary>
    /// <param name="response"><see cref="HttpResponseMessage"/></param>
    /// <param name="request"><see cref="HttpRequestMessage"/></param>
    /// <param name="apiName"></param>
    /// <returns>An instance of a <see cref="ApiException"/> subclass containing the appropriate exception for this response.</returns>
    public static async Task<ApiException> CreateSpecificExceptionAsync(HttpResponseMessage response, HttpRequestMessage request,
        string? apiName = null) {
        switch ((int) response.StatusCode) {
            case 401:
                return await Auth0FgaAuthenticationError.CreateAsync(response, request, apiName).ConfigureAwait(false);
            case 400:
            case 442:
                return await Auth0FgaApiValidationError.CreateAsync(response, request, apiName).ConfigureAwait(false);
            case 429:
                return await Auth0FgaApiRateLimitExceededError.CreateAsync(response, request, apiName).ConfigureAwait(false);
            case 500:
                return await Auth0FgaApiInternalError.CreateAsync(response, request, apiName).ConfigureAwait(false);
            default:
                return await Auth0FgaApiError.CreateAsync(response, request, apiName).ConfigureAwait(false);
        }
    }
}
