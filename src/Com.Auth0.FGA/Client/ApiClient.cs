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


using Com.Auth0.FGA.Exceptions;

namespace Com.Auth0.FGA.Client;

public class ApiClient : IDisposable {
    private readonly BaseClient _baseClient;
    private readonly OAuth2Client? _oauth2Client;
    private readonly Configuration.Configuration _configuration;

    public ApiClient(Configuration.Configuration configuration, HttpClient? userHttpClient = null) {
        configuration.IsValid();
        _configuration = configuration;
        _baseClient = new BaseClient(configuration, userHttpClient);

        if (!string.IsNullOrEmpty(_configuration.ClientId) || !_configuration.AllowNoAuth) {
            _oauth2Client = new OAuth2Client(configuration, _baseClient);
        }
    }

    public async Task<T> SendRequestAsync<T>(RequestBuilder requestBuilder, string apiName,
        CancellationToken cancellationToken = default) {
        IDictionary<string, string> additionalHeaders = new Dictionary<string, string>();

        if (_oauth2Client != null) {
            try {
                var token = await _oauth2Client.GetAccessTokenAsync();

                if (!string.IsNullOrEmpty(token)) {
                    additionalHeaders.Add("Authorization", $"Bearer {token}");
                }
            } catch (ApiException e) {
                throw new Auth0FgaAuthenticationError("Invalid Client Credentials", apiName, e);
            }
        }

        return await Retry(async () => await _baseClient.SendRequestAsync<T>(requestBuilder, additionalHeaders, apiName, cancellationToken));
    }

    private async Task<TResult> Retry<TResult>(Func<Task<TResult>> retryable) {
        var numRetries = 0;
        while (true) {
            try {
                numRetries++;

                return await retryable();
            } catch (Auth0FgaApiRateLimitExceededError err) {
                if (numRetries > _configuration.MaxRetry) {
                    throw;
                }
                var waitInMs = (int) ((err.ResetInMs == null || err.ResetInMs < _configuration.MinWaitInMs)
                    ? _configuration.MinWaitInMs
                    : err.ResetInMs);

                await Task.Delay(waitInMs);
            }
        }
    }

    public void Dispose() {
        _baseClient.Dispose();
    }
}
