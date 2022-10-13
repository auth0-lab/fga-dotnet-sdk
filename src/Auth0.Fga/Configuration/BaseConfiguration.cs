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


using Auth0.Fga.Exceptions;

namespace Auth0.Fga.Configuration;

/// <summary>
/// Setup Auth0 Fine Grained Authorization (FGA) BaseConfiguration
/// </summary>
public class BaseConfiguration {
    #region Methods

    private static bool IsWellFormedUriString(string uri) {
        return Uri.TryCreate(uri, UriKind.Absolute, out var uriResult) &&
               ((uriResult.ToString().Equals(uri) || uriResult.ToString().Equals($"{uri}/")) &&
                (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps));
    }

    /// <summary>
    ///     Checks if the configuration is valid
    /// </summary>
    /// <exception cref="FgaRequiredParamError"></exception>
    public void IsValid() {
        if (string.IsNullOrWhiteSpace(StoreId)) {
            throw new FgaRequiredParamError("BaseConfiguration", nameof(StoreId));
        }

        if (string.IsNullOrWhiteSpace(ApiScheme)) {
            throw new FgaRequiredParamError("BaseConfiguration", nameof(ApiScheme));
        }

        if (string.IsNullOrWhiteSpace(ApiHost)) {
            throw new FgaRequiredParamError("BaseConfiguration", nameof(ApiHost));
        }

        if (!IsWellFormedUriString(BasePath)) {
            throw new FgaValidationError(
                $"BaseConfiguration.ApiScheme ({ApiScheme}) and BaseConfiguration.ApiHost ({ApiHost}) do not form a valid URI ({BasePath})");
        }

        if (!string.IsNullOrWhiteSpace(ClientId) || !string.IsNullOrWhiteSpace(ClientSecret)) {
            if (string.IsNullOrWhiteSpace(ClientId)) {
                throw new FgaRequiredParamError("BaseConfiguration", nameof(ClientId));
            }

            if (string.IsNullOrWhiteSpace(ClientSecret)) {
                throw new FgaRequiredParamError("BaseConfiguration", nameof(ClientSecret));
            }

            if (string.IsNullOrWhiteSpace(ApiTokenIssuer)) {
                throw new FgaRequiredParamError("BaseConfiguration", nameof(ApiTokenIssuer));
            }

            if (string.IsNullOrWhiteSpace(ApiAudience)) {
                throw new FgaRequiredParamError("BaseConfiguration", nameof(ApiAudience));
            }
        }

        if (!string.IsNullOrWhiteSpace(ApiTokenIssuer) && !IsWellFormedUriString($"https://{ApiTokenIssuer}")) {
            throw new FgaValidationError(
                $"BaseConfiguration.ApiTokenIssuer does not form a valid URI (https://{ApiTokenIssuer})");
        }

        if (MaxRetry > 5) {
            throw new FgaValidationError("BaseConfiguration.MaxRetry exceeds maximum allowed limit of 5");
        }
    }

    #endregion

    #region Constants

    /// <summary>
    ///     Version of the package.
    /// </summary>
    /// <value>Version of the package.</value>
    public const string Version = "0.4.0";

    #endregion Constants

    #region Constructors

    /// <summary>
    ///     Initializes a new instance of the <see cref="BaseConfiguration" /> class
    /// </summary>
    /// <exception cref="FgaRequiredParamError"></exception>
    public BaseConfiguration(string storeId) : this() {
        StoreId = storeId;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="BaseConfiguration" /> class
    /// </summary>
    /// <exception cref="FgaRequiredParamError"></exception>
    public BaseConfiguration() {
        UserAgent = "auth0-fga-sdk {sdkId}/{packageVersion}".Replace("{sdkId}", "dotnet").Replace("{packageVersion}", Version);
        DefaultHeaders ??= new Dictionary<string, string>();

        if (!DefaultHeaders.ContainsKey("User-Agent")) {
            DefaultHeaders.Add("User-Agent", UserAgent);
        }
    }

    #endregion Constructors


    #region Properties

    /// <summary>
    ///     Gets or sets the default headers.
    /// </summary>
    public IDictionary<string, string> DefaultHeaders { get; set; }

    /// <summary>
    ///     Gets or sets the HTTP user agent.
    /// </summary>
    /// <value>Http user agent.</value>
    public string UserAgent { get; set; }

    /// <summary>
    ///     Gets the Base Path.
    /// </summary>
    /// <value>Base Path.</value>
    public string BasePath => $"{ApiScheme}://{ApiHost}";

    /// <summary>
    ///     Gets or sets the API Scheme.
    /// </summary>
    /// <value>ApiScheme.</value>
    public string ApiScheme { get; set; } = "https";

    /// <summary>
    ///     Gets or sets the API Host.
    /// </summary>
    /// <value>ApiHost.</value>
    public string ApiHost { get; set; }

    /// <summary>
    ///     Gets or sets the Store ID.
    /// </summary>
    /// <value>Store ID.</value>
    public string StoreId { get; set; }

    /// <summary>
    ///     Gets or sets the Client ID.
    /// </summary>
    /// <value>Client ID.</value>
    public string? ClientId { get; set; }

    /// <summary>
    ///     Gets or sets the Client Secret.
    /// </summary>
    /// <value>Client Secret.</value>
    public string? ClientSecret { get; set; }

    /// <summary>
    ///     Gets or sets the API Token Issuer.
    /// </summary>
    /// <value>API Token Issuer.</value>
    public string ApiTokenIssuer { get; set; } = null!;

    /// <summary>
    ///     Gets or sets the API Audience.
    /// </summary>
    /// <value>API Audience.</value>
    public string ApiAudience { get; set; } = null!;

    /// <summary>
    ///     Max number of times to retry after a request is rate limited
    /// </summary>
    /// <value>MaxRetry</value>
    public int MaxRetry { get; set; } = 0;

    /// <summary>
    ///     Minimum time in ms to wait before retrying
    /// </summary>
    /// <value>MinWaitInMs</value>
    public int MinWaitInMs { get; set; } = 100;

    #endregion Properties
}