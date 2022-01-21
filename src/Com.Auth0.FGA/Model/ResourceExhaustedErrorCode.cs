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

namespace Com.Auth0.FGA.Model {
    /// <summary>
    /// - no_resource_exhausted_error: no error  - rate_limit_exceeded: operation failed due to exceeding rate limit.  - auth_rate_limit_exceeded: rate limit error during authentication.
    /// </summary>
    /// <value>- no_resource_exhausted_error: no error  - rate_limit_exceeded: operation failed due to exceeding rate limit.  - auth_rate_limit_exceeded: rate limit error during authentication.</value>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ResourceExhaustedErrorCode {
        /// <summary>
        /// Enum NoResourceExhaustedError for value: no_resource_exhausted_error
        /// </summary>
        [EnumMember(Value = "no_resource_exhausted_error")]
        NoResourceExhaustedError = 1,

        /// <summary>
        /// Enum RateLimitExceeded for value: rate_limit_exceeded
        /// </summary>
        [EnumMember(Value = "rate_limit_exceeded")]
        RateLimitExceeded = 2,

        /// <summary>
        /// Enum AuthRateLimitExceeded for value: auth_rate_limit_exceeded
        /// </summary>
        [EnumMember(Value = "auth_rate_limit_exceeded")]
        AuthRateLimitExceeded = 3

    }

}
