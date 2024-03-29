# .NET SDK for Auth0 Fine Grained Authorization (FGA)

[![Nuget](https://img.shields.io/nuget/v/Auth0.Fga?label=Auth0.Fga&style=flat-square)](https://www.nuget.org/packages/Auth0.Fga)
[![Release](https://img.shields.io/github/v/release/auth0-lab/fga-dotnet-sdk?sort=semver&color=green)](https://github.com/auth0-lab/fga-dotnet-sdk/releases)
[![License](https://img.shields.io/badge/License-MIT-blue.svg)](./LICENSE)

This is an autogenerated SDK for Auth0 Fine Grained Authorization (FGA). It provides a wrapper around the [Auth0 Fine Grained Authorization API](https://docs.fga.dev/api/service).

**This SDK is considered deprecated**.

## Table of Contents

- [About Auth0 Fine Grained Authorization (FGA)](#about)
- [Resources](#resources)
- [Contributing](#contributing)
- [License](#license)

## About

[Okta Fine Grained Authorization (FGA)](https://fga.dev) is designed to make it easy for application builders to model their permission layer, and to add and integrate fine-grained authorization into their applications. Okta Fine Grained Authorization (FGA)’s design is optimized for reliability and low latency at a high scale.

**DEPRECATION WARNING**: This project is no longer maintained. We recommend using the [OpenFGA .NET SDK](https://github.com/openfga/dotnet-sdk) with the following configuration instead of this SDK:

```csharp
using OpenFga.Sdk.Client;
using OpenFga.Sdk.Client.Model;
using OpenFga.Sdk.Model;

namespace Example {
    public class Example {
        public static async Task Main() {
            try {
                var configuration = new ClientConfiguration() {
                    ApiUrl = "https://api.us1.fga.dev",
                    StoreId = Environment.GetEnvironmentVariable("FGA_STORE_ID"),
                    AuthorizationModelId = Environment.GetEnvironmentVariable("FGA_AUTHORIZATION_MODEL_ID"),
                    Credentials = new Credentials() {
                        Method = CredentialsMethod.ClientCredentials,
                        Config = new CredentialsConfig() {
                            ApiTokenIssuer = "fga.us.auth0.com",
                            ApiAudience = "https://api.us1.fga.dev/",
                            ClientId = Environment.GetEnvironmentVariable("FGA_CLIENT_ID"),
                            ClientSecret = Environment.GetEnvironmentVariable("FGA_CLIENT_SECRET"),
                        }
                    }
                };
                var fgaClient = new OpenFgaClient(configuration);
                var response = await fgaClient.ReadAuthorizationModels();
            } catch (ApiException e) {
                 Debug.Print("Error: "+ e);
            }
        }
    }
}
```

For US1 (Production US) environment, use the following values:
- API URL: `https://api.us1.fga.dev`
- Credential Method: ClientCredentials
- API Token Issuer: `fga.us.auth0.com`
- API Audience: `https://api.us1.fga.dev/`

You can get the rest of the necessary variables from the FGA Dashboard. See [here](https://docs.fga.dev/intro/dashboard#create-api-credentials).

## Resources

- [Okta Fine Grained Authorization (FGA) Documentation](https://docs.fga.dev)
- [Okta Fine Grained Authorization (FGA) API Documentation](https://docs.fga.dev/api/service)
- [Zanzibar Academy](https://zanzibar.academy)
- [Google's Zanzibar Paper (2019)](https://research.google/pubs/pub48190/)


## Contributing

This repo is deprecated and no longer accepting contributions.

## Author

[Okta FGA](https://github.com/auth0-lab)

## License

This project is licensed under the MIT license. See the [LICENSE](https://github.com/auth0-lab/fga-go-sdk/blob/main/LICENSE) file for more info.

The code in this repo was auto generated by [OpenAPI Generator](https://github.com/OpenAPITools/openapi-generator) from a template based on the [csharp-netcore template](https://github.com/OpenAPITools/openapi-generator/tree/master/modules/openapi-generator/src/main/resources/csharp-netcore), licensed under the [Apache License 2.0](https://github.com/OpenAPITools/openapi-generator/blob/master/LICENSE).

