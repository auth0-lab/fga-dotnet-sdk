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

using Auth0.Fga.Api;
using Auth0.Fga.Client;
using Auth0.Fga.Exceptions;
using Auth0.Fga.Exceptions.Parsers;
using Auth0.Fga.Model;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Auth0.Fga.Test.Api {
    /// <summary>
    ///  Class for testing Auth0FgaApi
    /// </summary>
    public class Auth0FgaApiTests : IDisposable {
        private readonly string _storeId;
        private readonly Configuration.Configuration _config;
        private static readonly string PlaygroundEnvironment = "playground";
        private static readonly string StagingEnvironment = "staging";

        public Auth0FgaApiTests() {
            _storeId = "6c181474-aaa1-4df7-8929-6e7b3a992754-test";
            _config = new Configuration.Configuration(_storeId, PlaygroundEnvironment);
        }

        private HttpResponseMessage GetCheckResponse(CheckResponse content, bool shouldRetry = false) {
            var response = new HttpResponseMessage() {
                StatusCode = shouldRetry ? HttpStatusCode.TooManyRequests : HttpStatusCode.OK,
                Content = Utils.CreateJsonStringContent(content),
                Headers = { }
            };

            if (shouldRetry) {
                response.Headers.Add(RateLimitParser.RateLimitHeader.LimitRemaining, "0");
                response.Headers.Add(RateLimitParser.RateLimitHeader.LimitResetIn, "100");
                response.Headers.Add(RateLimitParser.RateLimitHeader.LimitTotalInPeriod, "2");
            }

            return response;
        }

        public void Dispose() {
            // Cleanup when everything is done.
        }

        /// <summary>
        /// Test that a storeId is required in the configuration
        /// </summary>
        [Fact]
        public void StoreIdRequired() {
            var storeIdRequiredConfig = new Configuration.Configuration(null, PlaygroundEnvironment);
            void ActionMissingStoreId() => storeIdRequiredConfig.IsValid();
            var exception = Assert.Throws<FgaRequiredParamError>(ActionMissingStoreId);
            Assert.Equal("Required parameter StoreId was not defined when calling Configuration.", exception.Message);
        }

        // /// <summary>
        // /// Test that a valid environment is required in the configuration
        // /// </summary>
        [Fact]
        public void ValidEnvironmentRequired() {
            void ActionInvalidEnvironment() => new Configuration.Configuration(_storeId, "does-not-exist");
            var exception = Assert.Throws<FgaInvalidEnvironmentError>(ActionInvalidEnvironment);
            Assert.Equal("does-not-exist is not a valid environment. Valid environments are: default, us, staging, playground", exception.Message);
        }

        /// <summary>
        /// Test that providing no client id or secret when they are required should error
        /// </summary>
        [Fact]
        public void ClientIdClientSecretRequired() {
            var missingClientIdClientSecretConfigApiInit = new Configuration.Configuration(_storeId, StagingEnvironment);
            void ActionMissingClientIdClientSecretApiInit() =>
                missingClientIdClientSecretConfigApiInit.IsValid();
            var exceptionMissingClientIdClientSecretApiInit =
                Assert.Throws<FgaRequiredParamError>(ActionMissingClientIdClientSecretApiInit);
            Assert.Equal("Required parameter ClientId was not defined when calling Configuration.",
                exceptionMissingClientIdClientSecretApiInit.Message);

            var missingClientIdClientSecretConfig = new Configuration.Configuration(_storeId, StagingEnvironment);
            void ActionMissingClientIdClientSecret() =>
                missingClientIdClientSecretConfig.IsValid();
            var exceptionMissingClientIdClientSecret =
                Assert.Throws<FgaRequiredParamError>(ActionMissingClientIdClientSecret);
            Assert.Equal("Required parameter ClientId was not defined when calling Configuration.",
                exceptionMissingClientIdClientSecret.Message);

            var missingClientIdConfig = new Configuration.Configuration(_storeId, StagingEnvironment) {
                ClientSecret = "some-secret"
            };
            void ActionMissingClientId() =>
                missingClientIdConfig.IsValid();
            var exceptionMissingClientId =
                Assert.Throws<FgaRequiredParamError>(ActionMissingClientId);
            Assert.Equal("Required parameter ClientId was not defined when calling Configuration.",
                exceptionMissingClientId.Message);

            var missingClientSecretConfig = new Configuration.Configuration(_storeId, StagingEnvironment) {
                ClientId = "some-id"
            };
            void ActionMissingClientSecret() =>
                missingClientSecretConfig.IsValid();
            var exceptionMissingClientSecret =
                Assert.Throws<FgaRequiredParamError>(ActionMissingClientSecret);
            Assert.Equal("Required parameter ClientSecret was not defined when calling Configuration.",
                exceptionMissingClientSecret.Message);
        }

        /// <summary>
        /// Test that environments that require auth do not throw error when credentials provided
        /// </summary>
        [Fact]
        public void ClientIdClientSecretRequiredAndProvided() {
            var config = new Configuration.Configuration(_storeId, StagingEnvironment) {
                ClientId = "some-id",
                ClientSecret = "some-secret"
            };
            config.IsValid();
        }

        /// <summary>
        /// Test that environments that do not require auth do not throw error on missing credentials
        /// </summary>
        [Fact]
        public void ClientIdClientSecretNotRequired() {
            var config = new Configuration.Configuration(storeId: _storeId, environment: PlaygroundEnvironment);
            config.IsValid();
        }

        /// <summary>
        /// Test that a network call is issued to get the token at the first request if client id is provided
        /// </summary>
        [Fact]
        public async Task ExchangeCredentialsTest() {
            var config = new Configuration.Configuration(_storeId, StagingEnvironment) {
                ClientId = "some-id",
                ClientSecret = "some-secret"
            };

            var mockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req =>
                        req.RequestUri == new Uri($"https://{config.ApiTokenIssuer}/oauth/token") &&
                        req.Method == HttpMethod.Post),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage() {
                    StatusCode = HttpStatusCode.OK,
                    Content = Utils.CreateJsonStringContent(new OAuth2Client.AccessTokenResponse() {
                        AccessToken = "some-token",
                        ExpiresIn = 20000,
                        TokenType = "Bearer"
                    }),
                });

            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req =>
                        req.RequestUri.ToString()
                            .StartsWith($"{config.BasePath}/stores/{config.StoreId}/authorization-models") &&
                        req.Method == HttpMethod.Get),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage() {
                    StatusCode = HttpStatusCode.OK,
                    Content = Utils.CreateJsonStringContent(
                            new ReadAuthorizationModelsResponse() { AuthorizationModels = { } }),
                });

            var httpClient = new HttpClient(mockHandler.Object);
            var auth0FgaApi = new Auth0FgaApi(config, httpClient);

            var response = await auth0FgaApi.ReadAuthorizationModels(null, null);

            mockHandler.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.RequestUri == new Uri($"https://{config.ApiTokenIssuer}/oauth/token") &&
                    req.Method == HttpMethod.Post),
                ItExpr.IsAny<CancellationToken>()
            );
            mockHandler.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.RequestUri.ToString().StartsWith($"{config.BasePath}/stores/{config.StoreId}/authorization-models") &&
                    req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>()
            );
            mockHandler.Protected().Verify(
                "SendAsync",
                Times.Exactly(0),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.RequestUri == new Uri($"{config.BasePath}/stores/{config.StoreId}/check") &&
                    req.Method == HttpMethod.Post),
                ItExpr.IsAny<CancellationToken>()
            );
        }

        /**
         * Errors
         */
        /// <summary>
        /// Test 400s return FgaApiValidationError
        /// </summary>
        [Fact]
        public async Task FgaApiValidationErrorTest() {
            var mockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req =>
                        req.RequestUri == new Uri($"{_config.BasePath}/stores/{_config.StoreId}/check") &&
                        req.Method == HttpMethod.Post),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent("\"message\":\"FAILED_PRECONDITION\",\"code\":9", Encoding.UTF8, "application/json"),
                });

            var httpClient = new HttpClient(mockHandler.Object);

            var auth0FgaApi = new Auth0FgaApi(_config, httpClient);

            var body = new CheckRequest(new TupleKey("document:roadmap", "viewer", "user:anne"));

            Task<CheckResponse> BadRequestError() => auth0FgaApi.Check(body);
            var error = await Assert.ThrowsAsync<FgaApiValidationError>(BadRequestError);
            Assert.Equal(error.Method, HttpMethod.Post);
            Assert.Equal("Check", error.ApiName);
            Assert.Equal(_config.StoreId, error.StoreId);

            mockHandler.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.RequestUri == new Uri($"{_config.BasePath}/stores/{_config.StoreId}/check") &&
                    req.Method == HttpMethod.Post),
                ItExpr.IsAny<CancellationToken>()
            );
        }

        /// <summary>
        /// Test 500s return FgaApiInternalError
        /// </summary>
        [Fact]
        public async Task FgaApiInternalErrorTest() {
            var mockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req =>
                        req.RequestUri == new Uri($"{_config.BasePath}/stores/{_config.StoreId}/check") &&
                        req.Method == HttpMethod.Post),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage {
                    StatusCode = HttpStatusCode.InternalServerError,
                });

            var httpClient = new HttpClient(mockHandler.Object);

            var auth0FgaApi = new Auth0FgaApi(_config, httpClient);

            var body = new CheckRequest(new TupleKey("document:roadmap", "viewer", "user:81684243-9356-4421-8fbf-a4f8d36aa31b"));

            Task<CheckResponse> InternalApiError() => auth0FgaApi.Check(body);
            var error = await Assert.ThrowsAsync<FgaApiInternalError>(InternalApiError);
            Assert.Equal(error.Method, HttpMethod.Post);
            Assert.Equal("Check", error.ApiName);
            Assert.Equal($"{_config.BasePath}/stores/{_config.StoreId}/check", error.RequestUrl);
            Assert.Equal(_config.StoreId, error.StoreId);

            mockHandler.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.RequestUri == new Uri($"{_config.BasePath}/stores/{_config.StoreId}/check") &&
                    req.Method == HttpMethod.Post),
                ItExpr.IsAny<CancellationToken>()
            );
        }

        /// <summary>
        /// Test 500s return FgaApiError
        /// </summary>
        [Fact]
        public async Task FgaApiErrorTest() {
            var mockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req =>
                        req.RequestUri == new Uri($"{_config.BasePath}/stores/{_config.StoreId}/check") &&
                        req.Method == HttpMethod.Post),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage {
                    StatusCode = HttpStatusCode.NotFound,
                });

            var httpClient = new HttpClient(mockHandler.Object);

            var auth0FgaApi = new Auth0FgaApi(_config, httpClient);

            var body = new CheckRequest(new TupleKey("document:roadmap", "viewer", "user:81684243-9356-4421-8fbf-a4f8d36aa31b"));

            Task<CheckResponse> ApiError() => auth0FgaApi.Check(body);
            var error = await Assert.ThrowsAsync<FgaApiError>(ApiError);
            Assert.Equal(error.Method, HttpMethod.Post);
            Assert.Equal("Check", error.ApiName);
            Assert.Equal($"{_config.BasePath}/stores/{_config.StoreId}/check", error.RequestUrl);
            Assert.Equal(_config.StoreId, error.StoreId);

            mockHandler.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.RequestUri == new Uri($"{_config.BasePath}/stores/{_config.StoreId}/check") &&
                    req.Method == HttpMethod.Post),
                ItExpr.IsAny<CancellationToken>()
            );
        }

        /// <summary>
        /// Test 429s return FgaApiRateLimitExceededError
        /// </summary>
        [Fact]
        public async Task FgaApiRateLimitExceededErrorTest() {
            var mockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req =>
                        req.RequestUri == new Uri($"{_config.BasePath}/stores/{_config.StoreId}/check") &&
                        req.Method == HttpMethod.Post),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(GetCheckResponse(new CheckResponse { Allowed = true }, true));

            var httpClient = new HttpClient(mockHandler.Object);

            var auth0FgaApi = new Auth0FgaApi(_config, httpClient);

            var body = new CheckRequest(new TupleKey("document:roadmap", "viewer", "user:81684243-9356-4421-8fbf-a4f8d36aa31b"));

            Task<CheckResponse> RateLimitExceededError() => auth0FgaApi.Check(body);
            var error = await Assert.ThrowsAsync<FgaApiRateLimitExceededError>(RateLimitExceededError);
            Assert.Equal("Rate Limit Error for POST Check with API limit of 2 requests per Second.", error.Message);
            Assert.Equal(100, error.ResetInMs);
            Assert.Equal(2, error.Limit);
            Assert.Equal("Second", error.LimitUnit.ToString());
            Assert.Equal(error.Method, HttpMethod.Post);
            Assert.Equal("Check", error.ApiName);
            Assert.Equal(_config.StoreId, error.StoreId);

            mockHandler.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.RequestUri == new Uri($"{_config.BasePath}/stores/{_config.StoreId}/check") &&
                    req.Method == HttpMethod.Post),
                ItExpr.IsAny<CancellationToken>()
            );
        }

        /// <summary>
        /// Test Retry on Rate Limit Exceeded Error
        /// </summary>
        [Fact]
        public async Task RetryOnRateLimitTest() {
            var mockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            mockHandler.Protected()
                .SetupSequence<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req =>
                        req.RequestUri == new Uri($"{_config.BasePath}/stores/{_config.StoreId}/check") &&
                        req.Method == HttpMethod.Post),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(GetCheckResponse(new CheckResponse { Allowed = true }, true))
                .ReturnsAsync(GetCheckResponse(new CheckResponse { Allowed = true }, true))
                .ReturnsAsync(GetCheckResponse(new CheckResponse { Allowed = true }, false));

            var httpClient = new HttpClient(mockHandler.Object);

            var config = new Configuration.Configuration(_storeId, PlaygroundEnvironment) {
                MaxRetry = 3,
            };
            var auth0FgaApi = new Auth0FgaApi(config, httpClient);

            var body = new CheckRequest(new TupleKey("document:roadmap", "viewer", "user:81684243-9356-4421-8fbf-a4f8d36aa31b"));

            await auth0FgaApi.Check(body);

            mockHandler.Protected().Verify(
                "SendAsync",
                Times.Exactly(3),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.RequestUri == new Uri($"{_config.BasePath}/stores/{_config.StoreId}/check") &&
                    req.Method == HttpMethod.Post),
                ItExpr.IsAny<CancellationToken>()
            );
        }

        /**
          * Tests
          */
        /// <summary>
        /// Test ReadAuthorizationModels
        /// </summary>
        [Fact]
        public async Task ReadAuthorizationModelsTest() {
            const string authorizationModelId = "1uHxCSuTP0VKPYSnkq1pbb1jeZw";

            var mockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req =>
                        req.RequestUri.ToString()
                            .StartsWith($"{_config.BasePath}/stores/{_config.StoreId}/authorization-models") &&
                        req.Method == HttpMethod.Get),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage() {
                    StatusCode = HttpStatusCode.OK,
                    Content = Utils.CreateJsonStringContent(new ReadAuthorizationModelsResponse() {
                        AuthorizationModels = new List<AuthorizationModel>() { new() { Id = authorizationModelId, TypeDefinitions = { } } }
                    }),
                });

            var httpClient = new HttpClient(mockHandler.Object);
            var auth0FgaApi = new Auth0FgaApi(_config, httpClient);

            var response = await auth0FgaApi.ReadAuthorizationModels(null, null);

            mockHandler.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.RequestUri.ToString()
                        .StartsWith($"{_config.BasePath}/stores/{_config.StoreId}/authorization-models") &&
                    req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>()
            );

            Assert.IsType<ReadAuthorizationModelsResponse>(response);
            Assert.Equal(authorizationModelId, response.AuthorizationModels[0].Id);
        }

        /// <summary>
        /// Test WriteAuthorizationModel
        /// </summary>
        [Fact]
        public async Task WriteAuthorizationModelTest() {
            const string authorizationModelId = "1uHxCSuTP0VKPYSnkq1pbb1jeZw";
            var relations = new Dictionary<string, Userset>() {
                {"writer", new Userset(_this: new object())}, {
                    "viewer",
                    new Userset(union: new Usersets(new List<Userset>() {
                        new(new object(), new ObjectRelation("", "writer"))
                    }))
                }
            };
            var body = new WriteAuthorizationModelRequest(new List<TypeDefinition>() { new("repo", relations) });
            var mockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);

            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req =>
                        req.RequestUri == new Uri($"{_config.BasePath}/stores/{_config.StoreId}/authorization-models") &&
                        req.Method == HttpMethod.Post),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage() {
                    StatusCode = HttpStatusCode.OK,
                    Content = Utils.CreateJsonStringContent(
                            new WriteAuthorizationModelResponse() { AuthorizationModelId = authorizationModelId }),
                });

            var httpClient = new HttpClient(mockHandler.Object);
            var auth0FgaApi = new Auth0FgaApi(_config, httpClient);

            var response = await auth0FgaApi.WriteAuthorizationModel(body);

            mockHandler.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.RequestUri == new Uri($"{_config.BasePath}/stores/{_config.StoreId}/authorization-models") &&
                    req.Method == HttpMethod.Post),
                ItExpr.IsAny<CancellationToken>()
            );

            Assert.IsType<WriteAuthorizationModelResponse>(response);
        }

        /// <summary>
        /// Test ReadAuthorizationModel
        /// </summary>
        [Fact]
        public async Task ReadAuthorizationModelTest() {
            const string authorizationModelId = "01FMJA27YCE3QAT8RDS9VZFN0T";

            var mockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req =>
                        req.RequestUri ==
                        new Uri($"{_config.BasePath}/stores/{_config.StoreId}/authorization-models/{authorizationModelId}") &&
                        req.Method == HttpMethod.Get),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage() {
                    StatusCode = HttpStatusCode.OK,
                    Content = Utils.CreateJsonStringContent(new ReadAuthorizationModelResponse() {
                        AuthorizationModel = new AuthorizationModel(id: authorizationModelId,
                                typeDefinitions: new List<TypeDefinition>())
                    }),
                });

            var httpClient = new HttpClient(mockHandler.Object);
            var auth0FgaApi = new Auth0FgaApi(_config, httpClient);

            var response = await auth0FgaApi.ReadAuthorizationModel(authorizationModelId);

            mockHandler.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.RequestUri ==
                    new Uri($"{_config.BasePath}/stores/{_config.StoreId}/authorization-models/{authorizationModelId}") &&
                    req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>()
            );

            Assert.IsType<ReadAuthorizationModelResponse>(response);
            Assert.Equal(authorizationModelId, response.AuthorizationModel.Id);
        }

        /// <summary>
        /// Test Check
        /// </summary>
        [Fact]
        public async Task CheckTest() {
            var mockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req =>
                        req.RequestUri == new Uri($"{_config.BasePath}/stores/{_config.StoreId}/check") &&
                        req.Method == HttpMethod.Post),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage() {
                    StatusCode = HttpStatusCode.OK,
                    Content = Utils.CreateJsonStringContent(new CheckResponse { Allowed = true }),
                });

            var httpClient = new HttpClient(mockHandler.Object);
            var auth0FgaApi = new Auth0FgaApi(_config, httpClient);

            var body = new CheckRequest(new TupleKey("document:roadmap", "viewer", "user:81684243-9356-4421-8fbf-a4f8d36aa31b"));
            var response = await auth0FgaApi.Check(body);

            mockHandler.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.RequestUri == new Uri($"{_config.BasePath}/stores/{_config.StoreId}/check") &&
                    req.Method == HttpMethod.Post),
                ItExpr.IsAny<CancellationToken>()
            );

            Assert.IsType<CheckResponse>(response);
            Assert.True(response.Allowed);
        }

        /// <summary>
        /// Test Write (Write Relationship Tuples)
        /// </summary>
        [Fact]
        public async Task WriteWriteTest() {
            var mockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req =>
                        req.RequestUri == new Uri($"{_config.BasePath}/stores/{_config.StoreId}/write") &&
                        req.Method == HttpMethod.Post),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage() {
                    StatusCode = HttpStatusCode.OK,
                    Content = Utils.CreateJsonStringContent(new Object()),
                });

            var httpClient = new HttpClient(mockHandler.Object);
            var auth0FgaApi = new Auth0FgaApi(_config, httpClient);

            var body = new WriteRequest(
                new TupleKeys(new List<TupleKey> { new("document:roadmap", "viewer", "user:81684243-9356-4421-8fbf-a4f8d36aa31b") }));
            var response = await auth0FgaApi.Write(body);

            mockHandler.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.RequestUri == new Uri($"{_config.BasePath}/stores/{_config.StoreId}/write") &&
                    req.Method == HttpMethod.Post),
                ItExpr.IsAny<CancellationToken>()
            );
        }

        /// <summary>
        /// Test Write (Delete Relationship Tuples)
        /// </summary>
        [Fact]
        public async Task WriteDeleteTest() {
            var mockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req =>
                        req.RequestUri == new Uri($"{_config.BasePath}/stores/{_config.StoreId}/write") &&
                        req.Method == HttpMethod.Post),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage() {
                    StatusCode = HttpStatusCode.OK,
                    Content = Utils.CreateJsonStringContent(new Object()),
                });

            var httpClient = new HttpClient(mockHandler.Object);
            var auth0FgaApi = new Auth0FgaApi(_config, httpClient);

            var body = new WriteRequest(new TupleKeys(new List<TupleKey> { }),
                new TupleKeys(new List<TupleKey> { new("document:roadmap", "viewer", "user:81684243-9356-4421-8fbf-a4f8d36aa31b") }));
            var response = await auth0FgaApi.Write(body);

            mockHandler.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.RequestUri == new Uri($"{_config.BasePath}/stores/{_config.StoreId}/write") &&
                    req.Method == HttpMethod.Post),
                ItExpr.IsAny<CancellationToken>()
            );
        }

        /// <summary>
        /// Test Write (Write and Delete Relationship Tuples of a Specific Authorization Model ID)
        /// </summary>
        [Fact]
        public async Task WriteMixedWithAuthorizationModelIdTest() {
            var mockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req =>
                        req.RequestUri == new Uri($"{_config.BasePath}/stores/{_config.StoreId}/write") &&
                        req.Method == HttpMethod.Post),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage() {
                    StatusCode = HttpStatusCode.OK,
                    Content = Utils.CreateJsonStringContent(new Object()),
                });

            var httpClient = new HttpClient(mockHandler.Object);
            var auth0FgaApi = new Auth0FgaApi(_config, httpClient);

            var body = new WriteRequest(
                new TupleKeys(new List<TupleKey> { new("document:roadmap", "writer", "user:81684243-9356-4421-8fbf-a4f8d36aa31b") }),
                new TupleKeys(new List<TupleKey> { new("document:roadmap", "viewer", "user:81684243-9356-4421-8fbf-a4f8d36aa31b") }),
                "1uHxCSuTP0VKPYSnkq1pbb1jeZw");
            var response = await auth0FgaApi.Write(body);

            mockHandler.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.RequestUri == new Uri($"{_config.BasePath}/stores/{_config.StoreId}/write") &&
                    req.Method == HttpMethod.Post),
                ItExpr.IsAny<CancellationToken>()
            );
        }

        /// <summary>
        /// Test Expand
        /// </summary>
        [Fact]
        public async Task ExpandTest() {
            var mockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);

            var jsonResponse =
                "{\"tree\":{\"root\":{\"name\":\"document:roadmap#owner\", \"union\":{\"nodes\":[{\"name\":\"document:roadmap#owner\", \"leaf\":{\"users\":{\"users\":[\"team:product#member\"]}}}, {\"name\":\"document:roadmap#owner\", \"leaf\":{\"tupleToUserset\":{\"tupleset\":\"document:roadmap#owner\", \"computed\":[{\"userset\":\"org:contoso#admin\"}]}}}]}}}}";
            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req =>
                        req.RequestUri == new Uri($"{_config.BasePath}/stores/{_config.StoreId}/expand") &&
                        req.Method == HttpMethod.Post),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage() {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(jsonResponse, Encoding.UTF8, "application/json"),
                });

            var httpClient = new HttpClient(mockHandler.Object);
            var auth0FgaApi = new Auth0FgaApi(_config, httpClient);

            var body = new ExpandRequest(new TupleKey(_object: "document:roadmap", relation: "viewer"));
            var response = await auth0FgaApi.Expand(body);

            mockHandler.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.RequestUri == new Uri($"{_config.BasePath}/stores/{_config.StoreId}/expand") &&
                    req.Method == HttpMethod.Post),
                ItExpr.IsAny<CancellationToken>()
            );

            Assert.IsType<ExpandResponse>(response);
            ExpandResponse expectedResponse = JsonSerializer.Deserialize<ExpandResponse>(jsonResponse);
            Assert.Equal(response, expectedResponse);
        }

        /// <summary>
        /// Test Expand with Complex Response
        /// </summary>
        [Fact]
        public async Task ExpandComplexResponseTest() {
            var mockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            var mockResponse = new ExpandResponse(
                tree: new UsersetTree(
                root: new Node(name: "document:roadmap1#owner",
                union: new Nodes(
                    nodes: new List<Node>() {
                        new Node(name: "document:roadmap2#owner", leaf: new Leaf(users: new Users(users: new List<string>(){"team:product#member"}))),
                        new Node(name: "document:roadmap3#owner", leaf: new Leaf(tupleToUserset: new UsersetTreeTupleToUserset(tupleset: "document:roadmap#owner", computed: new List<Computed>(){
                        new Computed(userset: "org:contoso#admin")
                    }))),
                }),
                difference: new UsersetTreeDifference(
                    _base: new Node(name: "document:roadmap3#owner", leaf: new Leaf(users: new Users(users: new List<string>() { "team:product#member" }))),
                    subtract: new Node(name: "document:roadmap4#owner", leaf: new Leaf(users: new Users(users: new List<string>() { "team:product#member" })))
                ),
                intersection: new Nodes(
                    nodes: new List<Node>() {
                        new Node(name: "document:roadmap5#owner", leaf: new Leaf(users: new Users(users: new List<string>(){"team:product#commentor"}))),
                        new Node(name: "document:roadmap6#owner", leaf: new Leaf(tupleToUserset: new UsersetTreeTupleToUserset(tupleset: "document:roadmap#viewer", computed: new List<Computed>(){
                        new Computed(userset: "org:contoso#owner")
                    }))),
                }))
            ));

            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req =>
                        req.RequestUri == new Uri($"{_config.BasePath}/stores/{_config.StoreId}/expand") &&
                        req.Method == HttpMethod.Post),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage() {
                    StatusCode = HttpStatusCode.OK,
                    Content = Utils.CreateJsonStringContent(mockResponse),
                });

            var httpClient = new HttpClient(mockHandler.Object);
            var auth0FgaApi = new Auth0FgaApi(_config, httpClient);

            var body = new ExpandRequest(new TupleKey(_object: "document:roadmap", relation: "viewer"));
            var response = await auth0FgaApi.Expand(body);

            mockHandler.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.RequestUri == new Uri($"{_config.BasePath}/stores/{_config.StoreId}/expand") &&
                    req.Method == HttpMethod.Post),
                ItExpr.IsAny<CancellationToken>()
            );

            Assert.IsType<ExpandResponse>(response);
            Assert.Equal(response, mockResponse);
        }

        /// <summary>
        /// Test ListObjects
        /// </summary>
        [Fact]
        public async Task ListObjectsTest() {
            var mockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            var expectedResponse = new ListObjectsResponse { ObjectIds = new List<string> { "roadmap" } };
            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req =>
                        req.RequestUri == new Uri($"{_config.BasePath}/stores/{_config.StoreId}/list-objects") &&
                        req.Method == HttpMethod.Post),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage() {
                    StatusCode = HttpStatusCode.OK,
                    Content = Utils.CreateJsonStringContent(expectedResponse),
                });

            var httpClient = new HttpClient(mockHandler.Object);
            var auth0FgaApi = new Auth0FgaApi(_config, httpClient);

            var body = new ListObjectsRequest {
                AuthorizationModelId = "01GAHCE4YVKPQEKZQHT2R89MQV",
                User = "user:81684243-9356-4421-8fbf-a4f8d36aa31b",
                Relation = "can_read",
                Type = "document",
                ContextualTuples = new ContextualTupleKeys() {
                    TupleKeys = new List<TupleKey> {
                        new("folder:product", "editor", "user:81684243-9356-4421-8fbf-a4f8d36aa31b"),
                        new("document:roadmap", "parent", "folder:product")
                    }
                }
            };
            var response = await auth0FgaApi.ListObjects(body);

            mockHandler.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.RequestUri == new Uri($"{_config.BasePath}/stores/{_config.StoreId}/list-objects") &&
                    req.Method == HttpMethod.Post),
                ItExpr.IsAny<CancellationToken>()
            );

            Assert.IsType<ListObjectsResponse>(response);
            Assert.Single(response.ObjectIds);
            Assert.Equal(response, expectedResponse);
        }

        /// <summary>
        /// Test Read
        /// </summary>
        [Fact]
        public async Task ReadTest() {
            var mockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            var expectedResponse = new ReadResponse() {
                Tuples = new List<Model.Tuple>() {
                                new(new TupleKey("document:roadmap", "viewer", "user:81684243-9356-4421-8fbf-a4f8d36aa31b"), DateTime.Now)
                            }
            };
            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req =>
                        req.RequestUri == new Uri($"{_config.BasePath}/stores/{_config.StoreId}/read") &&
                        req.Method == HttpMethod.Post),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage() {
                    StatusCode = HttpStatusCode.OK,
                    Content = Utils.CreateJsonStringContent(expectedResponse),
                });

            var httpClient = new HttpClient(mockHandler.Object);
            var auth0FgaApi = new Auth0FgaApi(_config, httpClient);

            var body = new ReadRequest(new TupleKey("document:roadmap", "viewer", "user:81684243-9356-4421-8fbf-a4f8d36aa31b"));
            var response = await auth0FgaApi.Read(body);

            mockHandler.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.RequestUri == new Uri($"{_config.BasePath}/stores/{_config.StoreId}/read") &&
                    req.Method == HttpMethod.Post),
                ItExpr.IsAny<CancellationToken>()
            );

            Assert.IsType<ReadResponse>(response);
            Assert.Single(response.Tuples);
            Assert.Equal(response, expectedResponse);
        }

        /// <summary>
        /// Test ReadChanges
        /// </summary>
        [Fact]
        public async Task ReadChangesTest() {
            var mockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            var expectedResponse = new ReadChangesResponse() {
                Changes = new List<TupleChange>() {
                            new(new TupleKey("document:roadmap", "viewer", "user:81684243-9356-4421-8fbf-a4f8d36aa31b"), TupleOperation.WRITE, DateTime.Now),
                        },
                ContinuationToken = "eyJwayI6IkxBVEVTVF9OU0NPTkZJR19hdXRoMHN0b3JlIiwic2siOiIxem1qbXF3MWZLZExTcUoyN01MdTdqTjh0cWgifQ=="
            };
            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req =>
                        req.RequestUri.ToString()
                            .StartsWith($"{_config.BasePath}/stores/{_config.StoreId}/changes") &&
                        req.Method == HttpMethod.Get),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage() {
                    StatusCode = HttpStatusCode.OK,
                    Content = Utils.CreateJsonStringContent(expectedResponse),
                });

            var httpClient = new HttpClient(mockHandler.Object);
            var auth0FgaApi = new Auth0FgaApi(_config, httpClient);

            var type = "repo";
            var pageSize = 25;
            var continuationToken = "eyJwayI6IkxBVEVTVF9OU0NPTkZJR19hdXRoMHN0b3JlIiwic2siOiIxem1qbXF3MWZLZExTcUoyN01MdTdqTjh0cWgifQ==";
            var response = await auth0FgaApi.ReadChanges(type, pageSize, continuationToken);

            mockHandler.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.RequestUri.ToString()
                        .StartsWith($"{_config.BasePath}/stores/{_config.StoreId}/changes") &&
                    req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>()
            );

            Assert.IsType<ReadChangesResponse>(response);
            Assert.Single(response.Changes);
            Assert.Equal(response, expectedResponse);
        }

        /// <summary>
        /// Test WriteAssertions
        /// </summary>
        [Fact]
        public async Task WriteAssertionsTest() {
            const string authorizationModelId = "1uHxCSuTP0VKPYSnkq1pbb1jeZw";
            var mockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req =>
                        req.RequestUri ==
                        new Uri($"{_config.BasePath}/stores/{_config.StoreId}/assertions/{authorizationModelId}") &&
                        req.Method == HttpMethod.Put),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage() {
                    StatusCode = HttpStatusCode.NoContent,
                });

            var httpClient = new HttpClient(mockHandler.Object);
            var auth0FgaApi = new Auth0FgaApi(_config, httpClient);

            var body = new WriteAssertionsRequest(assertions: new List<Assertion>() {
                new(new TupleKey("document:roadmap", "viewer", "user:81684243-9356-4421-8fbf-a4f8d36aa31b"), true)
            });
            await auth0FgaApi.WriteAssertions(authorizationModelId, body);

            mockHandler.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.RequestUri ==
                    new Uri($"{_config.BasePath}/stores/{_config.StoreId}/assertions/{authorizationModelId}") &&
                    req.Method == HttpMethod.Put),
                ItExpr.IsAny<CancellationToken>()
            );
        }

        /// <summary>
        /// Test ReadAssertions
        /// </summary>
        [Fact]
        public async Task ReadAssertionsTest() {
            const string authorizationModelId = "01FMJA27YCE3QAT8RDS9VZFN0T";

            var mockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req =>
                        req.RequestUri ==
                        new Uri($"{_config.BasePath}/stores/{_config.StoreId}/assertions/{authorizationModelId}") &&
                        req.Method == HttpMethod.Get),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage() {
                    StatusCode = HttpStatusCode.OK,
                    Content = Utils.CreateJsonStringContent(new ReadAssertionsResponse(authorizationModelId,
                            assertions: new List<Assertion>())),
                });

            var httpClient = new HttpClient(mockHandler.Object);
            var auth0FgaApi = new Auth0FgaApi(_config, httpClient);

            var response = await auth0FgaApi.ReadAssertions(authorizationModelId);

            mockHandler.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.RequestUri ==
                    new Uri($"{_config.BasePath}/stores/{_config.StoreId}/assertions/{authorizationModelId}") &&
                    req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>()
            );

            Assert.IsType<ReadAssertionsResponse>(response);
            Assert.Equal(authorizationModelId, response.AuthorizationModelId);
            Assert.Empty(response.Assertions);
        }
    }
}