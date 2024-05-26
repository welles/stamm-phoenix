using FastEndpoints;

namespace StammPhoenix.Api.Core;

public abstract class GetEndpoint<TRequest, TResponse, TGroup> : EndpointBase<TRequest, TResponse, TGroup>
    where TRequest : notnull
    where TGroup : EndpointGroup, new()
{
    public override Http[] EndpointVerbs { get; } = [ Http.GET ];
}

public abstract class GetEndpoint<TRequest, TGroup> : GetEndpoint<TRequest, object?, TGroup>
    where TRequest : notnull
    where TGroup : EndpointGroup, new();

public abstract class GetEndpointWithoutRequest<TResponse, TGroup> : GetEndpoint<EmptyRequest, TResponse, TGroup>
    where TGroup : EndpointGroup, new();

public abstract class GetEndpointWithoutRequest<TGroup> : GetEndpoint<EmptyRequest, object?, TGroup>
    where TGroup : EndpointGroup, new();
