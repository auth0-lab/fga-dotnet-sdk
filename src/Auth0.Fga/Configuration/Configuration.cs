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


using Auth0.Fga.Exceptions;

namespace Auth0.Fga.Configuration;

/// <summary>
/// Setup Auth0 FGA Configuration
/// </summary>
public class Configuration {
    public static readonly string DefaultEnvironment = EnvironmentConfiguration.DefaultEnvironment;

    #region Methods

    /// <summary>
    ///     Checks if the configuration is valid
    /// </summary>
    /// <exception cref="Auth0FgaRequiredParamError"></exception>
    public void IsValid() {
        if (string.IsNullOrWhiteSpace(StoreId)) {
            throw new Auth0FgaRequiredParamError("Configuration", nameof(StoreId));
        }

        if (Environment == null) {
            throw new Auth0FgaRequiredParamError("Configuration", nameof(Environment));
        }

        if (AllowNoAuth) {
            return;
        }

        if (string.IsNullOrWhiteSpace(ClientId)) {
            throw new Auth0FgaRequiredParamError("Configuration", nameof(ClientId));
        }

        if (string.IsNullOrWhiteSpace(ClientSecret)) {
            throw new Auth0FgaRequiredParamError("Configuration", nameof(ClientSecret));
        }
    }

    #endregion

    #region Constants

    /// <summary>
    ///     Version of the package.
    /// </summary>
    /// <value>Version of the package.</value>
    public const string Version = "0.2.0";

    #endregion Constants

    #region Constructors

    /// <summary>
    ///     Initializes a new instance of the <see cref="Configuration" /> class
    /// </summary>
    /// <exception cref="Auth0FgaRequiredParamError"></exception>
    public Configuration(string storeId, string environment) : this() {
        StoreId = storeId;
        if (!string.IsNullOrEmpty(environment)) {
            Environment = environment;
        }
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Configuration" /> class
    /// </summary>
    /// <exception cref="Auth0FgaRequiredParamError"></exception>
    public Configuration() {
        if (string.IsNullOrEmpty(Environment)) {
            Environment = DefaultEnvironment;
        }
        UserAgent = "auth0-fga-sdk (dotnet) 0.2.0";
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
    public string BasePath { get; set; } = null!;

    /// <summary>
    ///     Gets or sets the Store ID.
    /// </summary>
    /// <value>Store ID.</value>
    public string StoreId { get; set; }

    private string _environment;

    /// <summary>
    ///     Gets or sets the Auth0 FGA Environment.
    /// </summary>
    /// <value>Auth0 FGA Environment.</value>
    private string Environment {
        get => _environment;
        set {
            _environment = value;
            var environmentConfig = EnvironmentConfiguration.Get(_environment);

            ApiIssuer = environmentConfig.ApiIssuer;
            ApiAudience = environmentConfig.ApiAudience;
            AllowNoAuth = environmentConfig.AllowNoAuth;
            BasePath = $"{environmentConfig.Scheme}://{environmentConfig.Host}";
        }
    }

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
    ///     Gets or sets the API Issuer.
    /// </summary>
    /// <value>API Issuer.</value>
    public string ApiIssuer { get; set; } = null!;

    /// <summary>
    ///     Gets or sets the API Audience.
    /// </summary>
    /// <value>API Audience.</value>
    public string ApiAudience { get; set; } = null!;

    /// <summary>
    ///     Allow no auth (no client id/secret will be considered a valid state)
    /// </summary>
    /// <value>AllowNoAuth</value>
    public bool AllowNoAuth { get; set; }

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
