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


namespace Com.Auth0.FGA.Exceptions;

/// <summary>
/// A generic validation error
/// </summary>
public class Auth0FgaValidationError : Exception {
    /// <summary>
    /// Initializes a new instance of the <see cref="Auth0FgaValidationError"/> class.
    /// </summary>
    public Auth0FgaValidationError() : base() {
    }

    /// <inheritdoc />
    public Auth0FgaValidationError(string message) : base(message) {
    }

    /// <inheritdoc />
    public Auth0FgaValidationError(string message, Exception innerException) : base(message, innerException) {
    }
}
