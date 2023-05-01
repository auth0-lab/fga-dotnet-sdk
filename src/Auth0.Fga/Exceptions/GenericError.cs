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
/// Base FGA Exception
/// </summary>
public class FgaError : Exception {
    /// <summary>
    /// Initializes a new instance of the <see cref="FgaError"/> class.
    /// </summary>
    public FgaError() : base() {
    }

    /// <inheritdoc />
    public FgaError(string message) : base(message) {
    }

    /// <inheritdoc />
    public FgaError(string message, Exception innerException) : base(message, innerException) {
    }
}