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


using Com.Auth0.FGA.Client;
using Com.Auth0.FGA.Model;

namespace Com.Auth0.FGA.Api;

public class Auth0FgaApi : IDisposable {
    private Configuration.Configuration _configuration;
    private ApiClient _apiClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="Auth0FgaApi"/> class.
    /// </summary>
    /// <param name="storeId"></param>
    /// <param name="clientId"></param>
    /// <param name="clientSecret"></param>
    /// <param name="environment"></param>
    /// <param name="defaultHeaders"></param>
    /// <param name="httpClient"></param>
    public Auth0FgaApi(
        string storeId,
        string? clientId,
        string? clientSecret,
        string? environment,
        IDictionary<string, string>? defaultHeaders,
        HttpClient? httpClient = null
    ) : this(new Configuration.Configuration(storeId, environment) {
        ClientId = clientId,
        ClientSecret = clientSecret,
        DefaultHeaders = defaultHeaders ?? new Dictionary<string, string>()
    }, httpClient) {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Auth0FgaApi"/> class.
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="httpClient"></param>
    public Auth0FgaApi(
        Configuration.Configuration configuration,
        HttpClient? httpClient = null
    ) {
        this._configuration = configuration;
        this._apiClient = new ApiClient(_configuration, httpClient);
    }

    /// <summary>
    /// Check whether a user is authorized to access an object The check API will return whether the user has a certain relationship with an object in a certain store. Path parameter &#x60;store_id&#x60; as well as body parameter &#x60;object&#x60;, &#x60;relation&#x60; and &#x60;user&#x60; are all required. The response will return whether the relationship exists in the field &#x60;allowed&#x60;.  ## [Limits](https://docs.fga.dev/intro/dashboard#limitations) - Each store has a limit of **25** check requests per second (RPS). ## Example In order to check if user &#x60;anne@auth0.com&#x60; has an owner relationship with object document:2021-budget, a check API call should be fired with the following body &#x60;&#x60;&#x60;json {   \&quot;tuple_key\&quot;: {     \&quot;user\&quot;: \&quot;anne@auth0.com\&quot;,     \&quot;relation\&quot;: \&quot;owner\&quot;     \&quot;object\&quot;: \&quot;document:2021-budget\&quot;   } } &#x60;&#x60;&#x60; Auth0 FGA&#39;s response will include &#x60;{ \&quot;allowed\&quot;: true }&#x60; if there is a relationship and &#x60;{ \&quot;allowed\&quot;: false }&#x60; if there isn&#39;t.
    /// </summary>
    /// <exception cref="Exceptions.ApiException">Thrown when fails to make API call</exception>
    /// <param name="body"></param>
    /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
    /// <returns>Task of CheckResponse</returns>
    public async Task<CheckResponse> Check(CheckRequestParams body, CancellationToken cancellationToken = default) {
        var pathParams = new Dictionary<string, string> { { "store_id", _configuration.StoreId } };

        var queryParams = new Dictionary<string, string>();

        var requestBuilder = new RequestBuilder {
            Method = new HttpMethod("POST"),
            BasePath = _configuration.BasePath,
            PathTemplate = "/stores/{store_id}/check",
            PathParameters = pathParams,
            Body = Utils.CreateJsonStringContent(body),
            QueryParameters = queryParams
        };

        return await this._apiClient.SendRequestAsync<CheckResponse>(requestBuilder,
            "Check", cancellationToken);
    }

    /// <summary>
    /// Remove 3rd party token issuer for Auth0 FGA read and write operations The DELETE token-issuers API will remove the specified 3rd party token issuer as a token issuer that is allowed by Auth0 FGA. The specified id is the id associated with the issuer url that is to be removed. ## Example To remove the 3rd party token issuer &#x60;https://example.issuer.com&#x60; (which has the id &#x60;0ujsszwN8NRY24YaXiTIE2VWDTS&#x60;), call DELETE token-issuers API with the path parameter id &#x60;0ujsszwN8NRY24YaXiTIE2VWDTS&#x60;.
    /// </summary>
    /// <exception cref="Exceptions.ApiException">Thrown when fails to make API call</exception>
    /// <param name="id">Id of token issuer to be removed</param>
    /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
    /// <returns>Task of void</returns>
    public async Task DeleteTokenIssuer(string id, CancellationToken cancellationToken = default) {
        var pathParams = new Dictionary<string, string> { { "store_id", _configuration.StoreId } };

        if (id != null) {
            pathParams.Add("id", id.ToString());
        }
        var queryParams = new Dictionary<string, string>();

        var requestBuilder = new RequestBuilder {
            Method = new HttpMethod("DELETE"),
            BasePath = _configuration.BasePath,
            PathTemplate = "/stores/{store_id}/settings/token-issuers/{id}",
            PathParameters = pathParams,
            QueryParameters = queryParams
        };

        await this._apiClient.SendRequestAsync(requestBuilder,
            "DeleteTokenIssuer", cancellationToken);
    }

    /// <summary>
    /// Expand all relationships in userset tree format, and following userset rewrite rules.  Useful to reason about and debug a certain relationship The expand API will return all users (including user and userset) that have certain relationship with an object in a certain store. This is different from the &#x60;/stores/{store_id}/read&#x60; API in that both users and computed references are returned. Path parameter &#x60;store_id&#x60; as well as body parameter &#x60;object&#x60;, &#x60;relation&#x60; are all required. The response will return a userset tree whose leaves are the user id and usersets.  Union, intersection and difference operator are located in the intermediate nodes.  ## [Limits](https://docs.fga.dev/intro/dashboard#limitations) - Each store has a limit of **30** expand requests per minute (RPM). ## Example Assume the following type definition for document: &#x60;&#x60;&#x60;yaml   type document     relations       define reader as self or writer       define writer as self &#x60;&#x60;&#x60; In order to expand all users that have &#x60;reader&#x60; relationship with object &#x60;document:2021-budget&#x60;, an expand API call should be fired with the following body &#x60;&#x60;&#x60;json {   \&quot;tuple_key\&quot;: {     \&quot;object\&quot;: \&quot;document:2021-budget\&quot;,     \&quot;relation\&quot;: \&quot;reader\&quot;   } } &#x60;&#x60;&#x60; Auth0 FGA&#39;s response will be a userset tree of the users and computed usersets that have read access to the document. &#x60;&#x60;&#x60;json {   \&quot;tree\&quot;:{     \&quot;root\&quot;:{       \&quot;type\&quot;:\&quot;document:2021-budget#reader\&quot;,       \&quot;union\&quot;:{         \&quot;nodes\&quot;:[           {             \&quot;type\&quot;:\&quot;document:2021-budget#reader\&quot;,             \&quot;leaf\&quot;:{               \&quot;users\&quot;:{                 \&quot;users\&quot;:[                   \&quot;bob@auth0.com\&quot;                 ]               }             }           },           {             \&quot;type\&quot;:\&quot;document:2021-budget#reader\&quot;,             \&quot;leaf\&quot;:{               \&quot;computed\&quot;:{                 \&quot;userset\&quot;:\&quot;document:2021-budget#writer\&quot;               }             }           }         ]       }     }   } } &#x60;&#x60;&#x60; The caller can then call expand API for the &#x60;writer&#x60; relationship for the &#x60;document:2021-budget&#x60;.
    /// </summary>
    /// <exception cref="Exceptions.ApiException">Thrown when fails to make API call</exception>
    /// <param name="body"></param>
    /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
    /// <returns>Task of ExpandResponse</returns>
    public async Task<ExpandResponse> Expand(ExpandRequestParams body, CancellationToken cancellationToken = default) {
        var pathParams = new Dictionary<string, string> { { "store_id", _configuration.StoreId } };

        var queryParams = new Dictionary<string, string>();

        var requestBuilder = new RequestBuilder {
            Method = new HttpMethod("POST"),
            BasePath = _configuration.BasePath,
            PathTemplate = "/stores/{store_id}/expand",
            PathParameters = pathParams,
            Body = Utils.CreateJsonStringContent(body),
            QueryParameters = queryParams
        };

        return await this._apiClient.SendRequestAsync<ExpandResponse>(requestBuilder,
            "Expand", cancellationToken);
    }

    /// <summary>
    /// Get tuples from the store that matches a query, without following userset rewrite rules The POST read API will return the tuples for a certain store that matches a query filter specified in the body. Tuples and type definitions allow Auth0 FGA to determine whether a relationship exists between an object and an user. It is different from the &#x60;/stores/{store_id}/expand&#x60; API in that only read returns relationship tuples that are stored in the system and satisfy the query. It does not expand or traverse the graph by taking the authorization model into account.Path parameter &#x60;store_id&#x60; is required.  In the body: 1. Object is mandatory. An object can be a full object (e.g., &#x60;type:object_id&#x60;) or type only (e.g., &#x60;type:&#x60;). 2. User is mandatory in the case the object is type only. ## [Limits](https://docs.fga.dev/intro/dashboard#limitations) - Each store has a limit of **5** read requests per second (RPS). ## Examples ### Query for all objects in a type definition To query for all objects that &#x60;bob@auth0.com&#x60; has &#x60;reader&#x60; relationship in the document type definition, call read API with body of &#x60;&#x60;&#x60;json {  \&quot;tuple_key\&quot;: {      \&quot;user\&quot;: \&quot;bob@auth0.com\&quot;,      \&quot;relation\&quot;: \&quot;reader\&quot;,      \&quot;object\&quot;: \&quot;document:\&quot;   } } &#x60;&#x60;&#x60; The API will return tuples and an optional continuation token, something like &#x60;&#x60;&#x60;json {   \&quot;tuples\&quot;: [     {       \&quot;key\&quot;: {         \&quot;user\&quot;: \&quot;bob@auth0.com\&quot;,         \&quot;relation\&quot;: \&quot;reader\&quot;,         \&quot;object\&quot;: \&quot;document:2021-budget\&quot;       },       \&quot;timestamp\&quot;: \&quot;2021-10-06T15:32:11.128Z\&quot;     }   ] } &#x60;&#x60;&#x60; This means that &#x60;bob@auth0.com&#x60; has a &#x60;reader&#x60; relationship with 1 document &#x60;document:2021-budget&#x60;. ### Query for all users with particular relationships for a particular document To query for all users that have &#x60;reader&#x60; relationship with &#x60;document:2021-budget&#x60;, call read API with body of  &#x60;&#x60;&#x60;json {   \&quot;tuple_key\&quot;: {      \&quot;object\&quot;: \&quot;document:2021-budget\&quot;,      \&quot;relation\&quot;: \&quot;reader\&quot;    } } &#x60;&#x60;&#x60; The API will return something like  &#x60;&#x60;&#x60;json {   \&quot;tuples\&quot;: [     {       \&quot;key\&quot;: {         \&quot;user\&quot;: \&quot;bob@auth0.com\&quot;,         \&quot;relation\&quot;: \&quot;reader\&quot;,         \&quot;object\&quot;: \&quot;document:2021-budget\&quot;       },       \&quot;timestamp\&quot;: \&quot;2021-10-06T15:32:11.128Z\&quot;     }   ] } &#x60;&#x60;&#x60; This means that &#x60;document:2021-budget&#x60; has 1 &#x60;reader&#x60; (&#x60;bob@auth0.com&#x60;).  Note that the API will not return writers such as &#x60;anne@auth0.com&#x60; even when all writers are readers.  This is because only direct relationship are returned for the READ API. ### Query for all users with all relationships for a particular document To query for all users that have any relationship with &#x60;document:2021-budget&#x60;, call read API with body of  &#x60;&#x60;&#x60;json {   \&quot;tuple_key\&quot;: {       \&quot;object\&quot;: \&quot;document:2021-budget\&quot;    } } &#x60;&#x60;&#x60; The API will return something like  &#x60;&#x60;&#x60;json {   \&quot;tuples\&quot;: [     {       \&quot;key\&quot;: {         \&quot;user\&quot;: \&quot;anne@auth0.com\&quot;,         \&quot;relation\&quot;: \&quot;writer\&quot;,         \&quot;object\&quot;: \&quot;document:2021-budget\&quot;       },       \&quot;timestamp\&quot;: \&quot;2021-10-05T13:42:12.356Z\&quot;     },     {       \&quot;key\&quot;: {         \&quot;user\&quot;: \&quot;bob@auth0.com\&quot;,         \&quot;relation\&quot;: \&quot;reader\&quot;,         \&quot;object\&quot;: \&quot;document:2021-budget\&quot;       },       \&quot;timestamp\&quot;: \&quot;2021-10-06T15:32:11.128Z\&quot;     }   ] } &#x60;&#x60;&#x60; This means that &#x60;document:2021-budget&#x60; has 1 &#x60;reader&#x60; (&#x60;bob@auth0.com&#x60;) and 1 &#x60;writer&#x60; (&#x60;anne@auth0.com&#x60;). 
    /// </summary>
    /// <exception cref="Exceptions.ApiException">Thrown when fails to make API call</exception>
    /// <param name="body"></param>
    /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
    /// <returns>Task of ReadResponse</returns>
    public async Task<ReadResponse> Read(ReadRequestParams body, CancellationToken cancellationToken = default) {
        var pathParams = new Dictionary<string, string> { { "store_id", _configuration.StoreId } };

        var queryParams = new Dictionary<string, string>();

        var requestBuilder = new RequestBuilder {
            Method = new HttpMethod("POST"),
            BasePath = _configuration.BasePath,
            PathTemplate = "/stores/{store_id}/read",
            PathParameters = pathParams,
            Body = Utils.CreateJsonStringContent(body),
            QueryParameters = queryParams
        };

        return await this._apiClient.SendRequestAsync<ReadResponse>(requestBuilder,
            "Read", cancellationToken);
    }

    /// <summary>
    /// Read assertions for an authorization model ID The GET assertions API will return, for a given authorization model id, all the assertions stored for it. An assertion is an object that contains a tuple key, and the expectation of whether a call to the Check API of that tuple key will return true or false. 
    /// </summary>
    /// <exception cref="Exceptions.ApiException">Thrown when fails to make API call</exception>
    /// <param name="authorizationModelId"></param>
    /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
    /// <returns>Task of ReadAssertionsResponse</returns>
    public async Task<ReadAssertionsResponse> ReadAssertions(string authorizationModelId, CancellationToken cancellationToken = default) {
        var pathParams = new Dictionary<string, string> { { "store_id", _configuration.StoreId } };

        if (authorizationModelId != null) {
            pathParams.Add("authorization_model_id", authorizationModelId.ToString());
        }
        var queryParams = new Dictionary<string, string>();

        var requestBuilder = new RequestBuilder {
            Method = new HttpMethod("GET"),
            BasePath = _configuration.BasePath,
            PathTemplate = "/stores/{store_id}/assertions/{authorization_model_id}",
            PathParameters = pathParams,
            QueryParameters = queryParams
        };

        return await this._apiClient.SendRequestAsync<ReadAssertionsResponse>(requestBuilder,
            "ReadAssertions", cancellationToken);
    }

    /// <summary>
    /// Return a particular version of an authorization model The GET authorization-models by ID API will return a particular version of authorization model that had been configured for a certain store.   Path parameter &#x60;store_id&#x60; and &#x60;id&#x60; are required. The response will return the authorization model for the particular version.  ## [Limits](https://docs.fga.dev/intro/dashboard#limitations) - Each store has a limit of **30** read authorization-models requests per minute (RPM). ## Example To retrieve the authorization model with ID &#x60;1yunpF9DkzXMzm0dHrsCuWsooEV&#x60; for the store, call the GET authorization-models by ID API with &#x60;1yunpF9DkzXMzm0dHrsCuWsooEV&#x60; as the &#x60;id&#x60; path parameter.  The API will return: &#x60;&#x60;&#x60;json {   \&quot;authorization_model\&quot;:{     \&quot;id\&quot;:\&quot;1yunpF9DkzXMzm0dHrsCuWsooEV\&quot;,     \&quot;type_definitions\&quot;:[       {         \&quot;type\&quot;:\&quot;document\&quot;,         \&quot;relations\&quot;:{           \&quot;reader\&quot;:{             \&quot;union\&quot;:{               \&quot;child\&quot;:[                 {                   \&quot;this\&quot;:{}                 },                 {                   \&quot;computedUserset\&quot;:{                     \&quot;object\&quot;:\&quot;\&quot;,                     \&quot;relation\&quot;:\&quot;writer\&quot;                   }                 }               ]             }           },           \&quot;writer\&quot;:{             \&quot;this\&quot;:{}           }         }       }     ]   } } &#x60;&#x60;&#x60; In the above example, there is only 1 type (&#x60;document&#x60;) with 2 relations (&#x60;writer&#x60; and &#x60;reader&#x60;).
    /// </summary>
    /// <exception cref="Exceptions.ApiException">Thrown when fails to make API call</exception>
    /// <param name="id"></param>
    /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
    /// <returns>Task of ReadAuthorizationModelResponse</returns>
    public async Task<ReadAuthorizationModelResponse> ReadAuthorizationModel(string id, CancellationToken cancellationToken = default) {
        var pathParams = new Dictionary<string, string> { { "store_id", _configuration.StoreId } };

        if (id != null) {
            pathParams.Add("id", id.ToString());
        }
        var queryParams = new Dictionary<string, string>();

        var requestBuilder = new RequestBuilder {
            Method = new HttpMethod("GET"),
            BasePath = _configuration.BasePath,
            PathTemplate = "/stores/{store_id}/authorization-models/{id}",
            PathParameters = pathParams,
            QueryParameters = queryParams
        };

        return await this._apiClient.SendRequestAsync<ReadAuthorizationModelResponse>(requestBuilder,
            "ReadAuthorizationModel", cancellationToken);
    }

    /// <summary>
    /// Return all the authorization model IDs for a particular store The GET authorization-models API will return all the IDs of the authorization models for a certain store. Path parameter &#x60;store_id&#x60; is required. Auth0 FGA&#39;s response will contain an array of all authorization model IDs, sorted in descending order of creation.  ## [Limits](https://docs.fga.dev/intro/dashboard#limitations) - Each store has a limit of **30** read authorization-models requests per minute (RPM). - Each response can contain up to **50** authorization model IDs. ## Example Assume that the store&#39;s authorization model has been configured twice.  To get all the IDs of the authorization models that had been created in this store, call GET authorization-models.  The API will return a response that looks like: &#x60;&#x60;&#x60;json {   \&quot;authorization_model_ids\&quot;: [       \&quot;1yunpF9DkzXMzm0dHrsCuWsooEV\&quot;,       \&quot;1yundoHpJHlodgn4EOVar2DhmKp\&quot;   ] } &#x60;&#x60;&#x60; If there are more authorization model IDs available, the response will contain an extra field &#x60;continuation_token&#x60;: &#x60;&#x60;&#x60;json {   \&quot;authorization_model_ids\&quot;: [       \&quot;1yunpF9DkzXMzm0dHrsCuWsooEV\&quot;,       \&quot;1yundoHpJHlodgn4EOVar2DhmKp\&quot;   ],   \&quot;continuation_token\&quot;: \&quot;eyJwayI6IkxBVEVTVF9OU0NPTkZJR19hdXRoMHN0b3JlIiwic2siOiIxem1qbXF3MWZLZExTcUoyN01MdTdqTjh0cWgifQ&#x3D;&#x3D;\&quot; } &#x60;&#x60;&#x60; 
    /// </summary>
    /// <exception cref="Exceptions.ApiException">Thrown when fails to make API call</exception>
    /// <param name="pageSize"> (optional)</param>
    /// <param name="continuationToken"> (optional)</param>
    /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
    /// <returns>Task of ReadAuthorizationModelsResponse</returns>
    public async Task<ReadAuthorizationModelsResponse> ReadAuthorizationModels(int? pageSize = default(int?), string? continuationToken = default(string?), CancellationToken cancellationToken = default) {
        var pathParams = new Dictionary<string, string> { { "store_id", _configuration.StoreId } };

        var queryParams = new Dictionary<string, string>();
        if (pageSize != null) {
            queryParams.Add("page_size", pageSize.ToString());
        }
        if (continuationToken != null) {
            queryParams.Add("continuation_token", continuationToken.ToString());
        }

        var requestBuilder = new RequestBuilder {
            Method = new HttpMethod("GET"),
            BasePath = _configuration.BasePath,
            PathTemplate = "/stores/{store_id}/authorization-models",
            PathParameters = pathParams,
            QueryParameters = queryParams
        };

        return await this._apiClient.SendRequestAsync<ReadAuthorizationModelsResponse>(requestBuilder,
            "ReadAuthorizationModels", cancellationToken);
    }

    /// <summary>
    /// Return store settings, including the environment tag The GET settings API will return the store&#39;s settings, including environment tag and an array of Auth0 FGA&#39;s allowed 3rd party token issuers. The environment tag is used to differentiate between development, staging, and production environments.   Path parameter &#x60;store_id&#x60; is required. ## Example GET settings API&#39;s response looks like: &#x60;&#x60;&#x60;json {   \&quot;environment\&quot;:\&quot;STAGING\&quot;,   \&quot;token_issuers\&quot;:[     {       \&quot;id\&quot;:\&quot;0ujsszwN8NRY24YaXiTIE2VWDTS\&quot;,       \&quot;issuer_url\&quot;:\&quot;https://example.issuer.com\&quot;     }   ] } &#x60;&#x60;&#x60; In the above response, the store is configured as STAGING and there is one allowed 3rd party token issuer &#x60;https://example.issuer.com&#x60;.
    /// </summary>
    /// <exception cref="Exceptions.ApiException">Thrown when fails to make API call</exception>
    /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
    /// <returns>Task of ReadSettingsResponse</returns>
    public async Task<ReadSettingsResponse> ReadSettings(CancellationToken cancellationToken = default) {
        var pathParams = new Dictionary<string, string> { { "store_id", _configuration.StoreId } };

        var queryParams = new Dictionary<string, string>();

        var requestBuilder = new RequestBuilder {
            Method = new HttpMethod("GET"),
            BasePath = _configuration.BasePath,
            PathTemplate = "/stores/{store_id}/settings",
            PathParameters = pathParams,
            QueryParameters = queryParams
        };

        return await this._apiClient.SendRequestAsync<ReadSettingsResponse>(requestBuilder,
            "ReadSettings", cancellationToken);
    }

    /// <summary>
    /// Add or delete tuples from the store The POST write API will update the tuples for a certain store.  Tuples and type definitions allow Auth0 FGA to determine whether a relationship exists between an object and an user. Path parameter &#x60;store_id&#x60; is required.  In the body, &#x60;writes&#x60; adds new tuples while &#x60;deletes&#x60; remove existing tuples.  &#x60;lock_tuple&#x60; is reserved for future use.  ## [Limits](https://docs.fga.dev/intro/dashboard#limitations) - Each write API call allows at most **10** tuples. - Each store has a limit of **50000** tuples. - Each store has a limit of **3** write requests per second (RPS). ## Example ### Adding relationships To add &#x60;anne@auth0.com&#x60; as a &#x60;writer&#x60; for &#x60;document:2021-budget&#x60;, call write API with the following  &#x60;&#x60;&#x60;json {   \&quot;writes\&quot;: {     \&quot;tuple_keys\&quot;: [       {         \&quot;user\&quot;: \&quot;anne@auth0.com\&quot;,         \&quot;relation\&quot;: \&quot;writer\&quot;,         \&quot;object\&quot;: \&quot;document:2021-budget\&quot;       }     ]   } } &#x60;&#x60;&#x60; ### Removing relationships To remove &#x60;bob@auth0.com&#x60; as a &#x60;reader&#x60; for &#x60;document:2021-budget&#x60;, call write API with the following  &#x60;&#x60;&#x60;json {   \&quot;deletes\&quot;: {     \&quot;tuple_keys\&quot;: [       {         \&quot;user\&quot;: \&quot;bob@auth0.com\&quot;,         \&quot;relation\&quot;: \&quot;reader\&quot;,         \&quot;object\&quot;: \&quot;document:2021-budget\&quot;       }     ]   } } &#x60;&#x60;&#x60; 
    /// </summary>
    /// <exception cref="Exceptions.ApiException">Thrown when fails to make API call</exception>
    /// <param name="body"></param>
    /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
    /// <returns>Task of Object</returns>
    public async Task<Object> Write(WriteRequestParams body, CancellationToken cancellationToken = default) {
        var pathParams = new Dictionary<string, string> { { "store_id", _configuration.StoreId } };

        var queryParams = new Dictionary<string, string>();

        var requestBuilder = new RequestBuilder {
            Method = new HttpMethod("POST"),
            BasePath = _configuration.BasePath,
            PathTemplate = "/stores/{store_id}/write",
            PathParameters = pathParams,
            Body = Utils.CreateJsonStringContent(body),
            QueryParameters = queryParams
        };

        return await this._apiClient.SendRequestAsync<Object>(requestBuilder,
            "Write", cancellationToken);
    }

    /// <summary>
    /// Upsert assertions for an authorization model ID The POST assertions API will add new assertions for an authorization model id, or overwrite the existing ones. An assertion is an object that contains a tuple key, and the expectation of whether a call to the Check API of that tuple key will return true or false. 
    /// </summary>
    /// <exception cref="Exceptions.ApiException">Thrown when fails to make API call</exception>
    /// <param name="authorizationModelId"></param>
    /// <param name="body"></param>
    /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
    /// <returns>Task of void</returns>
    public async Task WriteAssertions(string authorizationModelId, WriteAssertionsRequestParams body, CancellationToken cancellationToken = default) {
        var pathParams = new Dictionary<string, string> { { "store_id", _configuration.StoreId } };

        if (authorizationModelId != null) {
            pathParams.Add("authorization_model_id", authorizationModelId.ToString());
        }
        var queryParams = new Dictionary<string, string>();

        var requestBuilder = new RequestBuilder {
            Method = new HttpMethod("PUT"),
            BasePath = _configuration.BasePath,
            PathTemplate = "/stores/{store_id}/assertions/{authorization_model_id}",
            PathParameters = pathParams,
            Body = Utils.CreateJsonStringContent(body),
            QueryParameters = queryParams
        };

        await this._apiClient.SendRequestAsync(requestBuilder,
            "WriteAssertions", cancellationToken);
    }

    /// <summary>
    /// Create a new authorization model The POST authorization-model API will update the authorization model for a certain store. Path parameter &#x60;store_id&#x60; and &#x60;type_definitions&#x60; array in the body are required.  Each item in the &#x60;type_definitions&#x60; array is a type definition as specified in the field &#x60;type_definition&#x60;. The response will return the authorization model&#39;s ID in the &#x60;id&#x60; field.  ## [Limits](https://docs.fga.dev/intro/dashboard#limitations) - There can be at most **24** items in the type_definitions array. - Each store has a limit of **10** POST authorization-models requests per minute (RPM). ## Example To update the authorization model with a single &#x60;document&#x60; authorization model, call POST authorization-models API with the body:  &#x60;&#x60;&#x60;json {   \&quot;type_definitions\&quot;:[     {       \&quot;type\&quot;:\&quot;document\&quot;,       \&quot;relations\&quot;:{         \&quot;reader\&quot;:{           \&quot;union\&quot;:{             \&quot;child\&quot;:[               {                 \&quot;this\&quot;:{                  }               },               {                 \&quot;computedUserset\&quot;:{                   \&quot;object\&quot;:\&quot;\&quot;,                   \&quot;relation\&quot;:\&quot;writer\&quot;                 }               }             ]           }         },         \&quot;writer\&quot;:{           \&quot;this\&quot;:{            }         }       }     }   ] } &#x60;&#x60;&#x60; Auth0 FGA&#39;s response will include the version id for this authorization model, which will look like  &#x60;&#x60;&#x60; {\&quot;authorization_model_id\&quot;: \&quot;1yunpF9DkzXMzm0dHrsCuWsooEV\&quot;} &#x60;&#x60;&#x60; 
    /// </summary>
    /// <exception cref="Exceptions.ApiException">Thrown when fails to make API call</exception>
    /// <param name="body"></param>
    /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
    /// <returns>Task of WriteAuthorizationModelResponse</returns>
    public async Task<WriteAuthorizationModelResponse> WriteAuthorizationModel(TypeDefinitions body, CancellationToken cancellationToken = default) {
        var pathParams = new Dictionary<string, string> { { "store_id", _configuration.StoreId } };

        var queryParams = new Dictionary<string, string>();

        var requestBuilder = new RequestBuilder {
            Method = new HttpMethod("POST"),
            BasePath = _configuration.BasePath,
            PathTemplate = "/stores/{store_id}/authorization-models",
            PathParameters = pathParams,
            Body = Utils.CreateJsonStringContent(body),
            QueryParameters = queryParams
        };

        return await this._apiClient.SendRequestAsync<WriteAuthorizationModelResponse>(requestBuilder,
            "WriteAuthorizationModel", cancellationToken);
    }

    /// <summary>
    /// Update the environment tag for a store The PATCH settings API will update the environment tag to differentiate between development, staging, and production environments. Path parameter &#x60;store_id&#x60; is required. The response will return the updated environment tag as well as other configuration settings.  ## Example To update store&#39;s environment tag to &#x60;STAGING&#x60;, call PATCH settings API with the following with the body:  &#x60;&#x60;&#x60;json {\&quot;environment\&quot;: \&quot;STAGING\&quot;} &#x60;&#x60;&#x60; 
    /// </summary>
    /// <exception cref="Exceptions.ApiException">Thrown when fails to make API call</exception>
    /// <param name="body"></param>
    /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
    /// <returns>Task of WriteSettingsResponse</returns>
    public async Task<WriteSettingsResponse> WriteSettings(WriteSettingsRequestParams body, CancellationToken cancellationToken = default) {
        var pathParams = new Dictionary<string, string> { { "store_id", _configuration.StoreId } };

        var queryParams = new Dictionary<string, string>();

        var requestBuilder = new RequestBuilder {
            Method = new HttpMethod("PATCH"),
            BasePath = _configuration.BasePath,
            PathTemplate = "/stores/{store_id}/settings",
            PathParameters = pathParams,
            Body = Utils.CreateJsonStringContent(body),
            QueryParameters = queryParams
        };

        return await this._apiClient.SendRequestAsync<WriteSettingsResponse>(requestBuilder,
            "WriteSettings", cancellationToken);
    }

    /// <summary>
    /// Add 3rd party token issuer for Auth0 FGA read and write operations The POST token-issuers API will configure FGA so that tokens issued by the specified 3rd party token issuer will be allowed for Auth0 FGA&#39;s read and write operations. Otherwise, only tokens issued by Auth0 FGA&#39;s issuer (&#x60;fga.us.auth0.com&#x60;) will be accepted. ## Example To allow tokens issued by the 3rd party token issuer &#x60;https://example.issuer.com&#x60; for Auth0 FGA&#39;s read and write operations: 1. In the 3rd party issuer, configure Auth0 FGA API with the following audience in its issuer configuration: &#x60;https://api.us1.fga.dev&#x60;. 2. Call POST token-issuers API with the body: &#x60;{\&quot;issuer_url\&quot;: \&quot;https://example.issuer.com\&quot;}&#x60;  The response will be the id that is associated with the token issuer: &#x60;&#x60;&#x60;json {   \&quot;id\&quot;:\&quot;0ujsszwN8NRY24YaXiTIE2VWDTS\&quot; } &#x60;&#x60;&#x60; 
    /// </summary>
    /// <exception cref="Exceptions.ApiException">Thrown when fails to make API call</exception>
    /// <param name="body"></param>
    /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
    /// <returns>Task of WriteTokenIssuersResponse</returns>
    public async Task<WriteTokenIssuersResponse> WriteTokenIssuer(WriteTokenIssuersRequestParams body, CancellationToken cancellationToken = default) {
        var pathParams = new Dictionary<string, string> { { "store_id", _configuration.StoreId } };

        var queryParams = new Dictionary<string, string>();

        var requestBuilder = new RequestBuilder {
            Method = new HttpMethod("POST"),
            BasePath = _configuration.BasePath,
            PathTemplate = "/stores/{store_id}/settings/token-issuers",
            PathParameters = pathParams,
            Body = Utils.CreateJsonStringContent(body),
            QueryParameters = queryParams
        };

        return await this._apiClient.SendRequestAsync<WriteTokenIssuersResponse>(requestBuilder,
            "WriteTokenIssuer", cancellationToken);
    }


    public void Dispose() {
        _apiClient.Dispose();
    }
}
