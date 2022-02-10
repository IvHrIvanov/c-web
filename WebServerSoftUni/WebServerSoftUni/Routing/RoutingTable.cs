using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServerSoftUni.Common;
using WebServerSoftUni.HTTP;
using WebServerSoftUni.Responses;

namespace WebServerSoftUni.Routing
{
    public class RoutingTable : IRoutingTable
    {
        private readonly Dictionary<Method, Dictionary<string, Response>> routes;

        public RoutingTable() => this.routes = new()
        {
            [Method.GET] = new(),
            [Method.PUT] = new(),
            [Method.DELETE] = new(),
            [Method.POST] = new()
        };

        public IRoutingTable Map(string url, Method method, Response response) => method switch
        {
            Method.GET => this.MapGet(url, response),
            Method.POST => this.MapPost(url, response),
            _ => throw new InvalidOperationException($"Method {method} is not supported.")
        };

        public IRoutingTable MapPost(string url, Response response)
        {
            Guard.AgainstNull(url, nameof(url));
            Guard.AgainstNull(response, nameof(response));
            this.routes[Method.POST][url] = response;
            return this;
        }

        public IRoutingTable MapGet(string url, Response response)
        {
            Guard.AgainstNull(url, nameof(url));
            Guard.AgainstNull(response, nameof(response));

            this.routes[Method.GET][url] = response;
            return this;
        }

        public Response MatchRequest(Request request)
        {
            var requestMethod = request.Method;
            var requestUrl = request.Url;

            if (!this.routes.ContainsKey(requestMethod) || !this.routes[requestMethod].ContainsKey(requestUrl))
            {
                return new NotFoundResponse();
            }
            return this.routes[requestMethod][requestUrl];

        }
    }
}
