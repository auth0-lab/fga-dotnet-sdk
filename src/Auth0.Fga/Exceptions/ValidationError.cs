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

namespace Auth0.Fga.Exceptions;

/// <summary>
/// A generic validation error
/// </summary>
public class FgaValidationError : FgaError {
    /// <summary>
    /// Initializes a new instance of the <see cref="FgaValidationError"/> class.
    /// </summary>
    public FgaValidationError() : base() {
    }

    /// <inheritdoc />
    public FgaValidationError(string message) : base(message) {
    }

    /// <inheritdoc />
    public FgaValidationError(string message, Exception innerException) : base(message, innerException) {
    }
}