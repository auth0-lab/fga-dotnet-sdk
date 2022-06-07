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


using Auth0.Fga.Configuration;

namespace Auth0.Fga.Exceptions;

/// <summary>
/// An error that this thrown if the environment is incorrect or not provided
/// </summary>
public class FgaInvalidEnvironmentError : FgaValidationError {
    /// <summary>
    /// Initializes a new instance of the <see cref="FgaInvalidEnvironmentError"/> class.
    /// </summary>
    public FgaInvalidEnvironmentError()
        : base($"A valid environment was not provided. Valid environments are: {EnvironmentConfiguration.AvailableEnvironments}") {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FgaInvalidEnvironmentError"/> class with a specified error message.
    /// </summary>
    /// <param name="environment">The environment value that was provided.</param>
    public FgaInvalidEnvironmentError(string environment)
        : base($"{environment} is not a valid environment. Valid environments are: {EnvironmentConfiguration.AvailableEnvironments}") {
    }
}