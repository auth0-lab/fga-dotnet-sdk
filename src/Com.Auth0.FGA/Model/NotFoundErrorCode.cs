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
    /// - no_not_found_error: no error  - undefined_endpoint: undefined endpoint.  - customer_id_not_found: customer ID is not found.  - store_id_not_found: store ID not found  - store_client_id_not_found: store client ID not found.  - unimplemented: method is unimplemented
    /// </summary>
    /// <value>- no_not_found_error: no error  - undefined_endpoint: undefined endpoint.  - customer_id_not_found: customer ID is not found.  - store_id_not_found: store ID not found  - store_client_id_not_found: store client ID not found.  - unimplemented: method is unimplemented</value>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum NotFoundErrorCode {
        /// <summary>
        /// Enum NoNotFoundError for value: no_not_found_error
        /// </summary>
        [EnumMember(Value = "no_not_found_error")]
        NoNotFoundError = 1,

        /// <summary>
        /// Enum UndefinedEndpoint for value: undefined_endpoint
        /// </summary>
        [EnumMember(Value = "undefined_endpoint")]
        UndefinedEndpoint = 2,

        /// <summary>
        /// Enum CustomerIdNotFound for value: customer_id_not_found
        /// </summary>
        [EnumMember(Value = "customer_id_not_found")]
        CustomerIdNotFound = 3,

        /// <summary>
        /// Enum StoreIdNotFound for value: store_id_not_found
        /// </summary>
        [EnumMember(Value = "store_id_not_found")]
        StoreIdNotFound = 4,

        /// <summary>
        /// Enum StoreClientIdNotFound for value: store_client_id_not_found
        /// </summary>
        [EnumMember(Value = "store_client_id_not_found")]
        StoreClientIdNotFound = 5,

        /// <summary>
        /// Enum Unimplemented for value: unimplemented
        /// </summary>
        [EnumMember(Value = "unimplemented")]
        Unimplemented = 6

    }

}
