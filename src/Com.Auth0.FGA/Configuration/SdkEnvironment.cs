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


using System.Runtime.Serialization;

namespace Com.Auth0.FGA.Configuration;

/// <summary>
///     Enum used to define the SDK environment
/// </summary>
public enum SdkEnvironment {
    /// <summary>
    ///     The default environment
    /// </summary>
    [EnumMember(Value = "default")] Default,

    /// <summary>
    ///     US Environment
    /// </summary>
    [EnumMember(Value = "us")] Us,

    /// <summary>
    ///     Staging Environment
    /// </summary>
    [EnumMember(Value = "staging")] Staging,

    /// <summary>
    ///     Playground environment
    /// </summary>
    [EnumMember(Value = "playground")] Playground
}
