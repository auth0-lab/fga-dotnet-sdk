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
    /// - no_error: no error  - validation_error: generic validation error.  - authorization_model_not_found: authorization model not found.  - authorization_model_resolution_too_complex: too many rewrite rules to be resolved.  - invalid_write_input: invalid write input.  - cannot_allow_duplicate_tuples_in_one_request: duplicate tuples in one request.  - cannot_allow_duplicate_types_in_one_request: duplicate types in one request.  - cannot_allow_multiple_references_to_one_relation: cannot use a relation to define itself.  - invalid_continuation_token: invalid continuation token.  - invalid_tuple_set: invalid tuple set.  - invalid_check_input: invalid check input.  - invalid_expand_input: invalid expand input.  - unsupported_user_set: unsupported user set.  - invalid_object_format: invalid object format.  - token_issuer_already_registered: token issuers already registered.  - tos_agreement_already_signed: agreement already signed.  - write_failed_due_to_invalid_input: write request failed due to invalid input.  - authorization_model_assertions_not_found: no assertions found for authorization model.  - latest_authorization_model_not_found: latest authorization model not found.  - type_not_found: type not found.  - relation_not_found: relation not found.  - empty_relation_definition: empty relation definition.  - invalid_user: invalid user.  - invalid_token_issuer: invalid token issuer.  - invalid_tuple: invalid tuple.  - unknown_relation: unknown relation.  - store_id_invalid_length: store id has invalid length.  - issuer_url_invalid_uri: issuer url has invalid URI.  - issuer_url_required_absolute_path: issuer url is not absolute path.  - assertions_too_many_items: assertions have too many items.  - id_too_long: ID is too long.  - invalid_environment: invalid environment is specified.  - authorization_model_id_too_long: authorization model id is too long.  - tuple_key_value_not_specified: tuple key value is not specified.  - tuple_keys_too_many_or_too_few_items: tuple keys have too few or too many items.  - page_size_invalid: page size is outside of acceptable range.  - param_missing_value: params value is missing.  - difference_base_missing_value: difference&#39;s base value is missing.  - subtract_base_missing_value: subtract base value is missing.  - object_too_long: object length is too long.  - relation_too_long: relation length is too long.  - type_definitions_too_few_items: type definitions do not have enough item.  - type_invalid_length: type length is invalid.  - type_invalid_pattern: type does not match expected pattern.  - relations_too_few_items: relations have too few items.  - relations_too_long: relations&#39; length is too long.  - relations_invalid_pattern: relations do not match expected pattern.  - object_invalid_pattern: object does not match expected pattern.  - query_string_type_continuation_token_mismatch: type in the query string and the continuation token don&#39;t match.  - exceeded_entity_limit: Reached maximum number of &lt;entity&gt;.  - invalid_contextual_tuple: Invalid contextual tuple.  - duplicate_contextual_tuple: Duplicate contextual tuple.
    /// </summary>
    /// <value>- no_error: no error  - validation_error: generic validation error.  - authorization_model_not_found: authorization model not found.  - authorization_model_resolution_too_complex: too many rewrite rules to be resolved.  - invalid_write_input: invalid write input.  - cannot_allow_duplicate_tuples_in_one_request: duplicate tuples in one request.  - cannot_allow_duplicate_types_in_one_request: duplicate types in one request.  - cannot_allow_multiple_references_to_one_relation: cannot use a relation to define itself.  - invalid_continuation_token: invalid continuation token.  - invalid_tuple_set: invalid tuple set.  - invalid_check_input: invalid check input.  - invalid_expand_input: invalid expand input.  - unsupported_user_set: unsupported user set.  - invalid_object_format: invalid object format.  - token_issuer_already_registered: token issuers already registered.  - tos_agreement_already_signed: agreement already signed.  - write_failed_due_to_invalid_input: write request failed due to invalid input.  - authorization_model_assertions_not_found: no assertions found for authorization model.  - latest_authorization_model_not_found: latest authorization model not found.  - type_not_found: type not found.  - relation_not_found: relation not found.  - empty_relation_definition: empty relation definition.  - invalid_user: invalid user.  - invalid_token_issuer: invalid token issuer.  - invalid_tuple: invalid tuple.  - unknown_relation: unknown relation.  - store_id_invalid_length: store id has invalid length.  - issuer_url_invalid_uri: issuer url has invalid URI.  - issuer_url_required_absolute_path: issuer url is not absolute path.  - assertions_too_many_items: assertions have too many items.  - id_too_long: ID is too long.  - invalid_environment: invalid environment is specified.  - authorization_model_id_too_long: authorization model id is too long.  - tuple_key_value_not_specified: tuple key value is not specified.  - tuple_keys_too_many_or_too_few_items: tuple keys have too few or too many items.  - page_size_invalid: page size is outside of acceptable range.  - param_missing_value: params value is missing.  - difference_base_missing_value: difference&#39;s base value is missing.  - subtract_base_missing_value: subtract base value is missing.  - object_too_long: object length is too long.  - relation_too_long: relation length is too long.  - type_definitions_too_few_items: type definitions do not have enough item.  - type_invalid_length: type length is invalid.  - type_invalid_pattern: type does not match expected pattern.  - relations_too_few_items: relations have too few items.  - relations_too_long: relations&#39; length is too long.  - relations_invalid_pattern: relations do not match expected pattern.  - object_invalid_pattern: object does not match expected pattern.  - query_string_type_continuation_token_mismatch: type in the query string and the continuation token don&#39;t match.  - exceeded_entity_limit: Reached maximum number of &lt;entity&gt;.  - invalid_contextual_tuple: Invalid contextual tuple.  - duplicate_contextual_tuple: Duplicate contextual tuple.</value>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ErrorCode {
        /// <summary>
        /// Enum NoError for value: no_error
        /// </summary>
        [EnumMember(Value = "no_error")]
        NoError = 1,

        /// <summary>
        /// Enum ValidationError for value: validation_error
        /// </summary>
        [EnumMember(Value = "validation_error")]
        ValidationError = 2,

        /// <summary>
        /// Enum AuthorizationModelNotFound for value: authorization_model_not_found
        /// </summary>
        [EnumMember(Value = "authorization_model_not_found")]
        AuthorizationModelNotFound = 3,

        /// <summary>
        /// Enum AuthorizationModelResolutionTooComplex for value: authorization_model_resolution_too_complex
        /// </summary>
        [EnumMember(Value = "authorization_model_resolution_too_complex")]
        AuthorizationModelResolutionTooComplex = 4,

        /// <summary>
        /// Enum InvalidWriteInput for value: invalid_write_input
        /// </summary>
        [EnumMember(Value = "invalid_write_input")]
        InvalidWriteInput = 5,

        /// <summary>
        /// Enum CannotAllowDuplicateTuplesInOneRequest for value: cannot_allow_duplicate_tuples_in_one_request
        /// </summary>
        [EnumMember(Value = "cannot_allow_duplicate_tuples_in_one_request")]
        CannotAllowDuplicateTuplesInOneRequest = 6,

        /// <summary>
        /// Enum CannotAllowDuplicateTypesInOneRequest for value: cannot_allow_duplicate_types_in_one_request
        /// </summary>
        [EnumMember(Value = "cannot_allow_duplicate_types_in_one_request")]
        CannotAllowDuplicateTypesInOneRequest = 7,

        /// <summary>
        /// Enum CannotAllowMultipleReferencesToOneRelation for value: cannot_allow_multiple_references_to_one_relation
        /// </summary>
        [EnumMember(Value = "cannot_allow_multiple_references_to_one_relation")]
        CannotAllowMultipleReferencesToOneRelation = 8,

        /// <summary>
        /// Enum InvalidContinuationToken for value: invalid_continuation_token
        /// </summary>
        [EnumMember(Value = "invalid_continuation_token")]
        InvalidContinuationToken = 9,

        /// <summary>
        /// Enum InvalidTupleSet for value: invalid_tuple_set
        /// </summary>
        [EnumMember(Value = "invalid_tuple_set")]
        InvalidTupleSet = 10,

        /// <summary>
        /// Enum InvalidCheckInput for value: invalid_check_input
        /// </summary>
        [EnumMember(Value = "invalid_check_input")]
        InvalidCheckInput = 11,

        /// <summary>
        /// Enum InvalidExpandInput for value: invalid_expand_input
        /// </summary>
        [EnumMember(Value = "invalid_expand_input")]
        InvalidExpandInput = 12,

        /// <summary>
        /// Enum UnsupportedUserSet for value: unsupported_user_set
        /// </summary>
        [EnumMember(Value = "unsupported_user_set")]
        UnsupportedUserSet = 13,

        /// <summary>
        /// Enum InvalidObjectFormat for value: invalid_object_format
        /// </summary>
        [EnumMember(Value = "invalid_object_format")]
        InvalidObjectFormat = 14,

        /// <summary>
        /// Enum TokenIssuerAlreadyRegistered for value: token_issuer_already_registered
        /// </summary>
        [EnumMember(Value = "token_issuer_already_registered")]
        TokenIssuerAlreadyRegistered = 15,

        /// <summary>
        /// Enum TosAgreementAlreadySigned for value: tos_agreement_already_signed
        /// </summary>
        [EnumMember(Value = "tos_agreement_already_signed")]
        TosAgreementAlreadySigned = 16,

        /// <summary>
        /// Enum WriteFailedDueToInvalidInput for value: write_failed_due_to_invalid_input
        /// </summary>
        [EnumMember(Value = "write_failed_due_to_invalid_input")]
        WriteFailedDueToInvalidInput = 17,

        /// <summary>
        /// Enum AuthorizationModelAssertionsNotFound for value: authorization_model_assertions_not_found
        /// </summary>
        [EnumMember(Value = "authorization_model_assertions_not_found")]
        AuthorizationModelAssertionsNotFound = 18,

        /// <summary>
        /// Enum LatestAuthorizationModelNotFound for value: latest_authorization_model_not_found
        /// </summary>
        [EnumMember(Value = "latest_authorization_model_not_found")]
        LatestAuthorizationModelNotFound = 19,

        /// <summary>
        /// Enum TypeNotFound for value: type_not_found
        /// </summary>
        [EnumMember(Value = "type_not_found")]
        TypeNotFound = 20,

        /// <summary>
        /// Enum RelationNotFound for value: relation_not_found
        /// </summary>
        [EnumMember(Value = "relation_not_found")]
        RelationNotFound = 21,

        /// <summary>
        /// Enum EmptyRelationDefinition for value: empty_relation_definition
        /// </summary>
        [EnumMember(Value = "empty_relation_definition")]
        EmptyRelationDefinition = 22,

        /// <summary>
        /// Enum InvalidUser for value: invalid_user
        /// </summary>
        [EnumMember(Value = "invalid_user")]
        InvalidUser = 23,

        /// <summary>
        /// Enum InvalidTokenIssuer for value: invalid_token_issuer
        /// </summary>
        [EnumMember(Value = "invalid_token_issuer")]
        InvalidTokenIssuer = 24,

        /// <summary>
        /// Enum InvalidTuple for value: invalid_tuple
        /// </summary>
        [EnumMember(Value = "invalid_tuple")]
        InvalidTuple = 25,

        /// <summary>
        /// Enum UnknownRelation for value: unknown_relation
        /// </summary>
        [EnumMember(Value = "unknown_relation")]
        UnknownRelation = 26,

        /// <summary>
        /// Enum StoreIdInvalidLength for value: store_id_invalid_length
        /// </summary>
        [EnumMember(Value = "store_id_invalid_length")]
        StoreIdInvalidLength = 27,

        /// <summary>
        /// Enum IssuerUrlInvalidUri for value: issuer_url_invalid_uri
        /// </summary>
        [EnumMember(Value = "issuer_url_invalid_uri")]
        IssuerUrlInvalidUri = 28,

        /// <summary>
        /// Enum IssuerUrlRequiredAbsolutePath for value: issuer_url_required_absolute_path
        /// </summary>
        [EnumMember(Value = "issuer_url_required_absolute_path")]
        IssuerUrlRequiredAbsolutePath = 29,

        /// <summary>
        /// Enum AssertionsTooManyItems for value: assertions_too_many_items
        /// </summary>
        [EnumMember(Value = "assertions_too_many_items")]
        AssertionsTooManyItems = 30,

        /// <summary>
        /// Enum IdTooLong for value: id_too_long
        /// </summary>
        [EnumMember(Value = "id_too_long")]
        IdTooLong = 31,

        /// <summary>
        /// Enum InvalidEnvironment for value: invalid_environment
        /// </summary>
        [EnumMember(Value = "invalid_environment")]
        InvalidEnvironment = 32,

        /// <summary>
        /// Enum AuthorizationModelIdTooLong for value: authorization_model_id_too_long
        /// </summary>
        [EnumMember(Value = "authorization_model_id_too_long")]
        AuthorizationModelIdTooLong = 33,

        /// <summary>
        /// Enum TupleKeyValueNotSpecified for value: tuple_key_value_not_specified
        /// </summary>
        [EnumMember(Value = "tuple_key_value_not_specified")]
        TupleKeyValueNotSpecified = 34,

        /// <summary>
        /// Enum TupleKeysTooManyOrTooFewItems for value: tuple_keys_too_many_or_too_few_items
        /// </summary>
        [EnumMember(Value = "tuple_keys_too_many_or_too_few_items")]
        TupleKeysTooManyOrTooFewItems = 35,

        /// <summary>
        /// Enum PageSizeInvalid for value: page_size_invalid
        /// </summary>
        [EnumMember(Value = "page_size_invalid")]
        PageSizeInvalid = 36,

        /// <summary>
        /// Enum ParamMissingValue for value: param_missing_value
        /// </summary>
        [EnumMember(Value = "param_missing_value")]
        ParamMissingValue = 37,

        /// <summary>
        /// Enum DifferenceBaseMissingValue for value: difference_base_missing_value
        /// </summary>
        [EnumMember(Value = "difference_base_missing_value")]
        DifferenceBaseMissingValue = 38,

        /// <summary>
        /// Enum SubtractBaseMissingValue for value: subtract_base_missing_value
        /// </summary>
        [EnumMember(Value = "subtract_base_missing_value")]
        SubtractBaseMissingValue = 39,

        /// <summary>
        /// Enum ObjectTooLong for value: object_too_long
        /// </summary>
        [EnumMember(Value = "object_too_long")]
        ObjectTooLong = 40,

        /// <summary>
        /// Enum RelationTooLong for value: relation_too_long
        /// </summary>
        [EnumMember(Value = "relation_too_long")]
        RelationTooLong = 41,

        /// <summary>
        /// Enum TypeDefinitionsTooFewItems for value: type_definitions_too_few_items
        /// </summary>
        [EnumMember(Value = "type_definitions_too_few_items")]
        TypeDefinitionsTooFewItems = 42,

        /// <summary>
        /// Enum TypeInvalidLength for value: type_invalid_length
        /// </summary>
        [EnumMember(Value = "type_invalid_length")]
        TypeInvalidLength = 43,

        /// <summary>
        /// Enum TypeInvalidPattern for value: type_invalid_pattern
        /// </summary>
        [EnumMember(Value = "type_invalid_pattern")]
        TypeInvalidPattern = 44,

        /// <summary>
        /// Enum RelationsTooFewItems for value: relations_too_few_items
        /// </summary>
        [EnumMember(Value = "relations_too_few_items")]
        RelationsTooFewItems = 45,

        /// <summary>
        /// Enum RelationsTooLong for value: relations_too_long
        /// </summary>
        [EnumMember(Value = "relations_too_long")]
        RelationsTooLong = 46,

        /// <summary>
        /// Enum RelationsInvalidPattern for value: relations_invalid_pattern
        /// </summary>
        [EnumMember(Value = "relations_invalid_pattern")]
        RelationsInvalidPattern = 47,

        /// <summary>
        /// Enum ObjectInvalidPattern for value: object_invalid_pattern
        /// </summary>
        [EnumMember(Value = "object_invalid_pattern")]
        ObjectInvalidPattern = 48,

        /// <summary>
        /// Enum QueryStringTypeContinuationTokenMismatch for value: query_string_type_continuation_token_mismatch
        /// </summary>
        [EnumMember(Value = "query_string_type_continuation_token_mismatch")]
        QueryStringTypeContinuationTokenMismatch = 49,

        /// <summary>
        /// Enum ExceededEntityLimit for value: exceeded_entity_limit
        /// </summary>
        [EnumMember(Value = "exceeded_entity_limit")]
        ExceededEntityLimit = 50,

        /// <summary>
        /// Enum InvalidContextualTuple for value: invalid_contextual_tuple
        /// </summary>
        [EnumMember(Value = "invalid_contextual_tuple")]
        InvalidContextualTuple = 51,

        /// <summary>
        /// Enum DuplicateContextualTuple for value: duplicate_contextual_tuple
        /// </summary>
        [EnumMember(Value = "duplicate_contextual_tuple")]
        DuplicateContextualTuple = 52

    }

}