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
    /// - no_auth_error: no error  - auth_failure: generic authentication error. Details available via message field.  - auth_failed_invalid_subject: authentication failure due to invalid subject.  - auth_failed_invalid_audience: authentication failure due to invalid audience.  - auth_failed_invalid_issuer: authentication failure due to invalid issuer.  - invalid_claims: authentication failure due to invalid claims.  - auth_failed_invalid_bearer_token: authentication failure due to invalid bearer token.  - missing_customer_in_bearer_token: authentication failure with customer id missing in bearer token.  - missing_store_in_bearer_token: authentication failure with store id missing in bearer token.  - store_mismatch_in_bearer_token: authentication failure due to store mismatch from bearer token.  - customer_mismatch_in_bearer_token: authentication failure due to customer id in the request being different from the customer id in the bearer token.  - bearer_token_missing: bearer token missing in request.  - missing_user_in_bearer_token: authentication failure with user id missing in bearer token.  - unauthenticated: unauthenticated.  - insufficient_permissions: insufficient permissions.  - unauthorized_principal: authentication denial due to unauthorized principal.
    /// </summary>
    /// <value>- no_auth_error: no error  - auth_failure: generic authentication error. Details available via message field.  - auth_failed_invalid_subject: authentication failure due to invalid subject.  - auth_failed_invalid_audience: authentication failure due to invalid audience.  - auth_failed_invalid_issuer: authentication failure due to invalid issuer.  - invalid_claims: authentication failure due to invalid claims.  - auth_failed_invalid_bearer_token: authentication failure due to invalid bearer token.  - missing_customer_in_bearer_token: authentication failure with customer id missing in bearer token.  - missing_store_in_bearer_token: authentication failure with store id missing in bearer token.  - store_mismatch_in_bearer_token: authentication failure due to store mismatch from bearer token.  - customer_mismatch_in_bearer_token: authentication failure due to customer id in the request being different from the customer id in the bearer token.  - bearer_token_missing: bearer token missing in request.  - missing_user_in_bearer_token: authentication failure with user id missing in bearer token.  - unauthenticated: unauthenticated.  - insufficient_permissions: insufficient permissions.  - unauthorized_principal: authentication denial due to unauthorized principal.</value>
    [JsonConverter(typeof(JsonStringEnumMemberConverter<AuthErrorCode>))]
    public enum AuthErrorCode {
        /// <summary>
        /// Enum NoAuthError for value: no_auth_error
        /// </summary>
        [EnumMember(Value = "no_auth_error")]
        NoAuthError = 1,

        /// <summary>
        /// Enum AuthFailure for value: auth_failure
        /// </summary>
        [EnumMember(Value = "auth_failure")]
        AuthFailure = 2,

        /// <summary>
        /// Enum AuthFailedInvalidSubject for value: auth_failed_invalid_subject
        /// </summary>
        [EnumMember(Value = "auth_failed_invalid_subject")]
        AuthFailedInvalidSubject = 3,

        /// <summary>
        /// Enum AuthFailedInvalidAudience for value: auth_failed_invalid_audience
        /// </summary>
        [EnumMember(Value = "auth_failed_invalid_audience")]
        AuthFailedInvalidAudience = 4,

        /// <summary>
        /// Enum AuthFailedInvalidIssuer for value: auth_failed_invalid_issuer
        /// </summary>
        [EnumMember(Value = "auth_failed_invalid_issuer")]
        AuthFailedInvalidIssuer = 5,

        /// <summary>
        /// Enum InvalidClaims for value: invalid_claims
        /// </summary>
        [EnumMember(Value = "invalid_claims")]
        InvalidClaims = 6,

        /// <summary>
        /// Enum AuthFailedInvalidBearerToken for value: auth_failed_invalid_bearer_token
        /// </summary>
        [EnumMember(Value = "auth_failed_invalid_bearer_token")]
        AuthFailedInvalidBearerToken = 7,

        /// <summary>
        /// Enum MissingCustomerInBearerToken for value: missing_customer_in_bearer_token
        /// </summary>
        [EnumMember(Value = "missing_customer_in_bearer_token")]
        MissingCustomerInBearerToken = 8,

        /// <summary>
        /// Enum MissingStoreInBearerToken for value: missing_store_in_bearer_token
        /// </summary>
        [EnumMember(Value = "missing_store_in_bearer_token")]
        MissingStoreInBearerToken = 9,

        /// <summary>
        /// Enum StoreMismatchInBearerToken for value: store_mismatch_in_bearer_token
        /// </summary>
        [EnumMember(Value = "store_mismatch_in_bearer_token")]
        StoreMismatchInBearerToken = 10,

        /// <summary>
        /// Enum CustomerMismatchInBearerToken for value: customer_mismatch_in_bearer_token
        /// </summary>
        [EnumMember(Value = "customer_mismatch_in_bearer_token")]
        CustomerMismatchInBearerToken = 11,

        /// <summary>
        /// Enum BearerTokenMissing for value: bearer_token_missing
        /// </summary>
        [EnumMember(Value = "bearer_token_missing")]
        BearerTokenMissing = 12,

        /// <summary>
        /// Enum MissingUserInBearerToken for value: missing_user_in_bearer_token
        /// </summary>
        [EnumMember(Value = "missing_user_in_bearer_token")]
        MissingUserInBearerToken = 13,

        /// <summary>
        /// Enum Unauthenticated for value: unauthenticated
        /// </summary>
        [EnumMember(Value = "unauthenticated")]
        Unauthenticated = 14,

        /// <summary>
        /// Enum InsufficientPermissions for value: insufficient_permissions
        /// </summary>
        [EnumMember(Value = "insufficient_permissions")]
        InsufficientPermissions = 15,

        /// <summary>
        /// Enum UnauthorizedPrincipal for value: unauthorized_principal
        /// </summary>
        [EnumMember(Value = "unauthorized_principal")]
        UnauthorizedPrincipal = 16

    }

}