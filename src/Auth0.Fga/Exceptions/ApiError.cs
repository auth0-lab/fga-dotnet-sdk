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
using System.Net.Http.Headers;
using System.Text.Json.Serialization;

using Auth0.Fga.Exceptions.Parsers;

namespace Auth0.Fga.Exceptions;

public class Auth0FgaApiError : ApiException {
    /// <summary>
    /// The name of the API endpoint.
    /// </summary>
    [JsonPropertyName("api_name")]
    public string? ApiName { get; internal set; }

    /// <summary>
    /// The request method
    /// </summary>
    [JsonPropertyName("method")]
    public HttpMethod? Method { get; internal set; }

    /// <summary>
    /// The request URL.
    /// </summary>
    [JsonPropertyName("request_url")]
    public string? RequestUrl { get; internal set; }

    /// <summary>
    /// The request URL.
    /// </summary>
    [JsonPropertyName("store_id")]
    public string? StoreId { get; internal set; }

    /// <summary>
    /// The request data sent to the API
    /// </summary>
    [JsonPropertyName("request_data")]
    public HttpContent? RequestData { get; internal set; }

    /// <summary>
    /// The response data received from the API
    /// </summary>
    [JsonPropertyName("response_data")]
    public HttpContent ResponseData { get; internal set; }

    /// <summary>
    /// The request headers sent to the API
    /// </summary>
    [JsonPropertyName("request_headers")]
    public HttpRequestHeaders RequestHeaders { get; internal set; }

    /// <summary>
    /// The response headers received from the API
    /// </summary>
    [JsonPropertyName("response_headers")]
    public HttpResponseHeaders ResponseHeaders { get; internal set; }

    /// <summary>
    /// Optional <see cref="ApiError"/> from the failing API call.
    /// </summary>
    [JsonPropertyName("api_error")]
    public ApiErrorParser ApiError { get; }

    /// <summary>
    /// <see cref="HttpStatusCode"/> code from the failing API call.
    /// </summary>
    [JsonPropertyName("status_code")]
    public HttpStatusCode StatusCode { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Auth0FgaApiError"/> class.
    /// </summary>
    public Auth0FgaApiError() {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RateLimitApiException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public Auth0FgaApiError(string message) : base(message) {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Auth0FgaApiError"/> class with a specified error message
    /// and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or a null
    /// reference if no inner exception is specified.</param>
    public Auth0FgaApiError(string message, Exception innerException)
        : base(message, innerException) {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Auth0FgaApiError"/> class with a specified error message
    /// and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="statusCode"><see cref="HttpStatusCode"/>code of the failing API call.</param>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or a null
    /// reference if no inner exception is specified.</param>
    public Auth0FgaApiError(HttpStatusCode statusCode, string message, Exception innerException)
        : base(message, innerException) {
        StatusCode = statusCode;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiException"/> class with a specified
    /// </summary>
    /// <param name="statusCode"><see cref="HttpStatusCode"/>code of the failing API call.</param>
    /// <param name="apiError">Optional <see cref="ApiErrorParser"/> of the failing API call.</param>
    public Auth0FgaApiError(HttpStatusCode statusCode, ApiErrorParser? apiError = null)
        : this(apiError == null ? statusCode.ToString() : apiError.Message) {
        StatusCode = statusCode;
        ApiError = apiError ?? new ApiErrorParser();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiException"/> class with a specified
    /// </summary>
    /// <param name="response"></param>
    /// <param name="request"></param>
    /// <param name="apiName"></param>
    /// <param name="apiError">Optional <see cref="ApiErrorParser"/> of the failing API call.</param>
    public Auth0FgaApiError(HttpResponseMessage response, HttpRequestMessage request, string? apiName, ApiErrorParser? apiError = null)
        : this(apiError == null ? response.StatusCode.ToString() : apiError.Message) {
        StatusCode = response.StatusCode;
        ApiError = apiError ?? new ApiErrorParser();
        StoreId = request.RequestUri?.LocalPath.Split("/")[2];
        ApiName = apiName;
        Method = request.Method;
        RequestUrl = request.RequestUri?.ToString();
        RequestData = request.Content;
        RequestHeaders = request.Headers;
        ResponseData = response.Content;
        ResponseHeaders = response.Headers;
    }

    internal static async Task<Auth0FgaApiError> CreateAsync(HttpResponseMessage response, HttpRequestMessage request, string? apiName) {
        return new Auth0FgaApiError(response, request, apiName, await ApiErrorParser.Parse(response).ConfigureAwait(false));
    }
}
