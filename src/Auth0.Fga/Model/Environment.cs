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
using System.Text.Json.Serialization;

namespace Auth0.Fga.Model {
    /// <summary>
    /// Defines Environment
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Environment {
        /// <summary>
        /// Enum ENVIRONMENTUNSPECIFIED for value: ENVIRONMENT_UNSPECIFIED
        /// </summary>
        [EnumMember(Value = "ENVIRONMENT_UNSPECIFIED")]
        ENVIRONMENTUNSPECIFIED = 1,

        /// <summary>
        /// Enum DEVELOPMENT for value: DEVELOPMENT
        /// </summary>
        [EnumMember(Value = "DEVELOPMENT")]
        DEVELOPMENT = 2,

        /// <summary>
        /// Enum STAGING for value: STAGING
        /// </summary>
        [EnumMember(Value = "STAGING")]
        STAGING = 3,

        /// <summary>
        /// Enum PRODUCTION for value: PRODUCTION
        /// </summary>
        [EnumMember(Value = "PRODUCTION")]
        PRODUCTION = 4

    }

}
