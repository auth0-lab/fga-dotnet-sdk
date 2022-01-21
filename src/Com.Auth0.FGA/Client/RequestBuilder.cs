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


using System.Web;

using Com.Auth0.FGA.Exceptions;

namespace Com.Auth0.FGA.Client;

public class RequestBuilder {
    public HttpMethod Method { get; set; }
    public string BasePath { get; set; }
    public string PathTemplate { get; set; }

    public Dictionary<string, string> PathParameters { get; set; }

    public Dictionary<string, string> QueryParameters { get; set; }

    public HttpContent? Body { get; set; }

    public RequestBuilder() {
        PathParameters = new Dictionary<string, string>();
        QueryParameters = new Dictionary<string, string>();
    }

    public string BuildPathString() {
        if (PathTemplate == null) {
            throw new Auth0FgaRequiredParamError("RequestBuilder.BuildUri", nameof(PathTemplate));
        }

        var path = PathTemplate;
        if (PathParameters == null || PathParameters.Count < 1) {
            return path;
        }

        foreach (var parameter in PathParameters) {
            path = path.Replace("{" + parameter.Key + "}", parameter.Value);
        }

        return path;
    }

    public string BuildQueryParamsString() {
        if (QueryParameters == null || QueryParameters.Count < 1) {
            return "";
        }

        var query = "?";
        foreach (var parameter in QueryParameters) {
            query = query + parameter.Key + "=" + HttpUtility.UrlEncode(parameter.Value) + "&";
        }

        return query;
    }

    public Uri BuildUri() {
        if (BasePath == null) {
            throw new Auth0FgaRequiredParamError("RequestBuilder.BuildUri", nameof(BasePath));
        }
        var uriString = $"{BasePath}";

        uriString += BuildPathString();
        uriString += BuildQueryParamsString();

        return new Uri(uriString);
    }

    public HttpRequestMessage BuildRequest() {
        if (Method == null) {
            throw new Auth0FgaRequiredParamError("RequestBuilder.BuildRequest", nameof(Method));
        }
        return new HttpRequestMessage() { RequestUri = BuildUri(), Method = Method, Content = Body };
    }
}
