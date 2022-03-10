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


using Auth0.Fga.Exceptions.Parsers;

namespace Auth0.Fga.Exceptions;

public class Auth0FgaApiRateLimitExceededError : Auth0FgaApiError {
    /// <summary>
    /// The maximum number of requests the consumer is allowed to make.
    /// </summary>
    public long Limit { get; internal set; }

    /// <summary>
    /// The number of requests remaining in the current rate limit window.
    /// </summary>
    public long Remaining { get; internal set; }

    /// <summary>
    /// The period unit of the current rate limit window.
    /// </summary>
    public RateLimitParser.PeriodUnit LimitUnit { get; internal set; }

    /// <summary>
    /// Number of milliseconds after which rate limit is reset
    /// </summary>
    public long? ResetInMs { get; internal set; }

    /// <summary>
    /// The date and time offset at which the current rate limit window is reset.
    /// </summary>
    public DateTimeOffset? Reset { get; internal set; }

    /// <summary>
    /// Error Message
    /// </summary>
    public new string? Message { get; internal set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Auth0FgaApiRateLimitExceededError"/> class with a specified
    /// </summary>
    /// <param name="response"></param>
    /// <param name="request"></param>
    /// <param name="apiName"></param>
    /// <param name="apiError"></param>
    public Auth0FgaApiRateLimitExceededError(HttpResponseMessage response, HttpRequestMessage request, string? apiName,
        ApiErrorParser? apiError = null) : base(response, request, apiName, apiError) {
        var rateLimit = RateLimitParser.Parse(response.Headers,
            request.Method, apiName);
        Limit = rateLimit.Limit;
        Remaining = rateLimit.Remaining;
        LimitUnit = rateLimit.LimitUnit;
        ResetInMs = rateLimit.ResetInMs;
        Reset = rateLimit.Reset;
        Message =
            $"Auth0 FGA Rate Limit Error for {rateLimit.Method} {rateLimit.ApiName} with API limit of {rateLimit.Limit} requests per {rateLimit.LimitUnit}.";
    }

    internal new static async Task<Auth0FgaApiError> CreateAsync(HttpResponseMessage response, HttpRequestMessage request, string? apiName) {
        return new Auth0FgaApiRateLimitExceededError(response, request, apiName,
            await ApiErrorParser.Parse(response).ConfigureAwait(false));
    }
}
