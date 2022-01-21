# .NET SDK for Auth0 Fine Grained Authorization (FGA)


This is an autogenerated SDK for Auth0 Fine Grained Authorization (FGA). It provides a wrapper around the [Auth0 Fine Grained Authorization API](https://docs.fga.dev/api/service).

Warning: This SDK comes with no SLAs and is not production-ready!

## Table of Contents

- [About Auth0 Fine Grained Authorization](#about-auth0-fine-grained-authorization)
- [Resources](#resources)
- [Installation](#installation)
- [Getting Started](#getting-started)
  - [Initializing the API Client](#initializing-the-api-client)
  - [Getting your Store ID, Client ID and Client Secret](#getting-your-store-id-client-id-and-client-secret)
  - [Calling the API](#calling-the-api)
    - [Write Authorization Model](#write-authorization-model)
    - [Read a Single Authorization Model](#read-a-single-authorization-model)
    - [Read Authorization Model IDs](#read-authorization-model-ids)
    - [Check](#check)
    - [Write Tuples](#write-tuples)
    - [Delete Tuples](#delete-tuples)
    - [Expand](#expand)
    - [Read](#read)
  - [API Endpoints](#api-endpoints)
  - [Models](#models)
- [Contributing](#contributing)
  - [Issues](#issues)
  - [Pull Requests](#pull-requests) [Note: We are not accepting Pull Requests at this time!]
- [License](#license)

## About Auth0 Fine Grained Authorization

[Auth0 Fine Grained Authorization (FGA)](https://dashboard.fga.dev) is the  early-stage product we are building at Auth0 as part of [Auth0Lab](https://twitter.com/Auth0Lab/) to solve fine-grained authorization at scale.
If you are interested in learning more about our plans, please reach out via our <a target="_blank" href="https://discord.gg/8naAwJfWN6" rel="noreferrer">Discord chat</a>.

Please note:
* At this point in time, Auth0 Fine Grained Authorization does not come with any SLAs; availability and uptime are not guaranteed.
* While this project is in its early stages, the SDK methods are in flux and might change without a major bump

## Resources

- [The Auth0 FGA Playground](https://play.fga.dev)
- [The Auth0 FGA Documentation](https://docs.fga.dev)
- [Zanzibar Academy](https://zanzibar.academy)
- [Auth0Lab on Twitter](https://twitter.com/Auth0Lab/)
- [Discord Community](https://discord.gg/pvbNmqC)

## Installation

The Auth0 FGA .NET SDK is available on [NuGet](https://www.nuget.org/).

You can install it using:

* The [dotnet CLI](https://docs.microsoft.com/en-us/nuget/consume-packages/install-use-packages-dotnet-cli)
```powershell
dotnet add package Com.Auth0.FGA
```

* The [Package Manager Console](https://docs.microsoft.com/en-us/nuget/consume-packages/install-use-packages-powershell) inside Visual Studio:

```powershell
Install-Package Com.Auth0.FGA
```

* [Visual Studio](https://docs.microsoft.com/en-us/nuget/consume-packages/install-use-packages-visual-studio), [Visual Studio for Mac](https://docs.microsoft.com/en-us/visualstudio/mac/nuget-walkthrough) and [IntelliJ Rider](https://www.jetbrains.com/help/rider/Using_NuGet.html)

Search for and install `Com.Auth0.FGA` in each of their respective package manager UIs.


## Getting Started

### Initializing the API Client

```csharp
using Com.Auth0.FGA.Api;
using Com.Auth0.FGA.Client;
using Com.Auth0.FGA.Configuration;
using Com.Auth0.FGA.Model;

namespace Example {
    public class Example {
        public static void Main() {
            try {
                var storeId = Environment.GetEnvironmentVariable("AUTH0_FGA_STORE_ID");
                var environment = Environment.GetEnvironmentVariable("AUTH0_FGA_ENVIRONMENT")
                var configuration = new Configuration(storeId, environment) {
                    ClientId = Environment.GetEnvironmentVariable("AUTH0_FGA_CLIENT_ID"),
                    ClientSecret = Environment.GetEnvironmentVariable("AUTH0_FGA_CLIENT_SECRET"),
                };
                var auth0FgaApi = new (configuration);
                var response = auth0FgaApi.ReadAuthorizationModels();
                Debug.WriteLine(response.AuthorizationModelIds);
            } catch (ApiException e) {
                 Debug.Print("Status Code: "+ e.ErrorCode);
            }
        }
    }
}
```


### Getting your Store ID, Client ID and Client Secret

#### Production

Make sure you have created your credentials on the Auth0 FGA Dashboard. [Learn how ➡](https://docs.fga.dev/intro/dashboard#create-api-credentials)

You will need to set the `AUTH0_FGA_ENVIRONMENT` variable to `"us"`. Provide the store id, client id and client secret you have created on the Dashboard.

#### Playground

If you are testing this on the public playground, you need to set your `AUTH0_FGA_ENVIRONMENT` to `"playground"`.

To get your store id, you may copy it from the store you have created on the [Playground](https://play.fga.dev). [Learn how ➡](https://docs.fga.dev/intro/playground#getting-store-id)

In the playground environment, you do not need to provide a client id and client secret.

### Calling the API

#### Write Authorization Model

> Note: To learn how to build your authorization model, check the Docs at https://docs.fga.dev/

> Note: The Auth0 FGA Playground, Dashboard and Documentation use a friendly syntax which gets translated to the API syntax seen below. Learn more about [the Auth0 FGA configuration language](https://docs.fga.dev/modeling/configuration-language).

```csharp
var relations = new Dictionary<string, Userset>()
{
    {"writer", new Userset(_this: new object())},
    {
        "reader",
        new Userset(union: new Usersets(new List<Userset>()
            {new(new object(), new ObjectRelation("", "writer"))}))
    }
};
var body = new TypeDefinitions(new List<TypeDefinition>() {new("repo", relations)});
var response = await auth0FgaApi.WriteAuthorizationModel(body);

// response.AuthorizationModelId = 1uHxCSuTP0VKPYSnkq1pbb1jeZw
```

#### Read a Single Authorization Model

```csharp
string authorizationModelId = "1uHxCSuTP0VKPYSnkq1pbb1jeZw"; // Assuming `1uHxCSuTP0VKPYSnkq1pbb1jeZw` is an id of an existing model
var response = await auth0FgaApi.ReadAuthorizationModel(authorizationModelId);

// response.AuthorizationModel.Id = "1uHxCSuTP0VKPYSnkq1pbb1jeZw"
// response.AuthorizationModel.TypeDefinitions = [{ "type": "repo", "relations": { ... } }]
```

#### Read Authorization Model IDs

```csharp
var response = await auth0FgaApi.ReadAuthorizationModels();

// response.AuthorizationModelIds = ["1uHxCSuTP0VKPYSnkq1pbb1jeZw", "GtQpMohWezFmIbyXxVEocOCxxgq"];
```

#### Check

```csharp
var body =
    new CheckRequestParams(tupleKey: new TupleKey("repo:auth0/express-jwt", "reader", "anne"));
var response = await auth0FgaApi.Check(body);
// response.Allowed = true
```

#### Write Tuples

```csharp
var body = new WriteRequestParams(new TupleKeys(new List<TupleKey>
    {new("repo:auth0/express-jwt", "reader", "anne")}));
var response = await auth0FgaApi.Write(body);
```

#### Delete Tuples

```csharp
var body = new WriteRequestParams(new TupleKeys(new List<TupleKey> { }),
    new TupleKeys(new List<TupleKey> {new("repo:auth0/express-jwt", "reader", "anne")}));
var response = await auth0FgaApi.Write(body);
```

#### Expand

```csharp
var body = new ExpandRequestParams(new TupleKey("repo:auth0/express-jwt", "reader"));
var response = await auth0FgaApi.Expand(body);

// response.Tree.Root = {"name":"workspace:675bcac4-ad38-4fb1-a19a-94a5648c91d6#admin","leaf":{"users":{"users":["anne","beth"]}}}
```

#### Read

```csharp
// Find if a relationship tuple stating that a certain user is an admin on a certain workspace
var body = new ReadRequestParams(new TupleKey(
    _object: "workspace:675bcac4-ad38-4fb1-a19a-94a5648c91d6",
    relation: "admin",
    user: "81684243-9356-4421-8fbf-a4f8d36aa31b"));

// Find all relationship tuples where a certain user has a relationship as any relation to a certain workspace
var body = new ReadRequestParams(new TupleKey(
    _object: "workspace:675bcac4-ad38-4fb1-a19a-94a5648c91d6",
    user: "81684243-9356-4421-8fbf-a4f8d36aa31b"));

// Find all relationship tuples where a certain user is an admin on any workspace
var body = new ReadRequestParams(new TupleKey(
    _object: "workspace:",
    relation: "admin",
    user: "81684243-9356-4421-8fbf-a4f8d36aa31b"));

// Find all relationship tuples where any user has a relationship as any relation with a particular workspace
var body = new ReadRequestParams(new TupleKey(
    _object: "workspace:675bcac4-ad38-4fb1-a19a-94a5648c91d6"));

var body = new ReadRequestParams(new TupleKey("repo:auth0/express-jwt", "reader", "anne"));
var response = await auth0FgaApi.Read(body);

// In all the above situations, the response will be of the form:
// response = {"tuples":[{"key":{"user":"...","relation":"...","object":"..."},"timestamp":"..."}]}
```


### API Endpoints

| Method | HTTP request | Description |
| ------------- | ------------- | ------------- |
| [**Check**](docs/Auth0FgaApi.md#check) | **POST** /stores/{store_id}/check | Check whether a user is authorized to access an object |
| [**DeleteTokenIssuer**](docs/Auth0FgaApi.md#deletetokenissuer) | **DELETE** /stores/{store_id}/settings/token-issuers/{id} | Remove 3rd party token issuer for Auth0 FGA read and write operations |
| [**Expand**](docs/Auth0FgaApi.md#expand) | **POST** /stores/{store_id}/expand | Expand all relationships in userset tree format, and following userset rewrite rules.  Useful to reason about and debug a certain relationship |
| [**Read**](docs/Auth0FgaApi.md#read) | **POST** /stores/{store_id}/read | Get tuples from the store that matches a query, without following userset rewrite rules |
| [**ReadAssertions**](docs/Auth0FgaApi.md#readassertions) | **GET** /stores/{store_id}/assertions/{authorization_model_id} | Read assertions for an authorization model ID |
| [**ReadAuthorizationModel**](docs/Auth0FgaApi.md#readauthorizationmodel) | **GET** /stores/{store_id}/authorization-models/{id} | Return a particular version of an authorization model |
| [**ReadAuthorizationModels**](docs/Auth0FgaApi.md#readauthorizationmodels) | **GET** /stores/{store_id}/authorization-models | Return all the authorization model IDs for a particular store |
| [**ReadSettings**](docs/Auth0FgaApi.md#readsettings) | **GET** /stores/{store_id}/settings | Return store settings, including the environment tag |
| [**Write**](docs/Auth0FgaApi.md#write) | **POST** /stores/{store_id}/write | Add or delete tuples from the store |
| [**WriteAssertions**](docs/Auth0FgaApi.md#writeassertions) | **PUT** /stores/{store_id}/assertions/{authorization_model_id} | Upsert assertions for an authorization model ID |
| [**WriteAuthorizationModel**](docs/Auth0FgaApi.md#writeauthorizationmodel) | **POST** /stores/{store_id}/authorization-models | Create a new authorization model |
| [**WriteSettings**](docs/Auth0FgaApi.md#writesettings) | **PATCH** /stores/{store_id}/settings | Update the environment tag for a store |
| [**WriteTokenIssuer**](docs/Auth0FgaApi.md#writetokenissuer) | **POST** /stores/{store_id}/settings/token-issuers | Add 3rd party token issuer for Auth0 FGA read and write operations |



### Models

 - [Model.Any](docs/Any.md)
 - [Model.Assertion](docs/Assertion.md)
 - [Model.AuthErrorCode](docs/AuthErrorCode.md)
 - [Model.AuthenticationErrorMessageResponse](docs/AuthenticationErrorMessageResponse.md)
 - [Model.AuthorizationModel](docs/AuthorizationModel.md)
 - [Model.AuthorizationmodelDifference](docs/AuthorizationmodelDifference.md)
 - [Model.AuthorizationmodelTupleToUserset](docs/AuthorizationmodelTupleToUserset.md)
 - [Model.CheckRequestParams](docs/CheckRequestParams.md)
 - [Model.CheckResponse](docs/CheckResponse.md)
 - [Model.Computed](docs/Computed.md)
 - [Model.Environment](docs/Environment.md)
 - [Model.ErrorCode](docs/ErrorCode.md)
 - [Model.ExpandRequestParams](docs/ExpandRequestParams.md)
 - [Model.ExpandResponse](docs/ExpandResponse.md)
 - [Model.InternalErrorCode](docs/InternalErrorCode.md)
 - [Model.InternalErrorMessageResponse](docs/InternalErrorMessageResponse.md)
 - [Model.Leaf](docs/Leaf.md)
 - [Model.Node](docs/Node.md)
 - [Model.Nodes](docs/Nodes.md)
 - [Model.NotFoundErrorCode](docs/NotFoundErrorCode.md)
 - [Model.ObjectRelation](docs/ObjectRelation.md)
 - [Model.PathUnknownErrorMessageResponse](docs/PathUnknownErrorMessageResponse.md)
 - [Model.ReadAssertionsResponse](docs/ReadAssertionsResponse.md)
 - [Model.ReadAuthorizationModelResponse](docs/ReadAuthorizationModelResponse.md)
 - [Model.ReadAuthorizationModelsResponse](docs/ReadAuthorizationModelsResponse.md)
 - [Model.ReadRequestParams](docs/ReadRequestParams.md)
 - [Model.ReadResponse](docs/ReadResponse.md)
 - [Model.ReadSettingsResponse](docs/ReadSettingsResponse.md)
 - [Model.ResourceExhaustedErrorCode](docs/ResourceExhaustedErrorCode.md)
 - [Model.ResourceExhaustedErrorMessageResponse](docs/ResourceExhaustedErrorMessageResponse.md)
 - [Model.Status](docs/Status.md)
 - [Model.TokenIssuer](docs/TokenIssuer.md)
 - [Model.Tuple](docs/Tuple.md)
 - [Model.TupleKey](docs/TupleKey.md)
 - [Model.TupleKeys](docs/TupleKeys.md)
 - [Model.TypeDefinition](docs/TypeDefinition.md)
 - [Model.TypeDefinitions](docs/TypeDefinitions.md)
 - [Model.Users](docs/Users.md)
 - [Model.Userset](docs/Userset.md)
 - [Model.UsersetTree](docs/UsersetTree.md)
 - [Model.UsersetTreeDifference](docs/UsersetTreeDifference.md)
 - [Model.UsersetTreeTupleToUserset](docs/UsersetTreeTupleToUserset.md)
 - [Model.Usersets](docs/Usersets.md)
 - [Model.ValidationErrorMessageResponse](docs/ValidationErrorMessageResponse.md)
 - [Model.WriteAssertionsRequestParams](docs/WriteAssertionsRequestParams.md)
 - [Model.WriteAuthorizationModelResponse](docs/WriteAuthorizationModelResponse.md)
 - [Model.WriteRequestParams](docs/WriteRequestParams.md)
 - [Model.WriteSettingsRequestParams](docs/WriteSettingsRequestParams.md)
 - [Model.WriteSettingsResponse](docs/WriteSettingsResponse.md)
 - [Model.WriteTokenIssuersRequestParams](docs/WriteTokenIssuersRequestParams.md)
 - [Model.WriteTokenIssuersResponse](docs/WriteTokenIssuersResponse.md)



## Contributing

### Issue Reporting

If you have found a bug or if you have a feature request, please report them at this repository [issues](https://github.com/auth0-lab/fga-dotnet-sdk/issues) section. Please do not report security vulnerabilities on the public GitHub issue tracker. The [Responsible Disclosure Program](https://auth0.com/responsible-disclosure-policy) details the procedure for disclosing security issues.

For auth0 related questions/support please use the [Support Center](https://support.auth0.com).

### Pull Requests

Pull Requests are not currently open, please [raise an issue](https://github.com/auth0-lab/fga-dotnet-sdk/issues) or contact a team member on https://discord.gg/8naAwJfWN6 if there is a feature you'd like us to implement.

## Author

[Auth0Lab](https://github.com/auth0-lab)

## License

This project is licensed under the MIT license. See the [LICENSE](https://github.com/auth0-lab/fga-dotnet-sdk/blob/main/LICENSE) file for more info.

The code in this repo was auto generated by [OpenAPI Generator](https://github.com/OpenAPITools/openapi-generator) from a template based on the [csharp-netcore template](https://github.com/OpenAPITools/openapi-generator/tree/master/modules/openapi-generator/src/main/resources/csharp-netcore), licensed under the [Apache License 2.0](https://github.com/OpenAPITools/openapi-generator/blob/master/LICENSE).

