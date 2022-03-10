using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Com.Auth0.FGA.Api;
using Com.Auth0.FGA.Client;
using Com.Auth0.FGA.Exceptions;
using Com.Auth0.FGA.Exceptions.Parsers;
using Com.Auth0.FGA.Model;

using Moq;
using Moq.Protected;

using Xunit;

using Environment = Com.Auth0.FGA.Model.Environment;

namespace Com.Auth0.FGA.Test.Api {
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
            var exception = Assert.Throws<Auth0FgaRequiredParamError>(ActionMissingStoreId);
            Assert.Equal("Required parameter StoreId was not defined when calling Configuration.", exception.Message);
        }

        // /// <summary>
        // /// Test that a valid environment is required in the configuration
        // /// </summary>
        [Fact]
        public void ValidEnvironmentRequired() {
            void ActionInvalidEnvironment() => new Configuration.Configuration(_storeId, "does-not-exist");
            var exception = Assert.Throws<Auth0FgaInvalidEnvironmentError>(ActionInvalidEnvironment);
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
                Assert.Throws<Auth0FgaRequiredParamError>(ActionMissingClientIdClientSecretApiInit);
            Assert.Equal("Required parameter ClientId was not defined when calling Configuration.",
                exceptionMissingClientIdClientSecretApiInit.Message);

            var missingClientIdClientSecretConfig = new Configuration.Configuration(_storeId, StagingEnvironment);
            void ActionMissingClientIdClientSecret() =>
                missingClientIdClientSecretConfig.IsValid();
            var exceptionMissingClientIdClientSecret =
                Assert.Throws<Auth0FgaRequiredParamError>(ActionMissingClientIdClientSecret);
            Assert.Equal("Required parameter ClientId was not defined when calling Configuration.",
                exceptionMissingClientIdClientSecret.Message);

            var missingClientIdConfig = new Configuration.Configuration(_storeId, StagingEnvironment) {
                ClientSecret = "some-secret"
            };
            void ActionMissingClientId() =>
                missingClientIdConfig.IsValid();
            var exceptionMissingClientId =
                Assert.Throws<Auth0FgaRequiredParamError>(ActionMissingClientId);
            Assert.Equal("Required parameter ClientId was not defined when calling Configuration.",
                exceptionMissingClientId.Message);

            var missingClientSecretConfig = new Configuration.Configuration(_storeId, StagingEnvironment) {
                ClientId = "some-id"
            };
            void ActionMissingClientSecret() =>
                missingClientSecretConfig.IsValid();
            var exceptionMissingClientSecret =
                Assert.Throws<Auth0FgaRequiredParamError>(ActionMissingClientSecret);
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
                        req.RequestUri == new Uri($"https://{config.ApiIssuer}/oauth/token") &&
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
                            new ReadAuthorizationModelsResponse() { AuthorizationModelIds = { } }),
                });

            var httpClient = new HttpClient(mockHandler.Object);
            var auth0FgaApi = new Auth0FgaApi(config, httpClient);

            var response = await auth0FgaApi.ReadAuthorizationModels(null, null);

            mockHandler.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.RequestUri == new Uri($"https://{config.ApiIssuer}/oauth/token") &&
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
        /// Test 400s return Auth0FgaApiValidationError
        /// </summary>
        [Fact]
        public async Task Auth0FgaApiValidationErrorTest() {
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

            var body = new CheckRequestParams(new TupleKey("repo:auth0/express-jwt", "reader", "anne"));

            Task<CheckResponse> BadRequestError() => auth0FgaApi.Check(body);
            var error = await Assert.ThrowsAsync<Auth0FgaApiValidationError>(BadRequestError);
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
        /// Test 500s return Auth0FgaApiInternalError
        /// </summary>
        [Fact]
        public async Task Auth0FgaApiInternalErrorTest() {
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

            var body = new CheckRequestParams(new TupleKey("repo:auth0/express-jwt", "reader", "anne"));

            Task<CheckResponse> InternalApiError() => auth0FgaApi.Check(body);
            var error = await Assert.ThrowsAsync<Auth0FgaApiInternalError>(InternalApiError);
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
        /// Test 500s return Auth0FgaApiError
        /// </summary>
        [Fact]
        public async Task Auth0FgaApiErrorTest() {
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

            var body = new CheckRequestParams(new TupleKey("repo:auth0/express-jwt", "reader", "anne"));

            Task<CheckResponse> ApiError() => auth0FgaApi.Check(body);
            var error = await Assert.ThrowsAsync<Auth0FgaApiError>(ApiError);
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
        /// Test 429s return Auth0FgaApiRateLimitExceededError
        /// </summary>
        [Fact]
        public async Task Auth0FgaApiRateLimitExceededErrorTest() {
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

            var body = new CheckRequestParams(new TupleKey("repo:auth0/express-jwt", "reader", "anne"));

            Task<CheckResponse> RateLimitExceededError() => auth0FgaApi.Check(body);
            var error = await Assert.ThrowsAsync<Auth0FgaApiRateLimitExceededError>(RateLimitExceededError);
            Assert.Equal("Auth0 FGA Rate Limit Error for POST Check with API limit of 2 requests per Second.", error.Message);
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

            var body = new CheckRequestParams(new TupleKey("repo:auth0/express-jwt", "reader", "anne"));

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
                        AuthorizationModelIds = new List<string>() { authorizationModelId }
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
            Assert.Equal(authorizationModelId, response.AuthorizationModelIds[0]);
        }

        /// <summary>
        /// Test WriteAuthorizationModel
        /// </summary>
        [Fact]
        public async Task WriteAuthorizationModelTest() {
            const string authorizationModelId = "1uHxCSuTP0VKPYSnkq1pbb1jeZw";
            var relations = new Dictionary<string, Userset>() {
                {"writer", new Userset(_this: new object())}, {
                    "reader",
                    new Userset(union: new Usersets(new List<Userset>() {
                        new(new object(), new ObjectRelation("", "writer"))
                    }))
                }
            };
            var body = new TypeDefinitions(new List<TypeDefinition>() { new("repo", relations) });
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

            var body = new CheckRequestParams(new TupleKey("repo:auth0/express-jwt", "reader", "anne"));
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

            var body = new WriteRequestParams(
                new TupleKeys(new List<TupleKey> { new("repo:auth0/express-jwt", "reader", "anne") }));
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

            var body = new WriteRequestParams(new TupleKeys(new List<TupleKey> { }),
                new TupleKeys(new List<TupleKey> { new("repo:auth0/express-jwt", "reader", "anne") }));
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

            var body = new WriteRequestParams(
                new TupleKeys(new List<TupleKey> { new("repo:auth0/express-jwt", "writer", "anne") }),
                new TupleKeys(new List<TupleKey> { new("repo:auth0/express-jwt", "reader", "anne") }),
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
                    Content = Utils.CreateJsonStringContent(new ExpandResponse()),
                });

            var httpClient = new HttpClient(mockHandler.Object);
            var auth0FgaApi = new Auth0FgaApi(_config, httpClient);

            var body = new ExpandRequestParams(new TupleKey(_object: "repo:auth0/express-jwt", relation: "reader"));
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
        }

        /// <summary>
        /// Test Read
        /// </summary>
        [Fact]
        public async Task ReadTest() {
            var mockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
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
                    Content = Utils.CreateJsonStringContent(new ReadResponse() {
                        Tuples = new List<Model.Tuple>() {
                                new(new TupleKey("repo:auth0/express-jwt", "reader", "anne"), DateTime.Now)
                            }
                    }),
                });

            var httpClient = new HttpClient(mockHandler.Object);
            var auth0FgaApi = new Auth0FgaApi(_config, httpClient);

            var body = new ReadRequestParams(new TupleKey("repo:auth0/express-jwt", "reader", "anne"));
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

            var body = new WriteAssertionsRequestParams(assertions: new List<Assertion>() {
                new(new TupleKey("repo:auth0/express-jwt", "reader", "anne"), true)
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

        /// <summary>
        /// Test WriteSettings
        /// </summary>
        [Fact]
        public async Task WriteSettingsTest() {
            var mockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req =>
                        req.RequestUri ==
                        new Uri($"{_config.BasePath}/stores/{_config.StoreId}/settings") &&
                        req.Method == HttpMethod.Patch),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage() {
                    StatusCode = HttpStatusCode.OK,
                    Content = Utils.CreateJsonStringContent(
                        new WriteSettingsResponse() { Environment = Environment.PRODUCTION }),
                });

            var httpClient = new HttpClient(mockHandler.Object);
            var auth0FgaApi = new Auth0FgaApi(_config, httpClient);

            var body = new WriteSettingsRequestParams(Environment.PRODUCTION);
            var response = await auth0FgaApi.WriteSettings(body);

            mockHandler.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.RequestUri ==
                    new Uri($"{_config.BasePath}/stores/{_config.StoreId}/settings") &&
                    req.Method == HttpMethod.Patch),
                ItExpr.IsAny<CancellationToken>()
            );

            Assert.IsType<WriteSettingsResponse>(response);
            Assert.Equal(Environment.PRODUCTION, response.Environment);
        }

        /// <summary>
        /// Test ReadSettings
        /// </summary>
        [Fact]
        public async Task ReadSettingsTest() {
            var mockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req =>
                        req.RequestUri ==
                        new Uri($"{_config.BasePath}/stores/{_config.StoreId}/settings") &&
                        req.Method == HttpMethod.Get),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage() {
                    StatusCode = HttpStatusCode.OK,
                    Content = Utils.CreateJsonStringContent(new ReadSettingsResponse() { Environment = Environment.PRODUCTION }),
                });

            var httpClient = new HttpClient(mockHandler.Object);
            var auth0FgaApi = new Auth0FgaApi(_config, httpClient);

            var response = await auth0FgaApi.ReadSettings();

            mockHandler.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.RequestUri ==
                    new Uri($"{_config.BasePath}/stores/{_config.StoreId}/settings") &&
                    req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>()
            );

            Assert.IsType<ReadSettingsResponse>(response);
            Assert.Equal(Environment.PRODUCTION, response.Environment);
            Assert.IsType<ReadSettingsResponse>(response);
        }

        /// <summary>
        /// Test WriteTokenIssuer
        /// </summary>
        [Fact]
        public async Task WriteTokenIssuerTest() {
            var tokenIssuer = new TokenIssuer() {
                Id = "86331426-5422-42e1-be6d-fd52c352e364",
                IssuerUrl = "https://fga.auth0.example"
            };
            var mockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req =>
                        req.RequestUri ==
                        new Uri($"{_config.BasePath}/stores/{_config.StoreId}/settings/token-issuers") &&
                        req.Method == HttpMethod.Post),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage() {
                    StatusCode = HttpStatusCode.OK,
                    Content = Utils.CreateJsonStringContent(new WriteTokenIssuersResponse() { Id = tokenIssuer.Id }),
                });

            var httpClient = new HttpClient(mockHandler.Object);
            var auth0FgaApi = new Auth0FgaApi(_config, httpClient);

            var body = new WriteTokenIssuersRequestParams(tokenIssuer.IssuerUrl);
            var response = await auth0FgaApi.WriteTokenIssuer(body);

            mockHandler.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.RequestUri ==
                    new Uri($"{_config.BasePath}/stores/{_config.StoreId}/settings/token-issuers") &&
                    req.Method == HttpMethod.Post),
                ItExpr.IsAny<CancellationToken>()
            );

            Assert.IsType<WriteTokenIssuersResponse>(response);
            Assert.Equal(tokenIssuer.Id, response.Id);
        }

        /// <summary>
        /// Test DeleteTokenIssuer
        /// </summary>
        [Fact]
        public async Task DeleteTokenIssuerTest() {
            const string issuerId = "86331426-5422-42e1-be6d-fd52c352e364";

            var mockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req =>
                        req.RequestUri ==
                        new Uri($"{_config.BasePath}/stores/{_config.StoreId}/settings/token-issuers/{issuerId}") &&
                        req.Method == HttpMethod.Delete),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage() {
                    StatusCode = HttpStatusCode.NoContent,
                });

            var httpClient = new HttpClient(mockHandler.Object);
            var auth0FgaApi = new Auth0FgaApi(_config, httpClient);

            await auth0FgaApi.DeleteTokenIssuer(issuerId);

            mockHandler.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.RequestUri ==
                    new Uri($"{_config.BasePath}/stores/{_config.StoreId}/settings/token-issuers/{issuerId}") &&
                    req.Method == HttpMethod.Delete),
                ItExpr.IsAny<CancellationToken>()
            );
        }
    }
}
