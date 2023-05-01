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
/// Setup Auth0 Fine Grained Authorization (FGA) Configuration
/// </summary>
public class Configuration : BaseConfiguration {
    #region Constants

    public static readonly string DefaultEnvironment = EnvironmentConfiguration.DefaultEnvironment;

    /// <summary>
    ///     Version of the package.
    /// </summary>
    /// <value>Version of the package.</value>
    public const string Version = "0.5.0";

    #endregion Constants

    #region Methods

    /// <summary>
    ///     Checks if the configuration is valid
    /// </summary>
    /// <exception cref="FgaRequiredParamError"></exception>
    public void IsValid() {
        if (string.IsNullOrWhiteSpace(StoreId)) {
            throw new FgaRequiredParamError("Configuration", nameof(StoreId));
        }

        if (Environment == null) {
            throw new FgaRequiredParamError("Configuration", nameof(Environment));
        }

        if (AllowNoAuth) {
            return;
        }

        if (string.IsNullOrWhiteSpace(ClientId)) {
            throw new FgaRequiredParamError("Configuration", nameof(ClientId));
        }

        if (string.IsNullOrWhiteSpace(ClientSecret)) {
            throw new FgaRequiredParamError("Configuration", nameof(ClientSecret));
        }

        base.IsValid();
    }

    #endregion

    #region Constructors

    /// <summary>
    ///     Initializes a new instance of the <see cref="Configuration" /> class
    /// </summary>
    /// <exception cref="FgaRequiredParamError"></exception>
    public Configuration(string environment) : this() {
        if (!string.IsNullOrEmpty(environment)) {
            Environment = environment;
        }
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Configuration" /> class
    /// </summary>
    /// <exception cref="FgaRequiredParamError"></exception>
    public Configuration(string storeId, string environment) : this() {
        StoreId = storeId;
        if (!string.IsNullOrEmpty(environment)) {
            Environment = environment;
        }
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Configuration" /> class
    /// </summary>
    /// <exception cref="FgaRequiredParamError"></exception>
    public Configuration() {
        if (string.IsNullOrEmpty(Environment)) {
            Environment = DefaultEnvironment;
        }
        UserAgent = "auth0-fga-sdk {sdkId}/{packageVersion}".Replace("{sdkId}", "dotnet").Replace("{packageVersion}", Version);
        DefaultHeaders ??= new Dictionary<string, string>();

        if (!DefaultHeaders.ContainsKey("User-Agent")) {
            DefaultHeaders.Add("User-Agent", UserAgent);
        }
    }

    #endregion Constructors


    #region Properties

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

            ApiTokenIssuer = environmentConfig.ApiTokenIssuer;
            ApiAudience = environmentConfig.ApiAudience;
            AllowNoAuth = environmentConfig.AllowNoAuth;
            ApiScheme = environmentConfig.ApiScheme;
            ApiHost = environmentConfig.ApiHost;
        }
    }

    /// <summary>
    ///     Allow no auth (no client id/secret will be considered a valid state)
    /// </summary>
    /// <value>AllowNoAuth</value>
    public bool AllowNoAuth { get; set; }

    #endregion Properties
}