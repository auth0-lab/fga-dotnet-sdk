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

internal static class EnvironmentConfiguration {
    public static readonly string DefaultEnvironment = "default";

    public class Auth0FgaEnvironmentConfiguration {
        public Auth0FgaEnvironmentConfiguration(string apiScheme, string apiHost, string apiTokenIssuer, string apiAudience,
            bool allowNoAuth = false) {
            ApiScheme = apiScheme;
            ApiHost = apiHost;
            ApiTokenIssuer = apiTokenIssuer;
            ApiAudience = apiAudience;
            AllowNoAuth = allowNoAuth;
        }

        public string ApiScheme { get; }
        public string ApiHost { get; }
        public string ApiTokenIssuer { get; }
        public string ApiAudience { get; }
        public bool AllowNoAuth { get; }
    }

    private static readonly Dictionary<string, Auth0FgaEnvironmentConfiguration> EnvironmentConfigurations =
        new() {
            {
                "default",
                new Auth0FgaEnvironmentConfiguration("https", "api.us1.fga.dev",
                    "fga.us.auth0.com",
                    "https://api.us1.fga.dev/")
            },
            {
                "us",
                new Auth0FgaEnvironmentConfiguration("https", "api.us1.fga.dev",
                    "fga.us.auth0.com",
                    "https://api.us1.fga.dev/")
            },
            {
                "staging",
                new Auth0FgaEnvironmentConfiguration("https", "api.staging.fga.dev",
                    "sandcastle-dev.us.auth0.com",
                    "https://api.staging.fga.dev/")
            },
            {
                "playground",
                new Auth0FgaEnvironmentConfiguration("https", "api.playground.fga.dev",
                    "sandcastle-dev.us.auth0.com",
                    "https://api.playground.fga.dev/", true)
            }
        };

    public static readonly string AvailableEnvironments = string.Join(", ", new List<string>(EnvironmentConfigurations.Keys));

    public static Auth0FgaEnvironmentConfiguration Get(string environment) {
        try {
            return EnvironmentConfigurations[environment];
        }
        catch (KeyNotFoundException) {
            throw new FgaInvalidEnvironmentError(environment);
        }
    }
}