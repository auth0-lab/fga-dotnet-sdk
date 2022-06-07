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


using System.Text;
using System.Text.Json;
using System.Web;

namespace Auth0.Fga.Client;

/// <summary>
///
/// </summary>
public static class Utils {
    public static HttpContent CreateJsonStringContent<T>(T body) {
        var json = JsonSerializer.Serialize(body);
        return new StringContent(json, Encoding.UTF8, "application/json");
    }

    public static string BuildQueryParams(IDictionary<string, string> parameters) {
        var query = "";
        foreach (var parameter in parameters) {
            query = query + parameter.Key + "=" + HttpUtility.UrlEncode(parameter.Value) + "&";
        }

        return query;
    }

    public static Uri BuildUri(string basePath, string resource, IDictionary<string, string>? parameters = null) {
        var uriString = $"{basePath}/{resource}";

        if (parameters != null) {
            uriString += BuildQueryParams(parameters);
        }

        return new Uri(uriString);
    }

    public static HttpRequestMessage BuildRequest(HttpMethod method, string basePath, string resource, IDictionary<string, string>? parameters = null) {
        return new HttpRequestMessage() {
            RequestUri = BuildUri(basePath, resource, parameters),
            Method = method,
        };
    }
}