using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CQRS.Commanding.AspNet
{

    public sealed class CommandHandlersMap : EndpointDataSource, IEndpointConventionBuilder
    {
        private readonly List<Endpoint> _endpoints;

        public CommandHandlersMap(string root, IEndpointRouteBuilder endpointRouteBuilder)
        {
            _endpoints = new List<Endpoint>();
            endpointRouteBuilder.MapGet(root + "/TestEntity/{id}/Create", RequestDelegate);
        }

        private Task RequestDelegate(HttpContext context)
        {
            var endpoints = context.Features[typeof(IEndpointFeature)] as IEndpointFeature;
            var endpoint = endpoints?.Endpoint;
            if (endpoint != null)
            {
                var route = endpoint as RouteEndpoint;

            }

            return Task.CompletedTask;// _next.Invoke(context);
        }

        public void Add(Action<EndpointBuilder> convention)
        {
            throw new NotImplementedException();
        }

        public override IChangeToken GetChangeToken()
        {
            throw new NotImplementedException();
        }

        public override IReadOnlyList<Endpoint> Endpoints => _endpoints;
    }

    public static class Configurations
    {
        public static IEndpointConventionBuilder MapCommandHandlers(this IEndpointRouteBuilder endpoints)
        {
            //var nested = endpoints.CreateApplicationBuilder();
            var handler = new CommandHandlersMap("/cmd", endpoints);
            //endpoints.Map("/cmd", nested.Build());
            return handler;
        }
    }
}
