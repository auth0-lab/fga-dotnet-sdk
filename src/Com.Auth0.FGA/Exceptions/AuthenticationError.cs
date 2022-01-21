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


using System.Net;

using Com.Auth0.FGA.Exceptions.Parsers;

namespace Com.Auth0.FGA.Exceptions;

public class Auth0FgaAuthenticationError : Auth0FgaApiError {
    /// <summary>
    /// Initializes a new instance of the <see cref="Auth0FgaAuthenticationError"/> class with a specified error message
    /// and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="apiName">The name of the API called</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or a null
    /// reference if no inner exception is specified.</param>
    public Auth0FgaAuthenticationError(string message, string apiName, Exception innerException)
        : base(HttpStatusCode.Unauthorized, message, innerException) {
        ApiName = apiName;
        Method = HttpMethod.Post;
    }

    public Auth0FgaAuthenticationError(HttpResponseMessage response, HttpRequestMessage request, string? apiName,
        ApiErrorParser? apiError = null) : base(response, request, apiName, apiError) {
    }

    internal new static async Task<Auth0FgaAuthenticationError> CreateAsync(HttpResponseMessage response, HttpRequestMessage request, string? apiName) {
        return new Auth0FgaAuthenticationError(response, request, apiName,
            await ApiErrorParser.Parse(response).ConfigureAwait(false));
    }
}
