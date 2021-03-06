# Changelog

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
