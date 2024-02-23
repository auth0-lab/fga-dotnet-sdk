# Changelog

## v0.6.0

### [0.6.0](https://github.com/auth0-lab/fga-dotnet-sdk/compare/v0.5.1...v0.6.0) (2024-02-23)

[Breaking]

As of this point this SDK is deprecated and should no longer be used. Please use [OpenFga.Sdk](https://www.nuget.org/packages/OpenFga.Sdk) instead.

We strongly recommend you use the [OpenFGA .NET SDK](https://github.com/openfga/dotnet-sdk) directly instead with the following configuration:

For US1 (Production US) environment, use the following values:
- API URL: `https://api.us1.fga.dev`
- Credential Method: ClientCredentials
- API Token Issuer: `fga.us.auth0.com`
- API Audience: `https://api.us1.fga.dev/`

You can get the rest of the necessary variables from the FGA Dashboard. See [here](https://docs.fga.dev/intro/dashboard#create-api-credentials).


## v0.5.1

### [0.5.1](https://github.com/auth0-lab/fga-dotnet-sdk/compare/v0.5.0...v0.5.1) (2023-05-01)
- fix: client credentials token expiry period was being evaluated as ms instead of seconds, leading to token refreshes on every call
- chore(deps): upgrade dependencies

## v0.5.0

### [0.5.0](https://github.com/auth0-lab/fga-dotnet-sdk/compare/v0.4.0...v0.5.0) (2022-12-16)

Changes:
- [BREAKING] feat(list-objects)!: response has been changed to include the object type
    e.g. response that was `{"object_ids":["roadmap"]}`, will now be `{"objects":["document:roadmap"]}`

Fixes:
- [BREAKING] fix(models)!: update interfaces that had incorrectly optional fields to make them required

Chore:
- chore(deps): update dependencies

## v0.4.0

### [0.4.0](https://github.com/auth0-lab/fga-dotnet-sdk/compare/v0.3.1...v0.4.0) (2022-10-13)
- BREAKING: exported type `TypeDefinitions` is now `WriteAuthorizationModelRequest`
    Note: This is only a breaking change on the SDK, not the API.
- Support ListObjects
    Support for [ListObjects API](https://docs.fga.dev/api/service#/Relationship%20Queries/ListObjects)

    You call the API and receive the list of object ids from a particular type that the user has a certain relation with.

    For example, to find the list of documents that Anne can read:

    ```csharp
    var body = new ListObjectsRequest{
        AuthorizationModelId = "01GAHCE4YVKPQEKZQHT2R89MQV",
        User = "user:anne",
        Relation = "can_read",
        Type = "document"
    };
    var response = await auth0FgaApi.ListObjects(body);

    // response.ObjectIds = ["roadmap"]
    ```
- fix: issue in deserializing nullable DateTime
- fix: issue in deserializing enums (e.g. deserializing `ReadChangesResponse`)
- chore(deps): upgrade dependencies

## v0.3.1

### [0.3.1](https://github.com/auth0-lab/fga-dotnet-sdk/compare/v0.3.0...v0.3.1) (2022-06-10)

#### Changes
- fix: resolve issue deserializing API responses

#### Known Issues
- An issue still remains in deserializing `ReadChangesResponse` specifically because of an issue in deserializing the `TupleOperation` enum

## v0.3.0

### [0.3.0](https://github.com/auth0-lab/fga-dotnet-sdk/compare/v0.2.4...v0.3.0) (2022-06-07)

#### Changes
- feat!: [ReadAuthorizationModels](https://docs.fga.dev/api/service#/Store%20Models/ReadAuthorizationModels) now returns an array of Authorization Models instead of IDs [BREAKING CHANGE]

    The response will become similar to:
    ```json
    {
        "authorization_models": [
            {
              "id": (string),
              "type_definitions": [...]
            }
        ]
    }
    ```
- feat!: drop support for all settings endpoints [BREAKING CHANGE]
- feat!: Simplify error prefix to `Fga` [BREAKING CHANGE]

    Possible Exceptions:
    - `FgaError`: All errors thrown by the SDK extend this error
    - `FgaApiError`: All errors returned by the API extend this error
    - `FgaApiValidationError`: 400 and 422 Validation Errors returned by the API
    - `FgaApiRateLimitExceededError`: 429 errors returned by the API
    - `FgaApiInternalError`: 5xx errors returned by the API
    - `FgaApiAuthenticationError`: Error during authentication
    - `FgaValidationError`: Error thrown by the SDK when validating input
    - `FgaRequiredParamError`: Error thrown by the SDK when a required parameter is not provided
    - `FgaInvalidEnvironmentError`: Error thrown by the SDK when the provided environment is invalid
- feat!: drop `Params` postfix from the name of the request interface [BREAKING CHANGE]

  e.g. `ReadRequestParams` will become `ReadRequest`
- feat: add support for [contextual tuples](https://docs.fga.dev/intro/auth0-fga-concepts#what-are-contextual-tuples) in the [Check](https://docs.fga.dev/api/service#/Tuples/Check) request

    You can call them like so:
    ```dotnet
    var response = await fgaClient.Check(
        new CheckRequest(
            new TupleKey {User = "anne", Relation = "can_view", Object = "transaction:A"},
                new ContextualTupleKeys(new List<TupleKey>( {
                    new(user: "anne", relation: "user", _object: "ip-address-range:10.0.0.0/16"),
                    new(user: "anne", relation: "user", _object: "timeslot:18_19")
                }
            )
        ));
    ```
- fix: url encode path parameters (StoreId/AuthorizationModelId)
- chore(deps): upgrade dependencies
- chore: internal refactor

## v0.2.4

### [0.2.4](https://github.com/auth0-lab/fga-dotnet-sdk/compare/v0.2.3...v0.2.4) (2022-04-10)

#### Changes
- fix: resolve error parsing expand response due to issue in Users & Nodes models

## v0.2.3

### [0.2.3](https://github.com/auth0-lab/fga-dotnet-sdk/compare/v0.2.2...v0.2.3) (2022-04-09)

#### Changes
- fix: fix api endpoint configuration for us1

## v0.2.2

### [0.2.2](https://github.com/auth0-lab/fga-dotnet-sdk/compare/v0.2.1...v0.2.2) (2022-03-17)

#### Changes
- chore: fix user agent header format
- feat: add support for the Watch API

## v0.2.1

### [0.2.1](https://github.com/auth0-lab/fga-dotnet-sdk/compare/v0.2.0...v0.2.1) (2022-03-11)

#### Changes
chore(ci): add logo and enable auto-publish to nuget

## v0.2.0

### [0.2.0](https://github.com/auth0-lab/fga-dotnet-sdk/compare/v0.1.1...v0.2.0) (2022-03-10)

#### Changes
Project name is now `Auth0.Fga`

## v0.1.1

### [0.1.1](https://github.com/auth0-lab/fga-dotnet-sdk/compare/v0.1.0...v0.1.1) (2022-03-09)

#### Changes
- fix: fix for return types on 204 no content

## v0.1.1

### 0.1.0 (2022-03-07)

#### Changes
Initial Release
