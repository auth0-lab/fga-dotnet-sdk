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


using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Auth0.Fga.Model {
    /// <summary>
    /// Defines TupleOperation
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumMemberConverter<TupleOperation>))]
    public enum TupleOperation {
        /// <summary>
        /// Enum WRITE for value: TUPLE_OPERATION_WRITE
        /// </summary>
        [EnumMember(Value = "TUPLE_OPERATION_WRITE")]
        WRITE = 1,

        /// <summary>
        /// Enum DELETE for value: TUPLE_OPERATION_DELETE
        /// </summary>
        [EnumMember(Value = "TUPLE_OPERATION_DELETE")]
        DELETE = 2

    }

}