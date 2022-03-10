# Auth0.Fga.Api.Auth0FgaApi

All URIs are relative to *https://api.us1.fga.dev*

Method | HTTP request | Description
------------- | ------------- | -------------
[**Check**](Auth0FgaApi.md#check) | **POST** /stores/{store_id}/check | Check whether a user is authorized to access an object
[**DeleteTokenIssuer**](Auth0FgaApi.md#deletetokenissuer) | **DELETE** /stores/{store_id}/settings/token-issuers/{id} | Remove 3rd party token issuer for Auth0 FGA read and write operations
[**Expand**](Auth0FgaApi.md#expand) | **POST** /stores/{store_id}/expand | Expand all relationships in userset tree format, and following userset rewrite rules.  Useful to reason about and debug a certain relationship
[**Read**](Auth0FgaApi.md#read) | **POST** /stores/{store_id}/read | Get tuples from the store that matches a query, without following userset rewrite rules
[**ReadAssertions**](Auth0FgaApi.md#readassertions) | **GET** /stores/{store_id}/assertions/{authorization_model_id} | Read assertions for an authorization model ID
[**ReadAuthorizationModel**](Auth0FgaApi.md#readauthorizationmodel) | **GET** /stores/{store_id}/authorization-models/{id} | Return a particular version of an authorization model
[**ReadAuthorizationModels**](Auth0FgaApi.md#readauthorizationmodels) | **GET** /stores/{store_id}/authorization-models | Return all the authorization model IDs for a particular store
[**ReadSettings**](Auth0FgaApi.md#readsettings) | **GET** /stores/{store_id}/settings | Return store settings, including the environment tag
[**Write**](Auth0FgaApi.md#write) | **POST** /stores/{store_id}/write | Add or delete tuples from the store
[**WriteAssertions**](Auth0FgaApi.md#writeassertions) | **PUT** /stores/{store_id}/assertions/{authorization_model_id} | Upsert assertions for an authorization model ID
[**WriteAuthorizationModel**](Auth0FgaApi.md#writeauthorizationmodel) | **POST** /stores/{store_id}/authorization-models | Create a new authorization model
[**WriteSettings**](Auth0FgaApi.md#writesettings) | **PATCH** /stores/{store_id}/settings | Update the environment tag for a store
[**WriteTokenIssuer**](Auth0FgaApi.md#writetokenissuer) | **POST** /stores/{store_id}/settings/token-issuers | Add 3rd party token issuer for Auth0 FGA read and write operations


<a name="check"></a>
# **Check**
> CheckResponse Check (CheckRequestParams body)

Check whether a user is authorized to access an object

The check API will return whether the user has a certain relationship with an object in a certain store. Path parameter `store_id` as well as body parameter `object`, `relation` and `user` are all required. The response will return whether the relationship exists in the field `allowed`.  ## [Limits](https://docs.fga.dev/intro/dashboard#limitations) - Each store has a limit of **25** check requests per second (RPS). ## Example In order to check if user `anne@auth0.com` has an owner relationship with object document:2021-budget, a check API call should be fired with the following body ```json {   \"tuple_key\": {     \"user\": \"anne@auth0.com\",     \"relation\": \"owner\"     \"object\": \"document:2021-budget\"   } } ``` Auth0 FGA's response will include `{ \"allowed\": true }` if there is a relationship and `{ \"allowed\": false }` if there isn't.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using Auth0.Fga.Api;
using Auth0.Fga.Client;
using Auth0.Fga.Configuration;
using Auth0.Fga.Model;

namespace Example
{
    public class CheckExample
    {
        public static void Main()
        {
            var storeId = Environment.GetEnvironmentVariable("AUTH0_FGA_STORE_ID");
            var environment = Environment.GetEnvironmentVariable("AUTH0_FGA_ENVIRONMENT")
            var configuration = new Configuration(storeId, environment) {
                ClientId = Environment.GetEnvironmentVariable("AUTH0_FGA_CLIENT_ID"),
                ClientSecret = Environment.GetEnvironmentVariable("AUTH0_FGA_CLIENT_SECRET"),
            };
            HttpClient httpClient = new HttpClient();
            var auth0FgaApi = new Auth0FgaApi(config, httpClient);
            var body = new CheckRequestParams(); // CheckRequestParams | 

            try
            {
                // Check whether a user is authorized to access an object
                CheckResponse response = await auth0FgaApi.Check(body);
                Debug.WriteLine(response);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling Auth0FgaApi.Check: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------

 **body** | [**CheckRequestParams**](CheckRequestParams.md)|  | 

### Return type

[**CheckResponse**](CheckResponse.md)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | A successful response. |  -  |
| **400** | Request failed due to invalid input. |  -  |
| **401** | Request failed due to authentication errors. |  -  |
| **403** | Request failed due to forbidden permission. |  -  |
| **404** | Request failed due to incorrect path. |  -  |
| **429** | Request failed due to too many requests. |  -  |
| **500** | Request failed due to internal server error. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#api-endpoints) [[Back to Model list]](../README.md#models) [[Back to README]](../README.md)

<a name="deletetokenissuer"></a>
# **DeleteTokenIssuer**
> void DeleteTokenIssuer (string id)

Remove 3rd party token issuer for Auth0 FGA read and write operations

The DELETE token-issuers API will remove the specified 3rd party token issuer as a token issuer that is allowed by Auth0 FGA. The specified id is the id associated with the issuer url that is to be removed. ## Example To remove the 3rd party token issuer `https://example.issuer.com` (which has the id `0ujsszwN8NRY24YaXiTIE2VWDTS`), call DELETE token-issuers API with the path parameter id `0ujsszwN8NRY24YaXiTIE2VWDTS`.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using Auth0.Fga.Api;
using Auth0.Fga.Client;
using Auth0.Fga.Configuration;
using Auth0.Fga.Model;

namespace Example
{
    public class DeleteTokenIssuerExample
    {
        public static void Main()
        {
            var storeId = Environment.GetEnvironmentVariable("AUTH0_FGA_STORE_ID");
            var environment = Environment.GetEnvironmentVariable("AUTH0_FGA_ENVIRONMENT")
            var configuration = new Configuration(storeId, environment) {
                ClientId = Environment.GetEnvironmentVariable("AUTH0_FGA_CLIENT_ID"),
                ClientSecret = Environment.GetEnvironmentVariable("AUTH0_FGA_CLIENT_SECRET"),
            };
            HttpClient httpClient = new HttpClient();
            var auth0FgaApi = new Auth0FgaApi(config, httpClient);
            var id = "id_example";  // string | Id of token issuer to be removed

            try
            {
                // Remove 3rd party token issuer for Auth0 FGA read and write operations
                auth0FgaApi.DeleteTokenIssuer(id);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling Auth0FgaApi.DeleteTokenIssuer: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------

 **id** | **string**| Id of token issuer to be removed | 

### Return type

void (empty response body)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **204** | A successful response. |  -  |
| **400** | Request failed due to invalid input. |  -  |
| **401** | Request failed due to authentication errors. |  -  |
| **403** | Request failed due to forbidden permission. |  -  |
| **404** | Request failed due to incorrect path. |  -  |
| **429** | Request failed due to too many requests. |  -  |
| **500** | Request failed due to internal server error. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#api-endpoints) [[Back to Model list]](../README.md#models) [[Back to README]](../README.md)

<a name="expand"></a>
# **Expand**
> ExpandResponse Expand (ExpandRequestParams body)

Expand all relationships in userset tree format, and following userset rewrite rules.  Useful to reason about and debug a certain relationship

The expand API will return all users (including user and userset) that have certain relationship with an object in a certain store. This is different from the `/stores/{store_id}/read` API in that both users and computed references are returned. Path parameter `store_id` as well as body parameter `object`, `relation` are all required. The response will return a userset tree whose leaves are the user id and usersets.  Union, intersection and difference operator are located in the intermediate nodes.  ## [Limits](https://docs.fga.dev/intro/dashboard#limitations) - Each store has a limit of **30** expand requests per minute (RPM). ## Example Assume the following type definition for document: ```yaml   type document     relations       define reader as self or writer       define writer as self ``` In order to expand all users that have `reader` relationship with object `document:2021-budget`, an expand API call should be fired with the following body ```json {   \"tuple_key\": {     \"object\": \"document:2021-budget\",     \"relation\": \"reader\"   } } ``` Auth0 FGA's response will be a userset tree of the users and computed usersets that have read access to the document. ```json {   \"tree\":{     \"root\":{       \"type\":\"document:2021-budget#reader\",       \"union\":{         \"nodes\":[           {             \"type\":\"document:2021-budget#reader\",             \"leaf\":{               \"users\":{                 \"users\":[                   \"bob@auth0.com\"                 ]               }             }           },           {             \"type\":\"document:2021-budget#reader\",             \"leaf\":{               \"computed\":{                 \"userset\":\"document:2021-budget#writer\"               }             }           }         ]       }     }   } } ``` The caller can then call expand API for the `writer` relationship for the `document:2021-budget`.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using Auth0.Fga.Api;
using Auth0.Fga.Client;
using Auth0.Fga.Configuration;
using Auth0.Fga.Model;

namespace Example
{
    public class ExpandExample
    {
        public static void Main()
        {
            var storeId = Environment.GetEnvironmentVariable("AUTH0_FGA_STORE_ID");
            var environment = Environment.GetEnvironmentVariable("AUTH0_FGA_ENVIRONMENT")
            var configuration = new Configuration(storeId, environment) {
                ClientId = Environment.GetEnvironmentVariable("AUTH0_FGA_CLIENT_ID"),
                ClientSecret = Environment.GetEnvironmentVariable("AUTH0_FGA_CLIENT_SECRET"),
            };
            HttpClient httpClient = new HttpClient();
            var auth0FgaApi = new Auth0FgaApi(config, httpClient);
            var body = new ExpandRequestParams(); // ExpandRequestParams | 

            try
            {
                // Expand all relationships in userset tree format, and following userset rewrite rules.  Useful to reason about and debug a certain relationship
                ExpandResponse response = await auth0FgaApi.Expand(body);
                Debug.WriteLine(response);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling Auth0FgaApi.Expand: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------

 **body** | [**ExpandRequestParams**](ExpandRequestParams.md)|  | 

### Return type

[**ExpandResponse**](ExpandResponse.md)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | A successful response. |  -  |
| **400** | Request failed due to invalid input. |  -  |
| **401** | Request failed due to authentication errors. |  -  |
| **403** | Request failed due to forbidden permission. |  -  |
| **404** | Request failed due to incorrect path. |  -  |
| **429** | Request failed due to too many requests. |  -  |
| **500** | Request failed due to internal server error. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#api-endpoints) [[Back to Model list]](../README.md#models) [[Back to README]](../README.md)

<a name="read"></a>
# **Read**
> ReadResponse Read (ReadRequestParams body)

Get tuples from the store that matches a query, without following userset rewrite rules

The POST read API will return the tuples for a certain store that matches a query filter specified in the body. Tuples and type definitions allow Auth0 FGA to determine whether a relationship exists between an object and an user. It is different from the `/stores/{store_id}/expand` API in that only read returns relationship tuples that are stored in the system and satisfy the query. It does not expand or traverse the graph by taking the authorization model into account.Path parameter `store_id` is required.  In the body: 1. Object is mandatory. An object can be a full object (e.g., `type:object_id`) or type only (e.g., `type:`). 2. User is mandatory in the case the object is type only. ## [Limits](https://docs.fga.dev/intro/dashboard#limitations) - Each store has a limit of **5** read requests per second (RPS). ## Examples ### Query for all objects in a type definition To query for all objects that `bob@auth0.com` has `reader` relationship in the document type definition, call read API with body of ```json {  \"tuple_key\": {      \"user\": \"bob@auth0.com\",      \"relation\": \"reader\",      \"object\": \"document:\"   } } ``` The API will return tuples and an optional continuation token, something like ```json {   \"tuples\": [     {       \"key\": {         \"user\": \"bob@auth0.com\",         \"relation\": \"reader\",         \"object\": \"document:2021-budget\"       },       \"timestamp\": \"2021-10-06T15:32:11.128Z\"     }   ] } ``` This means that `bob@auth0.com` has a `reader` relationship with 1 document `document:2021-budget`. ### Query for all users with particular relationships for a particular document To query for all users that have `reader` relationship with `document:2021-budget`, call read API with body of  ```json {   \"tuple_key\": {      \"object\": \"document:2021-budget\",      \"relation\": \"reader\"    } } ``` The API will return something like  ```json {   \"tuples\": [     {       \"key\": {         \"user\": \"bob@auth0.com\",         \"relation\": \"reader\",         \"object\": \"document:2021-budget\"       },       \"timestamp\": \"2021-10-06T15:32:11.128Z\"     }   ] } ``` This means that `document:2021-budget` has 1 `reader` (`bob@auth0.com`).  Note that the API will not return writers such as `anne@auth0.com` even when all writers are readers.  This is because only direct relationship are returned for the READ API. ### Query for all users with all relationships for a particular document To query for all users that have any relationship with `document:2021-budget`, call read API with body of  ```json {   \"tuple_key\": {       \"object\": \"document:2021-budget\"    } } ``` The API will return something like  ```json {   \"tuples\": [     {       \"key\": {         \"user\": \"anne@auth0.com\",         \"relation\": \"writer\",         \"object\": \"document:2021-budget\"       },       \"timestamp\": \"2021-10-05T13:42:12.356Z\"     },     {       \"key\": {         \"user\": \"bob@auth0.com\",         \"relation\": \"reader\",         \"object\": \"document:2021-budget\"       },       \"timestamp\": \"2021-10-06T15:32:11.128Z\"     }   ] } ``` This means that `document:2021-budget` has 1 `reader` (`bob@auth0.com`) and 1 `writer` (`anne@auth0.com`). 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using Auth0.Fga.Api;
using Auth0.Fga.Client;
using Auth0.Fga.Configuration;
using Auth0.Fga.Model;

namespace Example
{
    public class ReadExample
    {
        public static void Main()
        {
            var storeId = Environment.GetEnvironmentVariable("AUTH0_FGA_STORE_ID");
            var environment = Environment.GetEnvironmentVariable("AUTH0_FGA_ENVIRONMENT")
            var configuration = new Configuration(storeId, environment) {
                ClientId = Environment.GetEnvironmentVariable("AUTH0_FGA_CLIENT_ID"),
                ClientSecret = Environment.GetEnvironmentVariable("AUTH0_FGA_CLIENT_SECRET"),
            };
            HttpClient httpClient = new HttpClient();
            var auth0FgaApi = new Auth0FgaApi(config, httpClient);
            var body = new ReadRequestParams(); // ReadRequestParams | 

            try
            {
                // Get tuples from the store that matches a query, without following userset rewrite rules
                ReadResponse response = await auth0FgaApi.Read(body);
                Debug.WriteLine(response);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling Auth0FgaApi.Read: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------

 **body** | [**ReadRequestParams**](ReadRequestParams.md)|  | 

### Return type

[**ReadResponse**](ReadResponse.md)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | A successful response. |  -  |
| **400** | Request failed due to invalid input. |  -  |
| **401** | Request failed due to authentication errors. |  -  |
| **403** | Request failed due to forbidden permission. |  -  |
| **404** | Request failed due to incorrect path. |  -  |
| **429** | Request failed due to too many requests. |  -  |
| **500** | Request failed due to internal server error. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#api-endpoints) [[Back to Model list]](../README.md#models) [[Back to README]](../README.md)

<a name="readassertions"></a>
# **ReadAssertions**
> ReadAssertionsResponse ReadAssertions (string authorizationModelId)

Read assertions for an authorization model ID

The GET assertions API will return, for a given authorization model id, all the assertions stored for it. An assertion is an object that contains a tuple key, and the expectation of whether a call to the Check API of that tuple key will return true or false. 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using Auth0.Fga.Api;
using Auth0.Fga.Client;
using Auth0.Fga.Configuration;
using Auth0.Fga.Model;

namespace Example
{
    public class ReadAssertionsExample
    {
        public static void Main()
        {
            var storeId = Environment.GetEnvironmentVariable("AUTH0_FGA_STORE_ID");
            var environment = Environment.GetEnvironmentVariable("AUTH0_FGA_ENVIRONMENT")
            var configuration = new Configuration(storeId, environment) {
                ClientId = Environment.GetEnvironmentVariable("AUTH0_FGA_CLIENT_ID"),
                ClientSecret = Environment.GetEnvironmentVariable("AUTH0_FGA_CLIENT_SECRET"),
            };
            HttpClient httpClient = new HttpClient();
            var auth0FgaApi = new Auth0FgaApi(config, httpClient);
            var authorizationModelId = "authorizationModelId_example";  // string | 

            try
            {
                // Read assertions for an authorization model ID
                ReadAssertionsResponse response = await auth0FgaApi.ReadAssertions(authorizationModelId);
                Debug.WriteLine(response);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling Auth0FgaApi.ReadAssertions: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------

 **authorizationModelId** | **string**|  | 

### Return type

[**ReadAssertionsResponse**](ReadAssertionsResponse.md)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | A successful response. |  -  |
| **400** | Request failed due to invalid input. |  -  |
| **401** | Request failed due to authentication errors. |  -  |
| **403** | Request failed due to forbidden permission. |  -  |
| **404** | Request failed due to incorrect path. |  -  |
| **429** | Request failed due to too many requests. |  -  |
| **500** | Request failed due to internal server error. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#api-endpoints) [[Back to Model list]](../README.md#models) [[Back to README]](../README.md)

<a name="readauthorizationmodel"></a>
# **ReadAuthorizationModel**
> ReadAuthorizationModelResponse ReadAuthorizationModel (string id)

Return a particular version of an authorization model

The GET authorization-models by ID API will return a particular version of authorization model that had been configured for a certain store.   Path parameter `store_id` and `id` are required. The response will return the authorization model for the particular version.  ## [Limits](https://docs.fga.dev/intro/dashboard#limitations) - Each store has a limit of **30** read authorization-models requests per minute (RPM). ## Example To retrieve the authorization model with ID `1yunpF9DkzXMzm0dHrsCuWsooEV` for the store, call the GET authorization-models by ID API with `1yunpF9DkzXMzm0dHrsCuWsooEV` as the `id` path parameter.  The API will return: ```json {   \"authorization_model\":{     \"id\":\"1yunpF9DkzXMzm0dHrsCuWsooEV\",     \"type_definitions\":[       {         \"type\":\"document\",         \"relations\":{           \"reader\":{             \"union\":{               \"child\":[                 {                   \"this\":{}                 },                 {                   \"computedUserset\":{                     \"object\":\"\",                     \"relation\":\"writer\"                   }                 }               ]             }           },           \"writer\":{             \"this\":{}           }         }       }     ]   } } ``` In the above example, there is only 1 type (`document`) with 2 relations (`writer` and `reader`).

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using Auth0.Fga.Api;
using Auth0.Fga.Client;
using Auth0.Fga.Configuration;
using Auth0.Fga.Model;

namespace Example
{
    public class ReadAuthorizationModelExample
    {
        public static void Main()
        {
            var storeId = Environment.GetEnvironmentVariable("AUTH0_FGA_STORE_ID");
            var environment = Environment.GetEnvironmentVariable("AUTH0_FGA_ENVIRONMENT")
            var configuration = new Configuration(storeId, environment) {
                ClientId = Environment.GetEnvironmentVariable("AUTH0_FGA_CLIENT_ID"),
                ClientSecret = Environment.GetEnvironmentVariable("AUTH0_FGA_CLIENT_SECRET"),
            };
            HttpClient httpClient = new HttpClient();
            var auth0FgaApi = new Auth0FgaApi(config, httpClient);
            var id = "id_example";  // string | 

            try
            {
                // Return a particular version of an authorization model
                ReadAuthorizationModelResponse response = await auth0FgaApi.ReadAuthorizationModel(id);
                Debug.WriteLine(response);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling Auth0FgaApi.ReadAuthorizationModel: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------

 **id** | **string**|  | 

### Return type

[**ReadAuthorizationModelResponse**](ReadAuthorizationModelResponse.md)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | A successful response. |  -  |
| **400** | Request failed due to invalid input. |  -  |
| **401** | Request failed due to authentication errors. |  -  |
| **403** | Request failed due to forbidden permission. |  -  |
| **404** | Request failed due to incorrect path. |  -  |
| **429** | Request failed due to too many requests. |  -  |
| **500** | Request failed due to internal server error. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#api-endpoints) [[Back to Model list]](../README.md#models) [[Back to README]](../README.md)

<a name="readauthorizationmodels"></a>
# **ReadAuthorizationModels**
> ReadAuthorizationModelsResponse ReadAuthorizationModels (int? pageSize = null, string? continuationToken = null)

Return all the authorization model IDs for a particular store

The GET authorization-models API will return all the IDs of the authorization models for a certain store. Path parameter `store_id` is required. Auth0 FGA's response will contain an array of all authorization model IDs, sorted in descending order of creation.  ## [Limits](https://docs.fga.dev/intro/dashboard#limitations) - Each store has a limit of **30** read authorization-models requests per minute (RPM). - Each response can contain up to **50** authorization model IDs. ## Example Assume that the store's authorization model has been configured twice.  To get all the IDs of the authorization models that had been created in this store, call GET authorization-models.  The API will return a response that looks like: ```json {   \"authorization_model_ids\": [       \"1yunpF9DkzXMzm0dHrsCuWsooEV\",       \"1yundoHpJHlodgn4EOVar2DhmKp\"   ] } ``` If there are more authorization model IDs available, the response will contain an extra field `continuation_token`: ```json {   \"authorization_model_ids\": [       \"1yunpF9DkzXMzm0dHrsCuWsooEV\",       \"1yundoHpJHlodgn4EOVar2DhmKp\"   ],   \"continuation_token\": \"eyJwayI6IkxBVEVTVF9OU0NPTkZJR19hdXRoMHN0b3JlIiwic2siOiIxem1qbXF3MWZLZExTcUoyN01MdTdqTjh0cWgifQ==\" } ``` 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using Auth0.Fga.Api;
using Auth0.Fga.Client;
using Auth0.Fga.Configuration;
using Auth0.Fga.Model;

namespace Example
{
    public class ReadAuthorizationModelsExample
    {
        public static void Main()
        {
            var storeId = Environment.GetEnvironmentVariable("AUTH0_FGA_STORE_ID");
            var environment = Environment.GetEnvironmentVariable("AUTH0_FGA_ENVIRONMENT")
            var configuration = new Configuration(storeId, environment) {
                ClientId = Environment.GetEnvironmentVariable("AUTH0_FGA_CLIENT_ID"),
                ClientSecret = Environment.GetEnvironmentVariable("AUTH0_FGA_CLIENT_SECRET"),
            };
            HttpClient httpClient = new HttpClient();
            var auth0FgaApi = new Auth0FgaApi(config, httpClient);
            var pageSize = 56;  // int? |  (optional) 
            var continuationToken = "continuationToken_example";  // string? |  (optional) 

            try
            {
                // Return all the authorization model IDs for a particular store
                ReadAuthorizationModelsResponse response = await auth0FgaApi.ReadAuthorizationModels(pageSize, continuationToken);
                Debug.WriteLine(response);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling Auth0FgaApi.ReadAuthorizationModels: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------

 **pageSize** | **int?**|  | [optional] 
 **continuationToken** | **string?**|  | [optional] 

### Return type

[**ReadAuthorizationModelsResponse**](ReadAuthorizationModelsResponse.md)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | A successful response. |  -  |
| **400** | Request failed due to invalid input. |  -  |
| **401** | Request failed due to authentication errors. |  -  |
| **403** | Request failed due to forbidden permission. |  -  |
| **404** | Request failed due to incorrect path. |  -  |
| **429** | Request failed due to too many requests. |  -  |
| **500** | Request failed due to internal server error. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#api-endpoints) [[Back to Model list]](../README.md#models) [[Back to README]](../README.md)

<a name="readsettings"></a>
# **ReadSettings**
> ReadSettingsResponse ReadSettings ()

Return store settings, including the environment tag

The GET settings API will return the store's settings, including environment tag and an array of Auth0 FGA's allowed 3rd party token issuers. The environment tag is used to differentiate between development, staging, and production environments.   Path parameter `store_id` is required. ## Example GET settings API's response looks like: ```json {   \"environment\":\"STAGING\",   \"token_issuers\":[     {       \"id\":\"0ujsszwN8NRY24YaXiTIE2VWDTS\",       \"issuer_url\":\"https://example.issuer.com\"     }   ] } ``` In the above response, the store is configured as STAGING and there is one allowed 3rd party token issuer `https://example.issuer.com`.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using Auth0.Fga.Api;
using Auth0.Fga.Client;
using Auth0.Fga.Configuration;
using Auth0.Fga.Model;

namespace Example
{
    public class ReadSettingsExample
    {
        public static void Main()
        {
            var storeId = Environment.GetEnvironmentVariable("AUTH0_FGA_STORE_ID");
            var environment = Environment.GetEnvironmentVariable("AUTH0_FGA_ENVIRONMENT")
            var configuration = new Configuration(storeId, environment) {
                ClientId = Environment.GetEnvironmentVariable("AUTH0_FGA_CLIENT_ID"),
                ClientSecret = Environment.GetEnvironmentVariable("AUTH0_FGA_CLIENT_SECRET"),
            };
            HttpClient httpClient = new HttpClient();
            var auth0FgaApi = new Auth0FgaApi(config, httpClient);

            try
            {
                // Return store settings, including the environment tag
                ReadSettingsResponse response = await auth0FgaApi.ReadSettings();
                Debug.WriteLine(response);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling Auth0FgaApi.ReadSettings: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------


### Return type

[**ReadSettingsResponse**](ReadSettingsResponse.md)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | A successful response. |  -  |
| **400** | Request failed due to invalid input. |  -  |
| **401** | Request failed due to authentication errors. |  -  |
| **403** | Request failed due to forbidden permission. |  -  |
| **404** | Request failed due to incorrect path. |  -  |
| **429** | Request failed due to too many requests. |  -  |
| **500** | Request failed due to internal server error. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#api-endpoints) [[Back to Model list]](../README.md#models) [[Back to README]](../README.md)

<a name="write"></a>
# **Write**
> Object Write (WriteRequestParams body)

Add or delete tuples from the store

The POST write API will update the tuples for a certain store.  Tuples and type definitions allow Auth0 FGA to determine whether a relationship exists between an object and an user. Path parameter `store_id` is required.  In the body, `writes` adds new tuples while `deletes` remove existing tuples.  `lock_tuple` is reserved for future use.  ## [Limits](https://docs.fga.dev/intro/dashboard#limitations) - Each write API call allows at most **10** tuples. - Each store has a limit of **50000** tuples. - Each store has a limit of **3** write requests per second (RPS). ## Example ### Adding relationships To add `anne@auth0.com` as a `writer` for `document:2021-budget`, call write API with the following  ```json {   \"writes\": {     \"tuple_keys\": [       {         \"user\": \"anne@auth0.com\",         \"relation\": \"writer\",         \"object\": \"document:2021-budget\"       }     ]   } } ``` ### Removing relationships To remove `bob@auth0.com` as a `reader` for `document:2021-budget`, call write API with the following  ```json {   \"deletes\": {     \"tuple_keys\": [       {         \"user\": \"bob@auth0.com\",         \"relation\": \"reader\",         \"object\": \"document:2021-budget\"       }     ]   } } ``` 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using Auth0.Fga.Api;
using Auth0.Fga.Client;
using Auth0.Fga.Configuration;
using Auth0.Fga.Model;

namespace Example
{
    public class WriteExample
    {
        public static void Main()
        {
            var storeId = Environment.GetEnvironmentVariable("AUTH0_FGA_STORE_ID");
            var environment = Environment.GetEnvironmentVariable("AUTH0_FGA_ENVIRONMENT")
            var configuration = new Configuration(storeId, environment) {
                ClientId = Environment.GetEnvironmentVariable("AUTH0_FGA_CLIENT_ID"),
                ClientSecret = Environment.GetEnvironmentVariable("AUTH0_FGA_CLIENT_SECRET"),
            };
            HttpClient httpClient = new HttpClient();
            var auth0FgaApi = new Auth0FgaApi(config, httpClient);
            var body = new WriteRequestParams(); // WriteRequestParams | 

            try
            {
                // Add or delete tuples from the store
                Object response = await auth0FgaApi.Write(body);
                Debug.WriteLine(response);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling Auth0FgaApi.Write: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------

 **body** | [**WriteRequestParams**](WriteRequestParams.md)|  | 

### Return type

**Object**

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | A successful response. |  -  |
| **400** | Request failed due to invalid input. |  -  |
| **401** | Request failed due to authentication errors. |  -  |
| **403** | Request failed due to forbidden permission. |  -  |
| **404** | Request failed due to incorrect path. |  -  |
| **429** | Request failed due to too many requests. |  -  |
| **500** | Request failed due to internal server error. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#api-endpoints) [[Back to Model list]](../README.md#models) [[Back to README]](../README.md)

<a name="writeassertions"></a>
# **WriteAssertions**
> void WriteAssertions (string authorizationModelId, WriteAssertionsRequestParams body)

Upsert assertions for an authorization model ID

The POST assertions API will add new assertions for an authorization model id, or overwrite the existing ones. An assertion is an object that contains a tuple key, and the expectation of whether a call to the Check API of that tuple key will return true or false. 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using Auth0.Fga.Api;
using Auth0.Fga.Client;
using Auth0.Fga.Configuration;
using Auth0.Fga.Model;

namespace Example
{
    public class WriteAssertionsExample
    {
        public static void Main()
        {
            var storeId = Environment.GetEnvironmentVariable("AUTH0_FGA_STORE_ID");
            var environment = Environment.GetEnvironmentVariable("AUTH0_FGA_ENVIRONMENT")
            var configuration = new Configuration(storeId, environment) {
                ClientId = Environment.GetEnvironmentVariable("AUTH0_FGA_CLIENT_ID"),
                ClientSecret = Environment.GetEnvironmentVariable("AUTH0_FGA_CLIENT_SECRET"),
            };
            HttpClient httpClient = new HttpClient();
            var auth0FgaApi = new Auth0FgaApi(config, httpClient);
            var authorizationModelId = "authorizationModelId_example";  // string | 
            var body = new WriteAssertionsRequestParams(); // WriteAssertionsRequestParams | 

            try
            {
                // Upsert assertions for an authorization model ID
                auth0FgaApi.WriteAssertions(authorizationModelId, body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling Auth0FgaApi.WriteAssertions: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------

 **authorizationModelId** | **string**|  | 
 **body** | [**WriteAssertionsRequestParams**](WriteAssertionsRequestParams.md)|  | 

### Return type

void (empty response body)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **204** | A successful response. |  -  |
| **400** | Request failed due to invalid input. |  -  |
| **401** | Request failed due to authentication errors. |  -  |
| **403** | Request failed due to forbidden permission. |  -  |
| **404** | Request failed due to incorrect path. |  -  |
| **429** | Request failed due to too many requests. |  -  |
| **500** | Request failed due to internal server error. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#api-endpoints) [[Back to Model list]](../README.md#models) [[Back to README]](../README.md)

<a name="writeauthorizationmodel"></a>
# **WriteAuthorizationModel**
> WriteAuthorizationModelResponse WriteAuthorizationModel (TypeDefinitions body)

Create a new authorization model

The POST authorization-model API will update the authorization model for a certain store. Path parameter `store_id` and `type_definitions` array in the body are required.  Each item in the `type_definitions` array is a type definition as specified in the field `type_definition`. The response will return the authorization model's ID in the `id` field.  ## [Limits](https://docs.fga.dev/intro/dashboard#limitations) - There can be at most **24** items in the type_definitions array. - Each store has a limit of **10** POST authorization-models requests per minute (RPM). ## Example To update the authorization model with a single `document` authorization model, call POST authorization-models API with the body:  ```json {   \"type_definitions\":[     {       \"type\":\"document\",       \"relations\":{         \"reader\":{           \"union\":{             \"child\":[               {                 \"this\":{                  }               },               {                 \"computedUserset\":{                   \"object\":\"\",                   \"relation\":\"writer\"                 }               }             ]           }         },         \"writer\":{           \"this\":{            }         }       }     }   ] } ``` Auth0 FGA's response will include the version id for this authorization model, which will look like  ``` {\"authorization_model_id\": \"1yunpF9DkzXMzm0dHrsCuWsooEV\"} ``` 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using Auth0.Fga.Api;
using Auth0.Fga.Client;
using Auth0.Fga.Configuration;
using Auth0.Fga.Model;

namespace Example
{
    public class WriteAuthorizationModelExample
    {
        public static void Main()
        {
            var storeId = Environment.GetEnvironmentVariable("AUTH0_FGA_STORE_ID");
            var environment = Environment.GetEnvironmentVariable("AUTH0_FGA_ENVIRONMENT")
            var configuration = new Configuration(storeId, environment) {
                ClientId = Environment.GetEnvironmentVariable("AUTH0_FGA_CLIENT_ID"),
                ClientSecret = Environment.GetEnvironmentVariable("AUTH0_FGA_CLIENT_SECRET"),
            };
            HttpClient httpClient = new HttpClient();
            var auth0FgaApi = new Auth0FgaApi(config, httpClient);
            var body = new TypeDefinitions(); // TypeDefinitions | 

            try
            {
                // Create a new authorization model
                WriteAuthorizationModelResponse response = await auth0FgaApi.WriteAuthorizationModel(body);
                Debug.WriteLine(response);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling Auth0FgaApi.WriteAuthorizationModel: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------

 **body** | [**TypeDefinitions**](TypeDefinitions.md)|  | 

### Return type

[**WriteAuthorizationModelResponse**](WriteAuthorizationModelResponse.md)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **201** | A successful response. |  -  |
| **400** | Request failed due to invalid input. |  -  |
| **401** | Request failed due to authentication errors. |  -  |
| **403** | Request failed due to forbidden permission. |  -  |
| **404** | Request failed due to incorrect path. |  -  |
| **429** | Request failed due to too many requests. |  -  |
| **500** | Request failed due to internal server error. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#api-endpoints) [[Back to Model list]](../README.md#models) [[Back to README]](../README.md)

<a name="writesettings"></a>
# **WriteSettings**
> WriteSettingsResponse WriteSettings (WriteSettingsRequestParams body)

Update the environment tag for a store

The PATCH settings API will update the environment tag to differentiate between development, staging, and production environments. Path parameter `store_id` is required. The response will return the updated environment tag as well as other configuration settings.  ## Example To update store's environment tag to `STAGING`, call PATCH settings API with the following with the body:  ```json {\"environment\": \"STAGING\"} ``` 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using Auth0.Fga.Api;
using Auth0.Fga.Client;
using Auth0.Fga.Configuration;
using Auth0.Fga.Model;

namespace Example
{
    public class WriteSettingsExample
    {
        public static void Main()
        {
            var storeId = Environment.GetEnvironmentVariable("AUTH0_FGA_STORE_ID");
            var environment = Environment.GetEnvironmentVariable("AUTH0_FGA_ENVIRONMENT")
            var configuration = new Configuration(storeId, environment) {
                ClientId = Environment.GetEnvironmentVariable("AUTH0_FGA_CLIENT_ID"),
                ClientSecret = Environment.GetEnvironmentVariable("AUTH0_FGA_CLIENT_SECRET"),
            };
            HttpClient httpClient = new HttpClient();
            var auth0FgaApi = new Auth0FgaApi(config, httpClient);
            var body = new WriteSettingsRequestParams(); // WriteSettingsRequestParams | 

            try
            {
                // Update the environment tag for a store
                WriteSettingsResponse response = await auth0FgaApi.WriteSettings(body);
                Debug.WriteLine(response);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling Auth0FgaApi.WriteSettings: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------

 **body** | [**WriteSettingsRequestParams**](WriteSettingsRequestParams.md)|  | 

### Return type

[**WriteSettingsResponse**](WriteSettingsResponse.md)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | A successful response. |  -  |
| **400** | Request failed due to invalid input. |  -  |
| **401** | Request failed due to authentication errors. |  -  |
| **403** | Request failed due to forbidden permission. |  -  |
| **404** | Request failed due to incorrect path. |  -  |
| **429** | Request failed due to too many requests. |  -  |
| **500** | Request failed due to internal server error. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#api-endpoints) [[Back to Model list]](../README.md#models) [[Back to README]](../README.md)

<a name="writetokenissuer"></a>
# **WriteTokenIssuer**
> WriteTokenIssuersResponse WriteTokenIssuer (WriteTokenIssuersRequestParams body)

Add 3rd party token issuer for Auth0 FGA read and write operations

The POST token-issuers API will configure FGA so that tokens issued by the specified 3rd party token issuer will be allowed for Auth0 FGA's read and write operations. Otherwise, only tokens issued by Auth0 FGA's issuer (`fga.us.auth0.com`) will be accepted. ## Example To allow tokens issued by the 3rd party token issuer `https://example.issuer.com` for Auth0 FGA's read and write operations: 1. In the 3rd party issuer, configure Auth0 FGA API with the following audience in its issuer configuration: `https://api.us1.fga.dev`. 2. Call POST token-issuers API with the body: `{\"issuer_url\": \"https://example.issuer.com\"}`  The response will be the id that is associated with the token issuer: ```json {   \"id\":\"0ujsszwN8NRY24YaXiTIE2VWDTS\" } ``` 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using Auth0.Fga.Api;
using Auth0.Fga.Client;
using Auth0.Fga.Configuration;
using Auth0.Fga.Model;

namespace Example
{
    public class WriteTokenIssuerExample
    {
        public static void Main()
        {
            var storeId = Environment.GetEnvironmentVariable("AUTH0_FGA_STORE_ID");
            var environment = Environment.GetEnvironmentVariable("AUTH0_FGA_ENVIRONMENT")
            var configuration = new Configuration(storeId, environment) {
                ClientId = Environment.GetEnvironmentVariable("AUTH0_FGA_CLIENT_ID"),
                ClientSecret = Environment.GetEnvironmentVariable("AUTH0_FGA_CLIENT_SECRET"),
            };
            HttpClient httpClient = new HttpClient();
            var auth0FgaApi = new Auth0FgaApi(config, httpClient);
            var body = new WriteTokenIssuersRequestParams(); // WriteTokenIssuersRequestParams | 

            try
            {
                // Add 3rd party token issuer for Auth0 FGA read and write operations
                WriteTokenIssuersResponse response = await auth0FgaApi.WriteTokenIssuer(body);
                Debug.WriteLine(response);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling Auth0FgaApi.WriteTokenIssuer: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------

 **body** | [**WriteTokenIssuersRequestParams**](WriteTokenIssuersRequestParams.md)|  | 

### Return type

[**WriteTokenIssuersResponse**](WriteTokenIssuersResponse.md)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **201** | A successful response. |  -  |
| **400** | Request failed due to invalid input. |  -  |
| **401** | Request failed due to authentication errors. |  -  |
| **403** | Request failed due to forbidden permission. |  -  |
| **404** | Request failed due to incorrect path. |  -  |
| **429** | Request failed due to too many requests. |  -  |
| **500** | Request failed due to internal server error. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#api-endpoints) [[Back to Model list]](../README.md#models) [[Back to README]](../README.md)

