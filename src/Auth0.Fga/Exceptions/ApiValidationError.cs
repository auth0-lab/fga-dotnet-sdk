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


using Auth0.Fga.Exceptions.Parsers;

namespace Auth0.Fga.Exceptions;

/// <summary>
/// FGA API Validation Error - Corresponding to HTTP Error Codes 400 and 422
/// </summary>
public class FgaApiValidationError : FgaApiError {
    /// <inheritdoc />
    public FgaApiValidationError(HttpResponseMessage response, HttpRequestMessage request, string? apiName,
        ApiErrorParser? apiError = null) : base(response, request, apiName, apiError) {
    }

    internal new static async Task<FgaApiValidationError> CreateAsync(HttpResponseMessage response, HttpRequestMessage request, string? apiName) {
        return new FgaApiValidationError(response, request, apiName,
            await ApiErrorParser.Parse(response).ConfigureAwait(false));
    }
}